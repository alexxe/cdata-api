namespace Example.DB
{
    using System.Data.Entity;

    using Example.DB.Migrations;

    public class CustomerModel : DbContext
    {
        public CustomerModel()
            : base("name=DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        static CustomerModel()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CustomerModel, Configuration>());
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().
                HasKey(c => c.Id).
                HasMany(e => e.Contacts).
                WithRequired(e => e.Customer);

            modelBuilder.Entity<Contact>().
                HasKey(x => x.Id).
                HasRequired(e => e.Customer).
                WithMany(e => e.Contacts);

            base.OnModelCreating(modelBuilder);
        }
    }
}
