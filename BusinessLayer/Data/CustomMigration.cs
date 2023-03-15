using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;


namespace BusinessLayer.Migrations
{
    /// <summary>
    /// Custom Migration for initial values
    /// </summary>
    [DbContext(typeof(DataContext))]
    [Migration("DefaultAddress")]
    public class CustomMigration : Migration
    {
        /// <summary>
        /// Method for migration
        /// </summary>
        /// <param name="migrationBuilder">Injected builder</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Address " +
                                 " (STREET, NUMBER, ZIPCODE, CITY) " +
                                 "VAlUES ('Default', '1A', '10000', 'Zagreb') ");
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