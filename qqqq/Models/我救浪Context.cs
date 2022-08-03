using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace qqqq.Models
{
    public partial class 我救浪Context : DbContext
    {
        public 我救浪Context()
        {
        }

        public 我救浪Context(DbContextOptions<我救浪Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Age> Ages { get; set; }
        public virtual DbSet<AggregatedCounter> AggregatedCounters { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<CommentResponse> CommentResponses { get; set; }
        public virtual DbSet<Counter> Counters { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Hash> Hashes { get; set; }
        public virtual DbSet<Hgender> Hgenders { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<JobParameter> JobParameters { get; set; }
        public virtual DbSet<JobQueue> JobQueues { get; set; }
        public virtual DbSet<Ligation> Ligations { get; set; }
        public virtual DbSet<List> Lists { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MemberComment> MemberComments { get; set; }
        public virtual DbSet<MemberWish> MemberWishes { get; set; }
        public virtual DbSet<MsgEmpAndMem> MsgEmpAndMems { get; set; }
        public virtual DbSet<MsgEmpToEmp> MsgEmpToEmps { get; set; }
        public virtual DbSet<MyFavorite> MyFavorites { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<PetDetail> PetDetails { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Schema> Schemas { get; set; }
        public virtual DbSet<Server> Servers { get; set; }
        public virtual DbSet<Set> Sets { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Vactivity> Vactivities { get; set; }
        public virtual DbSet<VactivityCategory> VactivityCategories { get; set; }
        public virtual DbSet<VallowTime> VallowTimes { get; set; }
        public virtual DbSet<Volunteer> Volunteers { get; set; }
        public virtual DbSet<Vstatus> Vstatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=我救浪;Integrated Security=True");
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Data Source=.;Initial Catalog=我救浪;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<Age>(entity =>
            {
                entity.ToTable("Age");

                entity.Property(e => e.AgeId).HasColumnName("AgeID");

                entity.Property(e => e.AgeType).HasMaxLength(50);
            });

            modelBuilder.Entity<AggregatedCounter>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PK_HangFire_CounterAggregated");

                entity.ToTable("AggregatedCounter", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_AggregatedCounter_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CityName).HasMaxLength(10);
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("Color");

                entity.Property(e => e.ColorId).HasColumnName("ColorID");

                entity.Property(e => e.ColorName).HasMaxLength(50);
            });

            modelBuilder.Entity<CommentResponse>(entity =>
            {
                entity.HasKey(e => e.ResponseId);

                entity.ToTable("CommentResponse");

                entity.Property(e => e.ResponseId).HasColumnName("ResponseID");

                entity.Property(e => e.CommentDate).HasColumnType("datetime");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.CommentResponses)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK_CommentResponse_Member_Comment");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.CommentResponses)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_CommentResponse_Employee1");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.CommentResponses)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_CommentResponse_Member");
            });

            modelBuilder.Entity<Counter>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Counter", "HangFire");

                entity.HasIndex(e => e.Key, "CX_HangFire_Counter")
                    .IsClustered();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpoyeeId);

                entity.ToTable("Employee");

                entity.Property(e => e.EmpoyeeId).HasColumnName("EmpoyeeID");

                entity.Property(e => e.Name).HasMaxLength(10);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender");

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.GenderType).HasMaxLength(50);
            });

            modelBuilder.Entity<Hash>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Field })
                    .HasName("PK_HangFire_Hash");

                entity.ToTable("Hash", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Hash_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Field).HasMaxLength(100);
            });

            modelBuilder.Entity<Hgender>(entity =>
            {
                entity.ToTable("HGender");

                entity.Property(e => e.HgenderId).HasColumnName("HGenderID");

                entity.Property(e => e.GenderType).HasMaxLength(50);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Job_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.HasIndex(e => e.StateName, "IX_HangFire_Job_StateName")
                    .HasFilter("([StateName] IS NOT NULL)");

                entity.Property(e => e.Arguments).IsRequired();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.InvocationData).IsRequired();

                entity.Property(e => e.StateName).HasMaxLength(20);
            });

            modelBuilder.Entity<JobParameter>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Name })
                    .HasName("PK_HangFire_JobParameter");

                entity.ToTable("JobParameter", "HangFire");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobParameters)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_JobParameter_Job");
            });

            modelBuilder.Entity<JobQueue>(entity =>
            {
                entity.HasKey(e => new { e.Queue, e.Id })
                    .HasName("PK_HangFire_JobQueue");

                entity.ToTable("JobQueue", "HangFire");

                entity.Property(e => e.Queue).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FetchedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Ligation>(entity =>
            {
                entity.ToTable("Ligation");

                entity.Property(e => e.LigationId).HasColumnName("LigationID");

                entity.Property(e => e.LigationType).HasMaxLength(50);
            });

            modelBuilder.Entity<List>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Id })
                    .HasName("PK_HangFire_List");

                entity.ToTable("List", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_List_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.BirthdayDate).HasColumnType("date");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.HgenderId).HasColumnName("HGenderID");

                entity.Property(e => e.MemberPhone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Photo).HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Member_City");

                entity.HasOne(d => d.Hgender)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.HgenderId)
                    .HasConstraintName("FK_Member_HGender");
            });

            modelBuilder.Entity<MemberComment>(entity =>
            {
                entity.HasKey(e => e.CommentId);

                entity.ToTable("Member_Comment");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.CommentDate).HasColumnType("date");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberComments)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Member_Comment_Member");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.MemberComments)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Member_Comment_Product");
            });

            modelBuilder.Entity<MemberWish>(entity =>
            {
                entity.HasKey(e => e.MemberId)
                    .HasName("PK_Member_Adopt");

                entity.ToTable("Member_Wish");

                entity.Property(e => e.MemberId)
                    .ValueGeneratedNever()
                    .HasColumnName("MemberID");

                entity.Property(e => e.AgeId).HasColumnName("AgeID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.ColorId).HasColumnName("ColorID");

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.LigationId).HasColumnName("LigationID");

                entity.Property(e => e.SizeId).HasColumnName("SizeID");

                entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

                entity.Property(e => e.YearCost).HasColumnType("money");

                entity.HasOne(d => d.Age)
                    .WithMany(p => p.MemberWishes)
                    .HasForeignKey(d => d.AgeId)
                    .HasConstraintName("FK_Member_Wish_Age");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.MemberWishes)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Member_Wish_Category");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.MemberWishes)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Member_Adopt_City");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.MemberWishes)
                    .HasForeignKey(d => d.ColorId)
                    .HasConstraintName("FK_Member_Wish_Color");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.MemberWishes)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("FK_Member_Wish_Gender");

                entity.HasOne(d => d.Ligation)
                    .WithMany(p => p.MemberWishes)
                    .HasForeignKey(d => d.LigationId)
                    .HasConstraintName("FK_Member_Wish_Ligation");

                entity.HasOne(d => d.Member)
                    .WithOne(p => p.MemberWish)
                    .HasForeignKey<MemberWish>(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_Adopt_Member");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.MemberWishes)
                    .HasForeignKey(d => d.SizeId)
                    .HasConstraintName("FK_Member_Wish_Size");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.MemberWishes)
                    .HasForeignKey(d => d.SubCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_Wish_SubCategory");
            });

            modelBuilder.Entity<MsgEmpAndMem>(entity =>
            {
                entity.HasKey(e => e.MsgEmpAndMem1);

                entity.ToTable("MsgEmpAndMem");

                entity.Property(e => e.MsgEmpAndMem1).HasColumnName("MsgEmpAndMem");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.MsgTime).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.MsgEmpAndMems)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_MsgEmpAndMem_Employee");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MsgEmpAndMems)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_MsgEmpAndMem_Member");
            });

            modelBuilder.Entity<MsgEmpToEmp>(entity =>
            {
                entity.ToTable("MsgEmpToEmp");

                entity.Property(e => e.MsgEmpToEmpId).HasColumnName("MsgEmpToEmpID");

                entity.Property(e => e.EmpReceiveId).HasColumnName("EmpReceiveID");

                entity.Property(e => e.EmpSendId).HasColumnName("EmpSendID");

                entity.Property(e => e.MsgTime).HasColumnType("datetime");

                entity.HasOne(d => d.EmpReceive)
                    .WithMany(p => p.MsgEmpToEmpEmpReceives)
                    .HasForeignKey(d => d.EmpReceiveId)
                    .HasConstraintName("FK_MsgEmpToEmp_Employee2");

                entity.HasOne(d => d.EmpSend)
                    .WithMany(p => p.MsgEmpToEmpEmpSends)
                    .HasForeignKey(d => d.EmpSendId)
                    .HasConstraintName("FK_MsgEmpToEmp_Employee1");
            });

            modelBuilder.Entity<MyFavorite>(entity =>
            {
                entity.HasKey(e => e.MyFavorite1)
                    .HasName("PK_MyFavorite_1");

                entity.ToTable("MyFavorite");

                entity.Property(e => e.MyFavorite1).HasColumnName("MyFavorite");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MyFavorites)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MyFavorite_Member1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.MyFavorites)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MyFavorite_Product1");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.OrderStatusId).HasColumnName("Order_StatusID");

                entity.Property(e => e.SendAddress).HasMaxLength(50);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Orders_Employee");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Orders_Member");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderStatusId)
                    .HasConstraintName("FK_Orders_Order_Status");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId, e.IsDonate })
                    .HasName("PK_Order_Detail_1");

                entity.ToTable("Order_Detail");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Detail_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Detail_Product");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("Order_Status");

                entity.Property(e => e.OrderStatusId).HasColumnName("Order_StatusID");

                entity.Property(e => e.StatusType).HasMaxLength(50);
            });

            modelBuilder.Entity<PetDetail>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK_AdoptionID");

                entity.ToTable("Pet_Detail");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProductID");

                entity.Property(e => e.AgeId).HasColumnName("AgeID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.ColorId).HasColumnName("ColorID");

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.LigationId).HasColumnName("LigationID");

                entity.Property(e => e.SizeId).HasColumnName("SizeID");

                entity.Property(e => e.YearCost).HasColumnType("money");

                entity.HasOne(d => d.Age)
                    .WithMany(p => p.PetDetails)
                    .HasForeignKey(d => d.AgeId)
                    .HasConstraintName("FK_Pet_Detail_Age");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.PetDetails)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Pet_Detail_City");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.PetDetails)
                    .HasForeignKey(d => d.ColorId)
                    .HasConstraintName("FK_Pet_Detail_Color");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.PetDetails)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("FK_Pet_Detail_Gender");

                entity.HasOne(d => d.Ligation)
                    .WithMany(p => p.PetDetails)
                    .HasForeignKey(d => d.LigationId)
                    .HasConstraintName("FK_Pet_Detail_Ligation");

                entity.HasOne(d => d.Product)
                    .WithOne(p => p.PetDetail)
                    .HasForeignKey<PetDetail>(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AdoptionID_Product");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.PetDetails)
                    .HasForeignKey(d => d.SizeId)
                    .HasConstraintName("FK_Pet_Detail_Size");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.HasKey(e => e.PictureId);

                entity.ToTable("Photo");

                entity.Property(e => e.PictureId).HasColumnName("PictureID");

                entity.Property(e => e.Picture).HasColumnType("image");

                entity.Property(e => e.PictureName).HasMaxLength(50);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Photos)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Photo_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ProductName).HasMaxLength(50);

                entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SubCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_SubCategory");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Product_Supplier");
            });

            modelBuilder.Entity<Schema>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("PK_HangFire_Schema");

                entity.ToTable("Schema", "HangFire");

                entity.Property(e => e.Version).ValueGeneratedNever();
            });

            modelBuilder.Entity<Server>(entity =>
            {
                entity.ToTable("Server", "HangFire");

                entity.HasIndex(e => e.LastHeartbeat, "IX_HangFire_Server_LastHeartbeat");

                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.LastHeartbeat).HasColumnType("datetime");
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Value })
                    .HasName("PK_HangFire_Set");

                entity.ToTable("Set", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Set_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.HasIndex(e => new { e.Key, e.Score }, "IX_HangFire_Set_Score");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(256);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("Size");

                entity.Property(e => e.SizeId).HasColumnName("SizeID");

                entity.Property(e => e.SizeType).HasMaxLength(50);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Id })
                    .HasName("PK_HangFire_State");

                entity.ToTable("State", "HangFire");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_State_Job");
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.ToTable("SubCategory");

                entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.SubCategoryName).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubCategory_Category");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Supplier_Employee");
            });

            modelBuilder.Entity<Vactivity>(entity =>
            {
                entity.HasKey(e => e.ActivityId)
                    .HasName("PK_ActivityTable");

                entity.ToTable("VActivity");

                entity.Property(e => e.ActivityId).HasColumnName("ActivityID");

                entity.Property(e => e.ActivityCategoryId).HasColumnName("ActivityCategoryID");

                entity.Property(e => e.ActivityPhoto).HasMaxLength(50);

                entity.Property(e => e.EndDate).HasMaxLength(50);

                entity.Property(e => e.StartDate).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.ActivityCategory)
                    .WithMany(p => p.Vactivities)
                    .HasForeignKey(d => d.ActivityCategoryId)
                    .HasConstraintName("FK_VActivity_VActivityCategory");
            });

            modelBuilder.Entity<VactivityCategory>(entity =>
            {
                entity.HasKey(e => e.ActivityCategoryId)
                    .HasName("PK_ActivityCategory");

                entity.ToTable("VActivityCategory");

                entity.Property(e => e.ActivityCategoryId).HasColumnName("ActivityCategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<VallowTime>(entity =>
            {
                entity.HasKey(e => e.AllowTimeId)
                    .HasName("PK_AllowTime");

                entity.ToTable("VAllowTime");

                entity.Property(e => e.AllowTimeId).HasColumnName("AllowTimeID");

                entity.Property(e => e.TimeRange).HasMaxLength(50);
            });

            modelBuilder.Entity<Volunteer>(entity =>
            {
                entity.ToTable("Volunteer");

                entity.Property(e => e.VolunteerId).HasColumnName("VolunteerID");

                entity.Property(e => e.ActivityId).HasColumnName("ActivityID");

                entity.Property(e => e.AllowDate).HasMaxLength(50);

                entity.Property(e => e.AllowTimeId).HasColumnName("AllowTimeID");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.OrderDate).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.VerificationCode).HasMaxLength(50);

                entity.Property(e => e.VstatusId).HasColumnName("VStatusID");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.Volunteers)
                    .HasForeignKey(d => d.ActivityId)
                    .HasConstraintName("FK_Volunteer_VActivity");

                entity.HasOne(d => d.AllowTime)
                    .WithMany(p => p.Volunteers)
                    .HasForeignKey(d => d.AllowTimeId)
                    .HasConstraintName("FK_Volunteer_VAllowTime");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Volunteers)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Volunteer_Member");

                entity.HasOne(d => d.Vstatus)
                    .WithMany(p => p.Volunteers)
                    .HasForeignKey(d => d.VstatusId)
                    .HasConstraintName("FK_Volunteer_VStatus");
            });

            modelBuilder.Entity<Vstatus>(entity =>
            {
                entity.ToTable("VStatus");

                entity.Property(e => e.VstatusId).HasColumnName("VStatusID");

                entity.Property(e => e.StatusType)
                    .HasMaxLength(50)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
