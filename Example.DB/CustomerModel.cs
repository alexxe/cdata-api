namespace Example.DB
{
    using System.Data.Entity;

    public partial class CustomerModel : DbContext
    {
        public CustomerModel()
            : base("CustomerModel")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
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
        }
    }
}
