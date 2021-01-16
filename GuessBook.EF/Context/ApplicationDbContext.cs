using Microsoft.EntityFrameworkCore;
using GuessBook.EF.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GuessBook.EF.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GuessBook.EF.Context
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Lang> Lang { get; set; }
        public virtual DbSet<MemberAnswer> MemberAnswer { get; set; }
        public virtual DbSet<Memberipdate> Memberipdate { get; set; }
        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<Options> Options { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    new ConfigurationBuilder()
                        .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"))
                        .Build()
                        .GetConnectionString("GuessBookDb")

                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Icon)
                    .HasColumnName("icon")
                    .HasMaxLength(50);

                entity.Property(e => e.Inx).HasColumnName("inx");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParentId).HasColumnName("parent_id");
            });

            modelBuilder.Entity<Lang>(entity =>
            {
                entity.HasKey(e => e.No);

                entity.ToTable("lang");

                entity.Property(e => e.No).HasColumnName("no");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MemberAnswer>(entity =>
            {
                entity.ToTable("member_answer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AnswerDigit)
                    .HasColumnName("answer_digit")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CodeDate)
                    .HasColumnName("code_date")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.OptionIds)
                    .HasColumnName("option_ids")
                    .HasMaxLength(250);

                entity.Property(e => e.QuestionId).HasColumnName("question_id");
            });

            modelBuilder.Entity<Memberipdate>(entity =>
            {
                entity.ToTable("memberipdate");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CodeDate)
                    .HasColumnName("code_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ip)
                    .HasColumnName("ip")
                    .HasMaxLength(50);

                entity.Property(e => e.Memberid).HasColumnName("memberid");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Members>(entity =>
            {
                entity.ToTable("members");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActivateCode)
                    .HasColumnName("activate_code")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CodeDate)
                    .HasColumnName("code_date")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Pwd)
                    .HasColumnName("pwd")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Options>(entity =>
            {
                entity.ToTable("options");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Photo)
                    .HasColumnName("photo")
                    .HasMaxLength(150);

                entity.Property(e => e.PhotoAlt)
                    .HasColumnName("photo_alt")
                    .HasMaxLength(150);

                entity.Property(e => e.PhotoName)
                    .HasColumnName("photo_name")
                    .HasMaxLength(150);

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.Property(e => e.Seq)
                    .HasColumnName("seq")
                    .HasComment("soru index");

                entity.Property(e => e.Text)
                    .HasColumnName("text")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Questions>(entity =>
            {
                entity.ToTable("questions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Cat1Parents)
                    .HasColumnName("cat1_parents")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType("smalldatetime")
                    .HasComment("sorunun bitişi (o tarihten sonra gösterilmeyecek)");

                entity.Property(e => e.IncrementSelect).HasColumnName("increment_select");

                entity.Property(e => e.MaxSelect).HasColumnName("max_select");

                entity.Property(e => e.MinSelect).HasColumnName("min_select");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(250);

                entity.Property(e => e.PhotoUrl)
                    .HasColumnName("photoUrl")
                    .HasMaxLength(500);

                entity.Property(e => e.QType)
                    .HasColumnName("q_type")
                    .HasComment("(Soru tipi) 1=Çoktan seçmeli fotolu, 2= Çoktan seçmeli yazı ile, 3= Çoktan çok seçmeli fotograflı, 4=Çoktan çok seçmeli yazılı, 5=Çoktan sıralı seçmeli fotograflı, 6=Çoktan sıralı seçmeli yazılı, 7=Sayılı");

                entity.Property(e => e.QTypeMax).HasColumnName("q_type_max");

                entity.Property(e => e.QTypeMin).HasColumnName("q_type_min");

                entity.Property(e => e.Qlink)
                    .HasColumnName("qlink")
                    .HasMaxLength(150);

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.SponsorIndex).HasColumnName("sponsor_index");

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasColumnType("smalldatetime")
                    .HasComment("sorunun başlangıcı (o tarihten sonra gösterime girecek)");

                entity.Property(e => e.Text)
                    .HasColumnName("text")
                    .HasMaxLength(1250)
                    .IsUnicode(false);

                entity.Property(e => e.TotalAnswer)
                    .HasColumnName("total_answer")
                    .HasComment("admin girişi yok");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.PermGroupId).HasColumnName("perm_group_id");

                entity.Property(e => e.Pwd)
                    .HasColumnName("pwd")
                    .HasMaxLength(50);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(50);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
