using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using StylishWebsiteBE.Domain.Enums;
using StylishWebsiteBE.Domain.ReadModels.Cards;
using StylishWebsiteBE.Domain.ReadModels.Identities;
using StylishWebsiteBE.Domain.ReadModels.Options;
using StylishWebsiteBE.Domain.ReadModels.Orders;
using StylishWebsiteBE.Domain.ReadModels.Products;
using System.Diagnostics.CodeAnalysis;

namespace StylishWebsiteBE.Data {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUserReadModel, ApplicationRoleReadModel, Guid> {
        public virtual DbSet<ProductReadModel> Products { get; set; }
        public virtual DbSet<ProductVariantReadModel> ProductVariants { get; set; }
        public virtual DbSet<OptionReadModel> Options { get; set; }
        public virtual DbSet<OptionValueReadModel> OptionValues { get; set; }
        public virtual DbSet<ProductOptionReadModel> ProductOptions { get; set; }
        public virtual DbSet<VariantValueReadModel> VariantValues { get; set; }
        public virtual DbSet<OrderDetailReadModel> OrderDetails { get; set; }
        public virtual DbSet<OrderReadModel> Orders { get; set; }
        public virtual DbSet<FunctionReadModel> Functions { get; set; }
        public virtual DbSet<PermissionReadModel> Permissions { get; set; }
        public virtual DbSet<RefreshTokenReadModel> RefreshTokens { get; set; }
        public virtual DbSet<CartReadModel> Carts { get; set; }
        public virtual DbSet<CartDetailReadModel> CartDetails { get; set; }

        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
            //NpgsqlConnection.GlobalTypeMapper.MapEnum<OptionType>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.LogTo(Console.WriteLine);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Function

            modelBuilder.Entity<FunctionReadModel>().HasIndex(entity => entity.Id).IncludeProperties(entity => new { entity.ServiceName, entity.FunctionName, entity.CreatedBy, entity.CreatedTime, entity.ModifiedBy, entity.ModifiedTime }).IsUnique();
            modelBuilder.Entity<PermissionReadModel>().HasKey(entity => new { entity.Type, entity.TypeId, entity.FunctionId });
            modelBuilder.HasPostgresEnum<PermissionType>();

            #endregion

            #region RefreshToken

            modelBuilder.Entity<RefreshTokenReadModel>().HasKey(entity => new { entity.Id });
            modelBuilder.Entity<RefreshTokenReadModel>().HasIndex(entity => entity.Id).IncludeProperties(entity => new { entity.UserId, entity.DeviceId, entity.RefreshToken, entity.ExpiredTime, entity.RevokedTime });
            modelBuilder.Entity<RefreshTokenReadModel>().HasIndex(entity => entity.RefreshToken).IncludeProperties(entity => new { entity.Id, entity.UserId, entity.DeviceId, entity.ExpiredTime, entity.RevokedTime });

            #endregion

            #region ProductModel

            modelBuilder.Entity<ProductReadModel>().HasKey(entity => entity.Id);
            modelBuilder.Entity<ProductReadModel>().HasIndex(entity => entity.Id).IncludeProperties(entity => new { entity.Name, entity.Description, entity.Category, entity.Brand, entity.IsDeleted, entity.CreatedBy, entity.CreatedTime, entity.ModifiedBy, entity.ModifiedTime, entity.DeletedBy, entity.DeletedTime }).IsUnique();
            modelBuilder.Entity<ProductReadModel>().HasIndex(entity => entity.Name).IncludeProperties(entity => new { entity.Id, entity.Description, entity.Category, entity.Brand, entity.IsDeleted, entity.CreatedBy, entity.CreatedTime, entity.ModifiedBy, entity.ModifiedTime, entity.DeletedBy, entity.DeletedTime }).IsUnique();

            #endregion

            #region ProductVariant

            modelBuilder.Entity<ProductVariantReadModel>().HasKey(entity => entity.Id);
            modelBuilder.Entity<ProductVariantReadModel>().Property(entity => entity.Images).HasColumnType("jsonb");

            modelBuilder.Entity<ProductVariantReadModel>().HasIndex(entity => entity.Id).IncludeProperties(entity => new { entity.SkuId, entity.ProductId, entity.ImportPrice, entity.Price, entity.Quantity, entity.IsDeleted }).IsUnique();
            modelBuilder.Entity<ProductVariantReadModel>().HasIndex(entity => entity.SkuId).IncludeProperties(entity => new { entity.Id, entity.ProductId, entity.ImportPrice, entity.Price, entity.Quantity, entity.IsDeleted }).IsUnique();
            modelBuilder.Entity<ProductVariantReadModel>().HasIndex(entity => entity.Price).IncludeProperties(entity => new { entity.Id, entity.ProductId, entity.ImportPrice, entity.SkuId, entity.Quantity, entity.IsDeleted });

            modelBuilder.Entity<ProductVariantReadModel>().HasOne(entity => entity.Product).WithMany(entity => entity.ProductVariants).HasForeignKey(entity => entity.ProductId);

            #endregion

            #region Option

            modelBuilder.Entity<OptionReadModel>().HasKey(entity => entity.Id);
            modelBuilder.Entity<OptionReadModel>().HasIndex(entity => entity.Id).IncludeProperties(entity => new { entity.Name, entity.IsDeleted }).IsUnique();
            modelBuilder.Entity<OptionReadModel>().HasIndex(entity => entity.Name).IncludeProperties(entity => new { entity.Id, entity.IsDeleted });

            #endregion

            #region OptionValue

            modelBuilder.Entity<OptionValueReadModel>().HasKey(entity => entity.Id);
            modelBuilder.Entity<OptionValueReadModel>().Property(entity => entity.Value).HasDefaultValue("Updating");

            modelBuilder.Entity<OptionValueReadModel>().HasIndex(entity => entity.Id).IncludeProperties(entity => new { entity.OptionId, entity.Value, entity.IsDeleted }).IsUnique();
            modelBuilder.Entity<OptionValueReadModel>().HasIndex(entity => new { entity.OptionId }).IncludeProperties(entity => new { entity.Id, entity.Value, entity.IsDeleted });
            modelBuilder.Entity<OptionValueReadModel>().HasIndex(entity => entity.Value).IncludeProperties(entity => new { entity.Id, entity.OptionId, entity.IsDeleted });

            modelBuilder.Entity<OptionValueReadModel>().HasOne(entity => entity.Option).WithMany(entity => entity.OptionValues).HasForeignKey(entity => entity.OptionId);

            #endregion

            #region ProductOption

            modelBuilder.Entity<ProductOptionReadModel>().HasKey(entity => entity.Id);

            modelBuilder.Entity<ProductOptionReadModel>().HasIndex(entity => entity.Id).IncludeProperties(entity => new { entity.OptionId, entity.ProductId }).IsUnique();
            modelBuilder.Entity<ProductOptionReadModel>().HasIndex(entity => entity.OptionId).IncludeProperties(entity => new { entity.Id, entity.ProductId });
            modelBuilder.Entity<ProductOptionReadModel>().HasIndex(entity => entity.ProductId).IncludeProperties(entity => new { entity.Id, entity.OptionId });

            modelBuilder.Entity<ProductOptionReadModel>().HasOne(entity => entity.Product).WithMany(entity => entity.ProductOptions).HasForeignKey(entity => entity.ProductId);
            modelBuilder.Entity<ProductOptionReadModel>().HasOne(entity => entity.Option).WithMany(entity => entity.ProductOptions).HasForeignKey(entity => entity.OptionId);

            #endregion

            #region VariantValues

            modelBuilder.Entity<VariantValueReadModel>().HasKey(entity => entity.Id);
            modelBuilder.Entity<VariantValueReadModel>().Property(entity => entity.ProductId).ValueGeneratedOnAdd();
            modelBuilder.Entity<VariantValueReadModel>().Property(entity => entity.ProductVariantId).ValueGeneratedOnAdd();
            modelBuilder.Entity<VariantValueReadModel>().Property(entity => entity.ProductOptionId).ValueGeneratedOnAdd();
            modelBuilder.Entity<VariantValueReadModel>().Property(entity => entity.OptionId).ValueGeneratedOnAdd();

            modelBuilder.Entity<VariantValueReadModel>().HasIndex(entity => entity.Id).IncludeProperties(entity => new { entity.OptionId, entity.ProductId, entity.ProductVariantId, entity.ValueId }).IsUnique();
            modelBuilder.Entity<VariantValueReadModel>().HasIndex(entity => entity.ProductId).IncludeProperties(entity => new { entity.OptionId, entity.Id, entity.ProductVariantId, entity.ValueId });
            modelBuilder.Entity<VariantValueReadModel>().HasIndex(entity => entity.ProductVariantId).IncludeProperties(entity => new { entity.OptionId, entity.Id, entity.ProductId, entity.ValueId });

            modelBuilder.Entity<VariantValueReadModel>().HasOne(entity => entity.Products).WithMany(entity => entity.VariantValues).HasForeignKey(entity => entity.ProductId);
            modelBuilder.Entity<VariantValueReadModel>().HasOne(entity => entity.ProductVariants).WithMany(entity => entity.VariantValues).HasForeignKey(entity => entity.ProductVariantId);
            modelBuilder.Entity<VariantValueReadModel>().HasOne(entity => entity.Options).WithMany(entity => entity.VariantValues).HasForeignKey(entity => entity.OptionId);
            modelBuilder.Entity<VariantValueReadModel>().HasOne(entity => entity.OptionValues).WithMany(entity => entity.VariantValues).HasForeignKey(entity => entity.ValueId);

            #endregion

            #region Orders

            modelBuilder.Entity<OrderReadModel>().HasKey(entity => entity.Id);

            modelBuilder.Entity<OrderReadModel>().HasIndex(entity => entity.Id).IncludeProperties(entity => new { entity.UserId, entity.DetailAddress, entity.OrderCode, entity.PhoneNumber}).IsUnique();
            modelBuilder.Entity<OrderReadModel>().HasIndex(entity => entity.UserId).IncludeProperties(entity => new { entity.Id });

            #endregion

            #region OrderDetails

            modelBuilder.Entity<OrderDetailReadModel>().HasKey(entity => entity.Id);

            modelBuilder.Entity<OrderDetailReadModel>().HasIndex(entity => entity.Id).IncludeProperties(entity => new { entity.OrderId, entity.ProductVariantId, entity.Quantity, entity.Price }).IsUnique();
            modelBuilder.Entity<OrderDetailReadModel>().HasIndex(entity => entity.OrderId).IncludeProperties(entity => new { entity.Id, entity.ProductVariantId, entity.Quantity, entity.Price });

            modelBuilder.Entity<OrderDetailReadModel>().HasOne(entity => entity.Order).WithMany(entity => entity.OrderDetails).HasForeignKey(entity => entity.OrderId);
            modelBuilder.Entity<OrderDetailReadModel>().HasOne(entity => entity.ProductVariant).WithMany(entity => entity.OrderDetails).HasForeignKey(entity => entity.ProductVariantId);

            #endregion

            #region Carts

            modelBuilder.Entity<CartReadModel>().HasKey(entity => entity.Id);

            modelBuilder.Entity<CartReadModel>().HasIndex(entity => entity.Id).IncludeProperties(entity => new { entity.UserId }).IsUnique();
            modelBuilder.Entity<CartReadModel>().HasIndex(entity => entity.UserId).IncludeProperties(entity => new { entity.Id });

            #endregion

            #region CartDetails

            modelBuilder.Entity<CartDetailReadModel>().HasKey(entity => entity.Id);

            modelBuilder.Entity<CartDetailReadModel>().HasIndex(entity => entity.Id).IncludeProperties(entity => new { entity.CartId, entity.ProductVariantId, entity.Quantity }).IsUnique();
            modelBuilder.Entity<CartDetailReadModel>().HasIndex(entity => entity.CartId).IncludeProperties(entity => new { entity.Id, entity.ProductVariantId, entity.Quantity});

            modelBuilder.Entity<CartDetailReadModel>().HasOne(entity => entity.Card).WithMany(entity => entity.CartDetails).HasForeignKey(entity => entity.CartId);
            modelBuilder.Entity<CartDetailReadModel>().HasOne(entity => entity.ProductVariants).WithMany(entity => entity.CartDetails).HasForeignKey(entity => entity.ProductVariantId);

            #endregion
        }
    }
}