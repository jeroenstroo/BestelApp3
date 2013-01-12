namespace BestelApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Productens", "Categorie_CategorieId", "dbo.Categories");
            DropIndex("dbo.Productens", new[] { "Categorie_CategorieId" });
            RenameColumn(table: "dbo.Productens", name: "Categorie_CategorieId", newName: "Categorie_CategorieNaam");
            DropPrimaryKey("dbo.Productens", new[] { "ProductId" });
            AddPrimaryKey("dbo.Productens", "ProductNaam");
            DropPrimaryKey("dbo.Categories", new[] { "CategorieId" });
            AddPrimaryKey("dbo.Categories", "CategorieNaam");
            AddForeignKey("dbo.Productens", "Categorie_CategorieNaam", "dbo.Categories", "CategorieNaam");
            CreateIndex("dbo.Productens", "Categorie_CategorieNaam");
            //DropColumn("dbo.Productens", "ProductId");
            //DropColumn("dbo.Categories", "CategorieId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "CategorieId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Productens", "ProductId", c => c.Int(nullable: false, identity: true));
            DropIndex("dbo.Productens", new[] { "Categorie_CategorieNaam" });
            DropForeignKey("dbo.Productens", "Categorie_CategorieNaam", "dbo.Categories");
            DropPrimaryKey("dbo.Categories", new[] { "CategorieNaam" });
            AddPrimaryKey("dbo.Categories", "CategorieId");
            DropPrimaryKey("dbo.Productens", new[] { "ProductNaam" });
            AddPrimaryKey("dbo.Productens", "ProductId");
            RenameColumn(table: "dbo.Productens", name: "Categorie_CategorieNaam", newName: "Categorie_CategorieId");
            CreateIndex("dbo.Productens", "Categorie_CategorieId");
            AddForeignKey("dbo.Productens", "Categorie_CategorieId", "dbo.Categories", "CategorieId");
        }
    }
}
