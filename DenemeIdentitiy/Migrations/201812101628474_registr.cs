namespace DenemeIdentitiy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class registr : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegistrationDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Compost = c.String(),
                        Fertilizer = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegistrationDocuments", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.RegistrationDocuments", new[] { "ApplicationUserId" });
            DropTable("dbo.RegistrationDocuments");
        }
    }
}
