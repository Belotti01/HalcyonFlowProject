using HalcyonFlowProject.Data.Settings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Data.Entity.Core;
using System.Data.SqlClient;

#nullable disable

namespace HalcyonFlowProject.Data.Database.Context {
	public class DB : IdentityDbContext<User, Role, long, UserClaim, UserRole, UserLogin, RoleClaim, UserToken> {
		protected bool IsMockObject = false;

		public DB(DbContextOptions<DB> options, bool useTestConfig = false)
			: base(options) {
			IsMockObject = useTestConfig;
		}

		public DB(bool useTestConfig = false)
			: base() {
			IsMockObject = useTestConfig;
		}

		/// <summary>
		///		Check that the information stored in the <paramref name="settings"/> object
		///		allows for a connection to the Database. Optionally check for the presence of
		///		all tables.
		/// </summary>
		/// <param name="settings">
		///		The settings to test the connection for.
		/// </param>
		/// <param name="verifyTables">
		///		Whether to also check that all required tables are available.
		/// </param>
		/// <returns>
		///		<see langword="true"/> if a connection is establishable and the database
		///		exists, <see langword="false"/> otherwise.
		/// </returns>
		public static bool CanConnect(DatabaseSettings settings, bool verifyTables) {
			bool canConnect = false;
			bool saved = false;

			settings.Save(ConfigFile.DatabaseTesting, () => saved = true);
			// Wait for the file to end saving
			while(!saved) Thread.Sleep(10);

			try {
				using DB db = new(true);
				canConnect = db.Database.CanConnect()
					&& (!verifyTables || db.VerifyTablesExist());
			} catch { }

			return canConnect;
		}

		// Overrided to be able to instantiate a DbContext at any point in the code without supplying the connection string.
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			DatabaseSettings settings = new(false);
			settings.Load(IsMockObject ? ConfigFile.DatabaseTesting : ConfigFile.Database);
			string connectionString = settings.GetConnectionString();
			// Throws an exception if the login info is invalid. This will result in the database settings
			// input window appearing, so don't catch it.
			optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
		}
		
		// All classes tied to tables should be here:
		public DbSet<Team> Teams { get; set; }
		public DbSet<Teammates> Teammates { get; set; }
		public DbSet<Ticket> Tickets { get; set; }
		public DbSet<ProjectTask> Tasks { get; set; }
		public DbSet<UserAssignment> UserAssignments { get; set; }
		public DbSet<TeamAssignments> TeamAssignments { get; set; }

		/// <summary>
		/// Checks whether all the required database tables are present in the database.
		/// </summary>
		/// <returns><see langword="true"/> if the tables exist in the database, <see langword="false"/> otherwise.</returns>
		public bool VerifyTablesExist() {
			return VerifyTableExists(Users)
			&& VerifyTableExists(Teams)
			&& VerifyTableExists(Tasks)
			&& VerifyTableExists(Tickets)
			&& VerifyTableExists(Teammates)
			&& VerifyTableExists(UserRoles)
			&& VerifyTableExists(UserAssignments)
			&& VerifyTableExists(TeamAssignments);
		}

		protected bool VerifyTableExists<TEntity>(DbSet<TEntity> table) where TEntity : class {
			try {
				_ = table.Any();
				return true;
            }catch(Exception ex) {
#if DEBUG
				if(ex is EntityCommandExecutionException entityException) {
					// SqlException is thrown if the table does not exist.
					return entityException.InnerException is SqlException
						? false
						: throw entityException;
				}else {
					// Parameterless throw avoids changing the stacktrace of the exception that would be re-thrown.
					throw; // Check ex
                }
#else
				return false;
#endif
            }
		}

		
    }
}