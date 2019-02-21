namespace DenemeIdentitiy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class denemdeneme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Brands", "Status", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Brands", "Status");
        }
    }
}
