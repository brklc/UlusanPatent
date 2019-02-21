namespace DenemeIdentitiy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "EndDate");
            DropColumn("dbo.AspNetUsers", "StartDate");
        }
    }
}
