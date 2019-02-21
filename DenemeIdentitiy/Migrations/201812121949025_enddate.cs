namespace DenemeIdentitiy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enddate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "RegisterEndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "RegisterEndDate", c => c.Int(nullable: false));
        }
    }
}
