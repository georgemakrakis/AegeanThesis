namespace AegeanThesis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ThesisForms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Supervisor = c.String(),
                        NumStudents = c.String(),
                        Purpose = c.String(),
                        Description = c.String(),
                        PrereqKnowledge = c.String(),
                        AnnouncDate = c.String(),
                        AdoptionDate = c.String(),
                        FinishDate = c.String(),
                        Grade = c.Single(nullable: false),
                        Assigned = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ThesisForms");
        }
    }
}
