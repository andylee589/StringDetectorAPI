namespace StringDetector.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobEntities",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        ProjectName = c.String(nullable: false, maxLength: 50),
                        JobNumber = c.String(nullable: false),
                        SourcePath = c.String(nullable: false),
                        Configuration = c.String(nullable: false),
                        Report = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Key);
            
            CreateTable(
                "dbo.JobStateEntities",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        JobKey = c.Guid(nullable: false),
                        JobStatus = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.JobEntities", t => t.JobKey, cascadeDelete: true)
                .Index(t => t.JobKey);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobStateEntities", "JobKey", "dbo.JobEntities");
            DropIndex("dbo.JobStateEntities", new[] { "JobKey" });
            DropTable("dbo.JobStateEntities");
            DropTable("dbo.JobEntities");
        }
    }
}
