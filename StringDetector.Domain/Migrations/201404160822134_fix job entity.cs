namespace StringDetector.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixjobentity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobEntities", "Report", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobEntities", "Report", c => c.String(nullable: false));
        }
    }
}
