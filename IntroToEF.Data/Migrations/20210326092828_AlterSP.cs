using Microsoft.EntityFrameworkCore.Migrations;

namespace IntroToEF.Data.Migrations
{
    public partial class AlterSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER PROCEDURE [dbo].[SamuraisWhoSaidAWord]
               @text VARCHAR(20)
               AS
               SELECT      Samurais.Id, Samurais.Name, Samurais.Dynasty
               FROM        Samurais INNER JOIN
                           Quotes ON Samurais.Id = Quotes.SamuraiId
               WHERE      (Quotes.Text LIKE '%'+@text+'%')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[SamuraisWhoSaidAWord]");
        }
    }
}
