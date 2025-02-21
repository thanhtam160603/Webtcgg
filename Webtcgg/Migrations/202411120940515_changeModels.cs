namespace Webtcgg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Product", "Alias", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_Product", "ProductCode", c => c.String(maxLength: 50));
            AlterColumn("dbo.tb_Product", "SeoDescripition", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_Product", "SeoKeywords", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Product", "SeoKeywords", c => c.String());
            AlterColumn("dbo.tb_Product", "SeoDescripition", c => c.String());
            AlterColumn("dbo.tb_Product", "ProductCode", c => c.Int(nullable: false));
            AlterColumn("dbo.tb_Product", "Alias", c => c.String());
        }
    }
}
