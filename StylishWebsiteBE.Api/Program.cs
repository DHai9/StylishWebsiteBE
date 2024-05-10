using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using PupuCore.AspNetCore.Middlewares;
using StylishWebsiteBE.Domain.DTOs.Cards;
using StylishWebsiteBE.Domain.DTOs.Identities;
using StylishWebsiteBE.Domain.DTOs.Options;
using StylishWebsiteBE.Domain.DTOs.Orders;
using StylishWebsiteBE.Domain.DTOs.Products;
using StylishWebsiteBE.Domain.DTOs.Statisticals;
using StylishWebsiteBE.Domain.Enums;
using StylishWebsiteBE.Infrastructure.Extensions;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddOData(opt =>
{
    opt.Count().Filter().Expand().Select().OrderBy().SetMaxTop(5000).AddRouteComponents("odata", GetEdmModel());
    opt.EnableQueryFeatures();
    opt.TimeZone = TimeZoneInfo.Utc;
}).AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddCors(options => options.AddPolicy(
    "AllowInternal",
    policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowedToAllowWildcardSubdomains()
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost
});
// Use odata route debug, /$odata
app.UseODataRouteDebug();
// Add OData /$query middleware
app.UseODataQueryRequest();
// Add the OData Batch middleware to support OData $Batch
app.UseODataBatching();
app.UseMiddleware<ExceptionMiddleware>(true);
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Custom
app.UseCors("AllowInternal");

app.Run();


IEdmModel GetEdmModel()
{
    var odataBuilder = new ODataConventionModelBuilder();
    odataBuilder.EntityType<UserDto>().HasKey(entity => new { entity.Id });
    odataBuilder.EntitySet<UserDto>("Users");
    odataBuilder.EntityType<UserDto>().Collection.Action("SignIn").ReturnsFromEntitySet<UserDto>("Users");
    odataBuilder.EntityType<UserDto>().Collection.Action("Register").ReturnsFromEntitySet<UserDto>("Users");

    odataBuilder.EntityType<ProductDto>().HasKey(entity => new { entity.Id });
    odataBuilder.EntitySet<ProductDto>("Products");

    odataBuilder.EntityType<ProductOptionDto>().HasKey(entity => new { entity.Id });
    odataBuilder.EntitySet<ProductOptionDto>("ProductOptions");

    odataBuilder.EntityType<ProductVariantDto>().HasKey(entity => new { entity.Id });
    odataBuilder.EntitySet<ProductVariantDto>("ProductVariants");


    odataBuilder.EntityType<VariantValueDto>().HasKey(entity => new { entity.Id });
    odataBuilder.EntitySet<VariantValueDto>("VariantValues");

    odataBuilder.EntityType<OptionDto>().HasKey(entity => new { entity.Id });
    odataBuilder.EntitySet<OptionDto>("Options");
    var getByType = odataBuilder.EntityType<OptionDto>()
        .Collection.Function("GetByType")
        .ReturnsFromEntitySet<OptionDto>("Options")
        .Returns<OptionDto>().Parameter<int>("type");

    odataBuilder.EntityType<OptionValueDto>().HasKey(entity => new { entity.Id });
    odataBuilder.EntitySet<OptionValueDto>("OptionValues");

    odataBuilder.EntityType<OrderDto>().HasKey(entity => new { entity.Id });
    odataBuilder.EntitySet<OrderDto>("Orders");
    var deleteItemOrder = odataBuilder.EntityType<OrderDto>().Collection.Action("DeleteItem").ReturnsFromEntitySet<OrderDto>("Orders");

    odataBuilder.EntityType<CartDto>().HasKey(entity => new { entity.Id });
    odataBuilder.EntitySet<CartDto>("Carts");
    var getByUserId = odataBuilder.EntityType<CartDto>().Collection.Function("GetByUserId").Returns<CartDto>().Parameter<Guid>("userId");
    var addItem = odataBuilder.EntityType<CartDto>().Collection.Action("AddItem").ReturnsFromEntitySet<CartDto>("Carts");
    var deleteItemCart = odataBuilder.EntityType<CartDto>().Collection.Action("DeleteItem").ReturnsFromEntitySet<CartDto>("Carts");

    odataBuilder.EntityType<ProductsStatisticalDto>().HasKey(entity => new { entity.Id });
    odataBuilder.EntitySet<ProductsStatisticalDto>("ProductsStatisticals");
    var getProductsBestSeller = odataBuilder.EntityType<ProductsStatisticalDto>().
        Collection.Function("GetProductsBestSeller").
        ReturnsFromEntitySet<ProductsStatisticalDto>("ProductsStatisticals").
        Returns<ProductsStatisticalDto>();
    getProductsBestSeller.Parameter<int?>("month");
    getProductsBestSeller.Parameter<int?>("year");
    getProductsBestSeller.Parameter<string?>("skuId");

    odataBuilder.EntityType<UserStatisticalsDto>().HasKey(entity => new { entity.Id });
    odataBuilder.EntitySet<UserStatisticalsDto>("UserStatisticals");
    var getUserUserStatisticals = odataBuilder.EntityType<UserStatisticalsDto>().
        Collection.Function("GetUserUserStatisticals").
        ReturnsFromEntitySet<UserStatisticalsDto>("UserStatisticals").
        Returns<UserStatisticalsDto>();
    getProductsBestSeller.Parameter<int>("month");
    getProductsBestSeller.Parameter<int>("year");

    return odataBuilder.GetEdmModel();
}