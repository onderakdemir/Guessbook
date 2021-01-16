using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GuessBook.EF.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    inx = table.Column<byte>(nullable: true),
                    name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    parent_id = table.Column<int>(nullable: true),
                    icon = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "lang",
                columns: table => new
                {
                    no = table.Column<byte>(nullable: false),
                    name = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lang", x => x.no);
                });

            migrationBuilder.CreateTable(
                name: "member_answer",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    member_id = table.Column<int>(nullable: true),
                    question_id = table.Column<int>(nullable: true),
                    code_date = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    option_ids = table.Column<string>(maxLength: 250, nullable: true),
                    answer_digit = table.Column<decimal>(type: "decimal(18, 0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_member_answer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "memberipdate",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    memberid = table.Column<int>(nullable: true),
                    code_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    ip = table.Column<string>(maxLength: 50, nullable: true),
                    status = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_memberipdate", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "members",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    active = table.Column<bool>(nullable: true),
                    activate_code = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    code_date = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    email = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    pwd = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    username = table.Column<string>(unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_members", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "options",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question_id = table.Column<int>(nullable: true),
                    text = table.Column<string>(maxLength: 150, nullable: true),
                    photo = table.Column<string>(maxLength: 150, nullable: true),
                    photo_alt = table.Column<string>(maxLength: 150, nullable: true),
                    photo_name = table.Column<string>(maxLength: 150, nullable: true),
                    seq = table.Column<byte>(nullable: true, comment: "soru index")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_options", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "questions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    active = table.Column<bool>(nullable: true),
                    name = table.Column<string>(maxLength: 250, nullable: true),
                    qlink = table.Column<string>(maxLength: 150, nullable: true),
                    start_time = table.Column<DateTime>(type: "smalldatetime", nullable: true, comment: "sorunun başlangıcı (o tarihten sonra gösterime girecek)"),
                    end_time = table.Column<DateTime>(type: "smalldatetime", nullable: true, comment: "sorunun bitişi (o tarihten sonra gösterilmeyecek)"),
                    category_id = table.Column<byte>(nullable: true),
                    text = table.Column<string>(unicode: false, maxLength: 1250, nullable: true),
                    q_type = table.Column<byte>(nullable: true, comment: "(Soru tipi) 1=Çoktan seçmeli fotolu, 2= Çoktan seçmeli yazı ile, 3= Çoktan çok seçmeli fotograflı, 4=Çoktan çok seçmeli yazılı, 5=Çoktan sıralı seçmeli fotograflı, 6=Çoktan sıralı seçmeli yazılı, 7=Sayılı"),
                    q_type_min = table.Column<byte>(nullable: true),
                    q_type_max = table.Column<byte>(nullable: true),
                    max_select = table.Column<double>(nullable: true),
                    min_select = table.Column<double>(nullable: true),
                    increment_select = table.Column<double>(nullable: true),
                    score = table.Column<double>(nullable: true),
                    total_answer = table.Column<int>(nullable: true, comment: "admin girişi yok"),
                    sponsor_index = table.Column<byte>(nullable: true),
                    photoUrl = table.Column<string>(maxLength: 500, nullable: true),
                    cat1_parents = table.Column<string>(fixedLength: true, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    active = table.Column<bool>(nullable: true),
                    username = table.Column<string>(maxLength: 50, nullable: true),
                    pwd = table.Column<string>(maxLength: 50, nullable: true),
                    code = table.Column<string>(maxLength: 50, nullable: true),
                    name = table.Column<string>(maxLength: 50, nullable: true),
                    title = table.Column<string>(maxLength: 50, nullable: true),
                    perm_group_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "lang");

            migrationBuilder.DropTable(
                name: "member_answer");

            migrationBuilder.DropTable(
                name: "memberipdate");

            migrationBuilder.DropTable(
                name: "members");

            migrationBuilder.DropTable(
                name: "options");

            migrationBuilder.DropTable(
                name: "questions");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
