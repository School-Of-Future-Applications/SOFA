namespace SOFA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LineTimeChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LineTimes", "StartTime", c => c.Double(nullable: false));
            AddColumn("dbo.LineTimes", "EndTime", c => c.Double(nullable: false));
            DropColumn("dbo.LineTimes", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LineTimes", "Time", c => c.Double(nullable: false));
            DropColumn("dbo.LineTimes", "EndTime");
            DropColumn("dbo.LineTimes", "StartTime");
        }
    }
}
