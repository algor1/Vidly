namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMembershipTypeName : DbMigration
    {
        public override void Up()
        {
            Sql("Update MemberShipTypes Set Name='Pay as you go' where Id=1" );
            Sql("Update MemberShipTypes Set Name='Monthly' where Id=2" );
            Sql("Update MemberShipTypes Set Name='Quarterly' where Id=3");
            Sql("Update MemberShipTypes Set Name='Yearly' where Id=4" );
        }
        
        public override void Down()
        {
        }
    }
}
