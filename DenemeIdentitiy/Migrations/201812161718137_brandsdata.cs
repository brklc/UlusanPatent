namespace DenemeIdentitiy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class brandsdata : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Brands", "LicenseNumber", c => c.String());
            AddColumn("dbo.Brands", "ProductTypes", c => c.Int());
            AddColumn("dbo.Brands", "LicenseType", c => c.String());
            AddColumn("dbo.Brands", "ReasonForIssue", c => c.String());
            AddColumn("dbo.Brands", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Brands", "Description");
            DropColumn("dbo.Brands", "ReasonForIssue");
            DropColumn("dbo.Brands", "LicenseType");
            DropColumn("dbo.Brands", "ProductTypes");
            DropColumn("dbo.Brands", "LicenseNumber");
        }
    }
}
