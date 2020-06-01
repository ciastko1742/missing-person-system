namespace MPB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MissingPersons",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Age = c.Int(nullable: false),
                        Location = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Role = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MissingPersons", "UserId", "dbo.Users");
            DropIndex("dbo.MissingPersons", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.MissingPersons");
        }
    }
}
