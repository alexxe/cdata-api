namespace Example.DB
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    using Example.DB.Migrations;

    public class CrmDataModel : DbContext
    {
        public CrmDataModel()
            : base("name=DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        static CrmDataModel()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CrmDataModel, Configuration>());
        }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Contact> Contacts { get; set; }

        public virtual DbSet<User> Users { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().
                HasKey(c => c.Id).
                HasMany(e => e.Contacts).
                WithRequired(e => e.Customer);
            modelBuilder.Entity<Customer>().
                HasOptional(c => c.CreatedBy).
                WithMany( c => c.Customers);
           modelBuilder.Entity<Customer>().Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Contact>().
                HasKey(x => x.Id).
                HasRequired(e => e.Customer).
                WithMany(e => e.Contacts);
            modelBuilder.Entity<Contact>().Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<User>().
               HasKey(x => x.Id).
               HasMany(e => e.Customers).
               WithOptional(c => c.CreatedBy);
            modelBuilder.Entity<User>().Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            base.OnModelCreating(modelBuilder);
        }
    }
}
