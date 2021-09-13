namespace LessonInOne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class start : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Year = c.DateTime(nullable: false),
                        YearInt = c.Int(),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Duration = c.Int(),
                        AlbomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbomId, cascadeDelete: true)
                .Index(t => t.AlbomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Songs", "AlbomId", "dbo.Albums");
            DropForeignKey("dbo.Albums", "GroupId", "dbo.Groups");
            DropIndex("dbo.Songs", new[] { "AlbomId" });
            DropIndex("dbo.Albums", new[] { "GroupId" });
            DropTable("dbo.Songs");
            DropTable("dbo.Groups");
            DropTable("dbo.Albums");
        }
    }
}
