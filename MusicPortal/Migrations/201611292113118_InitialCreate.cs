namespace MusicPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ganres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GanreName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Musics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RelativePath = c.String(),
                        AbsolutePath = c.String(),
                        FileName = c.String(),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        GanersId = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ganres", t => t.GanersId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.GanersId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 70),
                        Password2 = c.String(nullable: false, maxLength: 70),
                        StatusAdmin = c.Boolean(nullable: false),
                        StatusUser = c.Boolean(nullable: false),
                        Salt_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Salts", t => t.Salt_Id)
                .Index(t => t.Salt_Id);
            
            CreateTable(
                "dbo.Salts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Salt_Id", "dbo.Salts");
            DropForeignKey("dbo.Musics", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Musics", "GanersId", "dbo.Ganres");
            DropIndex("dbo.Users", new[] { "Salt_Id" });
            DropIndex("dbo.Musics", new[] { "User_Id" });
            DropIndex("dbo.Musics", new[] { "GanersId" });
            DropTable("dbo.Salts");
            DropTable("dbo.Users");
            DropTable("dbo.Musics");
            DropTable("dbo.Ganres");
        }
    }
}
