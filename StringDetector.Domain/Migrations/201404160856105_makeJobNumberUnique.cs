namespace StringDetector.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeJobNumberUnique : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobEntities", "JobNumber", c => c.String(nullable: false, maxLength: 10));
            CreateIndex("dbo.JobEntities", "JobNumber", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.JobEntities", new[] { "JobNumber" });
            AlterColumn("dbo.JobEntities", "JobNumber", c => c.String(nullable: false));
        }
    }
}
