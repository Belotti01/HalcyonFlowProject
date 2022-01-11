using HalcyonFlowProject.Data.Settings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
#nullable disable

namespace HalcyonFlowProject.Data {
	public class DB : IdentityDbContext {
		public DB(DbContextOptions<DB> options)
			: base(options) {
			Init();
		}

		public DB()
			: base() {
			Init();
		}

		public static bool CanConnect(DatabaseSettings settings) {
			DatabaseSettings oldSettings = new(true);
			bool canConnect = false;
			settings.Save();
			try {
				using DB db = new();
				if (db.Database.CanConnect()) {
					canConnect = true;
				}
			} catch { }

			oldSettings.Save();
			return canConnect;
		}

		protected void Init() {

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			string connectionString = new DatabaseSettings().GetConnectionString();
			optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
		}

		public DbSet<User> UserData { get; set; }
		public DbSet<UserRole> UserDataRole { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<Teammate> Teammates { get; set; }
		public DbSet<Ticket> Tickets { get; set; }
		public DbSet<ProjectTask> Tasks { get; set; }
		public DbSet<UserAssignment> UserAssignments { get; set; }
		public DbSet<TeamAssignment> TeamAssignments { get; set; }
	}
}