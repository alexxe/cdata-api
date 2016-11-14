namespace Example.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EdvNr = c.String(nullable: false, maxLength: 8),
                        FirstName = c.String(nullable: false, maxLength: 10),
                        LastName = c.String(nullable: false, maxLength: 10),
                        Street = c.String(maxLength: 35),
                        Ort = c.String(maxLength: 35),
                        Customer_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EdvNr = c.String(nullable: false, maxLength: 8),
                        CustomerNr = c.String(nullable: false, maxLength: 10),
                        Firma1 = c.String(maxLength: 100),
                        Firma2 = c.String(maxLength: 100),
                        ShortName = c.String(maxLength: 100),
                        Street = c.String(maxLength: 35),
                        Ort = c.String(maxLength: 35),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contact", "Customer_Id", "dbo.Customer");
            DropIndex("dbo.Contact", new[] { "Customer_Id" });
            DropTable("dbo.Customer");
            DropTable("dbo.Contact");
        }
    }
}
