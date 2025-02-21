namespace Webtcgg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_ProductCategory", "Alias", c => c.String(nullable: false, maxLength: 150));
            AddColumn("dbo.tb_ProductCategory", "Icon", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_ProductCategory", "Description", c => c.String(maxLength: 150));
            AlterColumn("dbo.tb_ProductCategory", "SeoTitle", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_ProductCategory", "SeoDescripition", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_ProductCategory", "SeoKeywords", c => c.String(maxLength: 250));
            DropColumn("dbo.tb_ProductCategory", "Position");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_ProductCategory", "Position", c => c.Int(nullable: false));
            AlterColumn("dbo.tb_ProductCategory", "SeoKeywords", c => c.String());
            AlterColumn("dbo.tb_ProductCategory", "SeoDescripition", c => c.String());
            AlterColumn("dbo.tb_ProductCategory", "SeoTitle", c => c.String());
            AlterColumn("dbo.tb_ProductCategory", "Description", c => c.String());
            DropColumn("dbo.tb_ProductCategory", "Icon");
            DropColumn("dbo.tb_ProductCategory", "Alias");
        }
    }
}
