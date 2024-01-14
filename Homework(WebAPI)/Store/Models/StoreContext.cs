using Microsoft.EntityFrameworkCore;

namespace NetStore.Models
{
    public class StoreContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Stores { get; set; }
        public StoreContext()
        {

        }

        public StoreContext(DbContextOptions<StoreContext> dbc) : base(dbc)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .Build();


            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"))
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.HasKey(x => x.ProdId).HasName("ProductID");
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name)
               .HasColumnName("ProductName")
               .HasMaxLength(255)
               .IsRequired();

                entity.Property(e => e.Description)
                      .HasColumnName("Description")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.Price)
                      .HasColumnName("Price")
                      .IsRequired();

                entity.HasOne(x => x.Group)
                .WithMany(c => c.Products)
                .HasForeignKey(y => y.ProdId)
                .IsRequired()
                .HasConstraintName("GroupToProduct");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("ProductGroups");

                entity.HasKey(x => x.GroupId).HasName("GroupID");
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name)
               .HasColumnName("ProductName")
               .HasMaxLength(255);
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {

                entity.ToTable("Storage");

                entity.HasKey(x => x.WhId).HasName("StoreID");


                entity.Property(e => e.Name)
                .HasColumnName("StorageName");

                entity.Property(e => e.Count)
                .HasColumnName("ProductCount");

                entity.HasMany(x => x.Products)
                .WithMany(m => m.Stores)
                .UsingEntity(j => j.ToTable("StorageProduct"));
            });
        }
    }
}
