namespace MJmail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fourth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "MailToName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "MailToName");
        }
    }
}
