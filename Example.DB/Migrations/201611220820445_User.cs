namespace Example.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Customer", "CreatedBy_Id", c => c.Int());
            CreateIndex("dbo.Customer", "CreatedBy_Id");
            AddForeignKey("dbo.Customer", "CreatedBy_Id", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customer", "CreatedBy_Id", "dbo.User");
            DropIndex("dbo.Customer", new[] { "CreatedBy_Id" });
            DropColumn("dbo.Customer", "CreatedBy_Id");
            DropTable("dbo.User");
        }
    }
}
