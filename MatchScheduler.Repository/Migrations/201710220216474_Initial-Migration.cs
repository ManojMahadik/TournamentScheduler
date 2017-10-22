namespace MatchScheduler.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        ScheduleId = c.Int(nullable: false, identity: true),
                        TournamentName = c.String(),
                    })
                .PrimaryKey(t => t.ScheduleId);
            
            CreateTable(
                "dbo.ScheduleItems",
                c => new
                    {
                        ScheduleItemId = c.Int(nullable: false, identity: true),
                        ScheduleId = c.Int(nullable: false),
                        Day = c.Int(),
                        Team1 = c.String(),
                        Team2 = c.String(),
                        IsAtHome = c.Boolean(),
                    })
                .PrimaryKey(t => t.ScheduleItemId)
                .ForeignKey("dbo.Schedules", t => t.ScheduleId, cascadeDelete: true)
                .Index(t => t.ScheduleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleItems", "ScheduleId", "dbo.Schedules");
            DropIndex("dbo.ScheduleItems", new[] { "ScheduleId" });
            DropTable("dbo.ScheduleItems");
            DropTable("dbo.Schedules");
        }
    }
}
