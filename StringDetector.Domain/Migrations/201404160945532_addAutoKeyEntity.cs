namespace StringDetector.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAutoKeyEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AutoGenerateKeyEntities",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        NextKey = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Key);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AutoGenerateKeyEntities");
        }
    }
}
