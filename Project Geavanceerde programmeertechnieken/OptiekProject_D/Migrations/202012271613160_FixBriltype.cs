namespace OptiekProject_D.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixBriltype : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Brillen", "BriltypeID", "dbo.BrilTypes");
            DropForeignKey("dbo.Brillen", "ModelID", "dbo.Modellen");
            DropIndex("dbo.Brillen", new[] { "ModelID" });
            DropIndex("dbo.Brillen", new[] { "BriltypeID" });
            RenameColumn(table: "dbo.Brillen", name: "ModelID", newName: "Model_ModelID");
            AddColumn("dbo.Modellen", "BriltypeID", c => c.Int(nullable: false));
            AlterColumn("dbo.Brillen", "Model_ModelID", c => c.Int());
            CreateIndex("dbo.Brillen", "Model_ModelID");
            CreateIndex("dbo.Modellen", "BriltypeID");
            AddForeignKey("dbo.Modellen", "BriltypeID", "dbo.BrilTypes", "BriltypeID", cascadeDelete: true);
            AddForeignKey("dbo.Brillen", "Model_ModelID", "dbo.Modellen", "ModelID");
            DropColumn("dbo.Brillen", "BriltypeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Brillen", "BriltypeID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Brillen", "Model_ModelID", "dbo.Modellen");
            DropForeignKey("dbo.Modellen", "BriltypeID", "dbo.BrilTypes");
            DropIndex("dbo.Modellen", new[] { "BriltypeID" });
            DropIndex("dbo.Brillen", new[] { "Model_ModelID" });
            AlterColumn("dbo.Brillen", "Model_ModelID", c => c.Int(nullable: false));
            DropColumn("dbo.Modellen", "BriltypeID");
            RenameColumn(table: "dbo.Brillen", name: "Model_ModelID", newName: "ModelID");
            CreateIndex("dbo.Brillen", "BriltypeID");
            CreateIndex("dbo.Brillen", "ModelID");
            AddForeignKey("dbo.Brillen", "ModelID", "dbo.Modellen", "ModelID", cascadeDelete: true);
            AddForeignKey("dbo.Brillen", "BriltypeID", "dbo.BrilTypes", "BriltypeID", cascadeDelete: true);
        }
    }
}
