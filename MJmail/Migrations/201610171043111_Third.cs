namespace MJmail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Third : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "MailFromName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "MailFromName");
        }
    }
}
