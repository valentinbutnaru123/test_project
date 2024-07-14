namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWeekTable : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.WeekDaysPos",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					IdPos = c.Int(nullable: false),
					WeekDays = c.String(),
				})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.PosEntities", t => t.IdPos, cascadeDelete: true)
				.Index(t => t.IdPos);

		}

		public override void Down()
		{
			DropIndex("dbo.WeekDaysPos", new[] { "IdPos" });
			DropForeignKey("dbo.WeekDaysPos", "IdPos", "dbo.PosEntities");
			DropTable("dbo.WeekDaysPos");
		}

	}
}
