using AutoMapper.Extensions.ExpressionMapping;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PupuCore.AspNetCore.Http;
using PupuCore.Infrastructure.Exceptions;
using StylishWebsiteBE.Data;
using StylishWebsiteBE.Domain.ReadModels.Identities;
using StylishWebsiteBE.Infrastructure.IRepositories.Carts;
using StylishWebsiteBE.Infrastructure.IRepositories.Identities;
using StylishWebsiteBE.Infrastructure.IRepositories.Options;
using StylishWebsiteBE.Infrastructure.IRepositories.Orders;
using StylishWebsiteBE.Infrastructure.IRepositories.Products;
using StylishWebsiteBE.Infrastructure.IServices.Carts;
using StylishWebsiteBE.Infrastructure.IServices.Identities;
using StylishWebsiteBE.Infrastructure.IServices.Options;
using StylishWebsiteBE.Infrastructure.IServices.Orders;
using StylishWebsiteBE.Infrastructure.IServices.Products;
using StylishWebsiteBE.Infrastructure.Repositories.Carts;
using StylishWebsiteBE.Infrastructure.Repositories.Identities;
using StylishWebsiteBE.Infrastructure.Repositories.Options;
using StylishWebsiteBE.Infrastructure.Repositories.Orders;
using StylishWebsiteBE.Infrastructure.Repositories.Products;
using StylishWebsiteBE.Infrastructure.Services.Carts;
using StylishWebsiteBE.Infrastructure.Services.Identities;
using StylishWebsiteBE.Infrastructure.Services.Options;
using StylishWebsiteBE.Infrastructure.Services.Orders;
using StylishWebsiteBE.Infrastructure.Services.Products;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StylishWebsiteBE.Infrastructure.Extensions
{
    public static class ServicesCollectionExtensions {
        public static IServiceCollection AddApplication(this IServiceCollection services, ConfigurationManager configuration)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var entryAssembly = Assembly.GetEntryAssembly();
            services.AddAutoMapper(configuration =>
            {
                configuration.Internal().MethodMappingEnabled = false;
                configuration.AddExpressionMapping();
            }, executingAssembly, entryAssembly);
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Default")));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:ValidAudience"],
                    ValidIssuer = configuration["Jwt:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"])),
                    RequireExpirationTime = false
                };
                options.Events = new JwtBearerEvents()
                {
                    OnForbidden = ctx =>
                    {
                        var message = SingleResultMessage.Fail(new ForbiddenException());
                        var result = JsonSerializer.Serialize(message, message.GetType(), new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles });
                        return ctx.Response.WriteAsync(result);
                    }
                };
            });
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Default")));
            services.AddIdentity<ApplicationUserReadModel, ApplicationRoleReadModel>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.AddTransient<HostInjectorDelegatingHandler>();
            services.AddHttpClient();
            // User
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<UserService>();
            // Role
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<RoleService>();
            // Product
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ProductService>();
            // ProductOption
            services.AddScoped<IProductOptionRepository, ProductOptionRepository>();
            services.AddScoped<IProductOptionService, ProductOptionService>();
            services.AddScoped<ProductOptionService>();
            // ProductVariant
            services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
            services.AddScoped<IProductVariantService, ProductVariantService>();
            services.AddScoped<ProductVariantService>();
            // VariantValue
            services.AddScoped<IVariantValueRepository, VariantValueRepository>();
            services.AddScoped<IVariantValueService, VariantValueService>();
            services.AddScoped<VariantValueService>();
            // Option
            services.AddScoped<IOptionRepository, OptionRepository>();
            services.AddScoped<IOptionService, OptionService>();
            services.AddScoped<OptionService>();
            // OptionValue
            services.AddScoped<IOptionValueRepository, OptionValueRepository>();
            services.AddScoped<IOptionValueService, OptionValueService>();
            services.AddScoped<OptionValueService>();
            // Order
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<OrderService>();
            // Order Detail
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<OrderDetailService>();
            // Cart
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<CartService>();
            // Cart Detail
            services.AddScoped<ICartDetailRepository, CartDetailRepository>();
            services.AddScoped<ICartDetailService, CartDetailService>();
            services.AddScoped<CartDetailService>();


            services.AddHealthChecks();
            services.AddHttpContextAccessor();
            services.AddHttpClient();

            return services;
        }
    }
}