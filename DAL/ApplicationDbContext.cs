using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using DAL.Entities;


namespace DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {


        //public ApplicationDbContext(string connectionString) : base(connectionString)
        //{
        //}

        public ApplicationDbContext() : base("name=MyDbContext")
        {

        }

        public DbSet<CityEntity> Cities => Set<CityEntity>();
        public DbSet<ConnectionTypeEntity> ConnectionTypes => Set<ConnectionTypeEntity>();
        public DbSet<IssueEntity> Issues => Set<IssueEntity>();
        public DbSet<IssuesTypeEntity> IssuesType => Set<IssuesTypeEntity>();
        public DbSet<LogEntity> Logs => Set<LogEntity>();
        public DbSet<PosEntity> Pos => Set<PosEntity>();
        public DbSet<StatusEntity> Statuses => Set<StatusEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<UserTypeEntity> UserTypes => Set<UserTypeEntity>();
		public DbSet<WeekDaysPos> WeekDaysPOS => Set<WeekDaysPos>();
        public DbSet<PriorityEntity> Priorities => Set<PriorityEntity>();



		protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //User
            modelBuilder.Entity<UserEntity>().HasRequired(u => u.UserType).WithMany(c => c.Users).HasForeignKey(u => u.UserTypeId);


            //Pos
            modelBuilder.Entity<PosEntity>().HasRequired(u => u.Cities).WithMany(c => c.Pos).HasForeignKey(u => u.City_Id);
            modelBuilder.Entity<PosEntity>().HasRequired(u => u.ConnectionType).WithMany(c => c.Pos).HasForeignKey(u => u.ConnType_Id);

            //Logs
            modelBuilder.Entity<LogEntity>().HasRequired(u => u.User).WithMany(c => c.Logs).HasForeignKey(u => u.IdUser);
            modelBuilder.Entity<LogEntity>().HasRequired(u => u.Issues).WithMany(c => c.Logs).HasForeignKey(u => u.IdIssue);


            //Issues
            modelBuilder.Entity<IssueEntity>().HasRequired(u => u.Pos).WithMany(c => c.Issues).HasForeignKey(u => u.IdPos);
            modelBuilder.Entity<IssueEntity>().HasRequired(u => u.User).WithMany(c => c.Issues).HasForeignKey(u => u.IdUserCreated);
            modelBuilder.Entity<IssueEntity>().HasRequired(u => u.UserType).WithMany(c => c.Issues).HasForeignKey(u => u.IdUserType);
            modelBuilder.Entity<IssueEntity>().HasRequired(u => u.IssuesType).WithMany(c => c.Issues).HasForeignKey(u => u.IdType);
            modelBuilder.Entity<IssueEntity>().HasRequired(u => u.Status).WithMany(c => c.Issues).HasForeignKey(u => u.IdStatus);
            modelBuilder.Entity<IssueEntity>().HasRequired(u => u.Priority).WithMany(c => c.Issues).HasForeignKey(u => u.PriorityId);


			//WrekDaysPos
			modelBuilder.Entity<WeekDaysPos>()
				.HasKey(w => w.Id);
			modelBuilder.Entity<WeekDaysPos>().Property(x => x.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
			modelBuilder.Entity<WeekDaysPos>().HasRequired(u => u.PosEntity).WithMany(c => c.WeekDaysPos).HasForeignKey(u => u.IdPos);


   



		}


	} 

}
