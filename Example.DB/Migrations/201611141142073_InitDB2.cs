namespace Example.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contact", "Customer_Id", "dbo.Customer");
            AddForeignKey("dbo.Contact", "Customer_Id", "dbo.Customer", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contact", "Customer_Id", "dbo.Customer");
            AddForeignKey("dbo.Contact", "Customer_Id", "dbo.Customer", "Id");
        }
    }
}
