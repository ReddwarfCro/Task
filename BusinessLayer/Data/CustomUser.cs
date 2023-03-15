using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;


namespace BusinessLayer.Migrations
{
    /// <summary>
    /// Custom Migration for initial values
    /// </summary>
    [DbContext(typeof(DataContext))]
    [Migration("DefaultUser")]
    public class CustomUser : Migration
    {
        /// <summary>
        /// Method for migration
        /// </summary>
        /// <param name="migrationBuilder">Injected builder</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Users " +
                                 " (UserName, PasswordHash, Name, FamilyName) " +
                                 "VAlUES ('Ivica', CONVERT(varbinary(20), HASHBYTES('sha1', 'a')), 'Ivica', 'Vinkovic') ");
        }

        /// <summary>
        /// Method for downgrade
        /// </summary>
        /// <param name="migrationBuilder">Injected builder</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // TODO: Implemnent this how you see fit
            base.Down(migrationBuilder);
        }
    }
}