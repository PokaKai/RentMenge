namespace rent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class images5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rents", "houseimage_2", c => c.String());
            AddColumn("dbo.Rents", "houseimage_3", c => c.String());
            AddColumn("dbo.Rents", "houseimage_4", c => c.String());
            AddColumn("dbo.Rents", "houseimage_5", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rents", "houseimage_5");
            DropColumn("dbo.Rents", "houseimage_4");
            DropColumn("dbo.Rents", "houseimage_3");
            DropColumn("dbo.Rents", "houseimage_2");
        }
    }
}
