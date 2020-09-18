using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStoreApplication.Migrations
{
    public partial class FirstCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: false),
                    UserFirstName = table.Column<string>(nullable: false),
                    UserLastName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(nullable: false),
                    BookAuthor = table.Column<string>(nullable: false),
                    BookPublisher = table.Column<string>(nullable: true),
                    BookPrice = table.Column<double>(nullable: false),
                    BookEnrollUserUserId = table.Column<int>(nullable: true),
                    BookSubscriptionUserId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    Stock = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_Users_BookEnrollUserUserId",
                        column: x => x.BookEnrollUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_Users_BookSubscriptionUserId",
                        column: x => x.BookSubscriptionUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookEnrollUserUserId",
                table: "Books",
                column: "BookEnrollUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookSubscriptionUserId",
                table: "Books",
                column: "BookSubscriptionUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
