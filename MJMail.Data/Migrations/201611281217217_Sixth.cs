namespace MJmail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sixth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatFriends",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Friend = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChatFriends", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ChatFriends", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ChatFriends");
        }
    }
}
