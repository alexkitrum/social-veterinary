namespace SocialVeterinary.Migrations
{
    using FluentMigrator;

    [Migration(2020_03_31_12_00_00)]
    public class InitialMigration : Migration
    {
        public override void Up()
        {
            Create.Table("persons")
                .WithColumn("id").AsInt64().Identity().PrimaryKey()
                .WithColumn("name").AsString(256).Unique()
                .WithColumn("last_name").AsString(256).NotNullable()
                .WithColumn("is_employee").AsBoolean();


            Create.Table("pets")
                .WithColumn("id").AsInt64().Identity().PrimaryKey()
                .WithColumn("person_id").AsInt64()
                    .ForeignKey("FK_pets_persons", "public", "persons", "id").OnDelete(System.Data.Rule.Cascade)
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("type").AsInt16();
        }

        public override void Down()
        {
            Delete.Table("pets");
            Delete.Table("persons");
        }
    }
}
