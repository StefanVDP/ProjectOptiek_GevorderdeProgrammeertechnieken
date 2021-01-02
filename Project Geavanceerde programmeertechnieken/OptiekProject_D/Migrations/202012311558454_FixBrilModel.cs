namespace OptiekProject_D.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixBrilModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Brillen", "Model_ModelID", "dbo.Modellen");
            DropIndex("dbo.Brillen", new[] { "Model_ModelID" });
            RenameColumn(table: "dbo.Brillen", name: "Model_ModelID", newName: "ModelID");
            AlterColumn("dbo.Brillen", "ModelID", c => c.Int(nullable: false));
            CreateIndex("dbo.Brillen", "ModelID");
            AddForeignKey("dbo.Brillen", "ModelID", "dbo.Modellen", "ModelID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Brillen", "ModelID", "dbo.Modellen");
            DropIndex("dbo.Brillen", new[] { "ModelID" });
            AlterColumn("dbo.Brillen", "ModelID", c => c.Int());
            RenameColumn(table: "dbo.Brillen", name: "ModelID", newName: "Model_ModelID");
            CreateIndex("dbo.Brillen", "Model_ModelID");
            AddForeignKey("dbo.Brillen", "Model_ModelID", "dbo.Modellen", "ModelID");
        }
    }
}
