namespace MPB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodanieSprawdzenia : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Login", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Login", c => c.String());
        }
    }
}
