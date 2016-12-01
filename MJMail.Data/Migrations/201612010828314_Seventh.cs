namespace MJmail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seventh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChatFriends", "FriendUserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChatFriends", "FriendUserName");
        }
    }
}
