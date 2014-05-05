namespace SOFA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimetabledClasschanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimetabledClasses", "DisplayName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimetabledClasses", "DisplayName");
        }
    }
}
