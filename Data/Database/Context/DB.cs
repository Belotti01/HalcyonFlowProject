using HalcyonFlowProject.Data.Settings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
#nullable disable

namespace HalcyonFlowProject.Data.Database.Context {
	public class DB : IdentityDbContext<User, Role, long, UserClaim, UserRole, UserLogin, RoleClaim, UserToken> {
		public ObjectContext ObjectContext => ((IObjectContextAdapter)this).ObjectContext;

		public DB(DbContextOptions<DB> options)
			: base(options) {
			Init();
		}

		public DB()
			: base() {
			Init();
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
			DatabaseSettings oldSettings = new(true);
			bool canConnect = false;
			settings.Save();
			try {
				using DB db = new();
				if (db.Database.CanConnect()) {
					canConnect = !verifyTables || db.VerifyTablesExist();
				}
			} catch { }

			oldSettings.Save();
			return canConnect;
		}

		protected void Init() {
			// Called on object initialization
		}

		// Override to be able to instantiate a DbContext at any point in the code.
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			string connectionString = new DatabaseSettings().GetConnectionString();
			optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
		}
		
		// All classes tied to tables should be here:
		public DbSet<Team> Teams { get; set; }
		public DbSet<Teammates> Teammates { get; set; }
		public DbSet<Ticket> Tickets { get; set; }
		public DbSet<ProjectTask> Tasks { get; set; }
		public DbSet<UserAssignment> UserAssignments { get; set; }
		public DbSet<TeamAssignments> TeamAssignments { get; set; }


		public bool VerifyTablesExist() {
			try {
				VerifyTableExists(Teams);
				VerifyTableExists(Teammates);
				VerifyTableExists(Tickets);
				VerifyTableExists(Tasks);
				VerifyTableExists(UserAssignments);
				VerifyTableExists(TeamAssignments);
				return true;
			} catch {
				//TODO: Handle exceptions
				return false;
            }
		}

		protected bool VerifyTableExists<TEntity>(DbSet<TEntity> table) where TEntity : class {
			try {
				_ = table.Any();
				return true;
            }catch(EntityCommandExecutionException ex) {
				if(ex.InnerException is SqlException) {
					return false;
                }else {
					throw ex;
                }
            }
		}

		#region CRUD<T>
		public IEnumerable<T> Get<T>() where T : class {
			DbSet<T> set = Set<T>();
			return set.AsEnumerable();
		}

		public IAsyncEnumerable<T> GetAsync<T>() where T : class {
			DbSet<T> set = Set<T>();
			return set.AsAsyncEnumerable();
		}

		public void Add<T>(T entity, bool save = true) where T : class {
			DbSet<T> set = Set<T>();
			set.Add(entity);
			if(save) {
				SaveChanges();
			}
		}

		public async Task AddAsync<T>(T entity, bool save = true) where T : class {
			DbSet<T> set = Set<T>();
			await set.AddAsync(entity);
			if(save) {
				await SaveChangesAsync();
			}
		}

		public void Update<T>(T entity, bool save = true) where T : class {
			Entry(entity).State = EntityState.Modified;
			if(save) {
				SaveChanges();
			}
		}

		public async Task UpdateAsync<T>(T entity, bool save = true) where T : class {
			Entry(entity).State = EntityState.Modified;
			if(save) {
				await SaveChangesAsync();
			}
		}

		public void Delete<T>(T entity, bool save = true) where T : class {
			Remove(entity);
			if(save) {
				SaveChanges();
			}
		}

		public async Task DeleteAsync<T>(T entity, bool save = true) where T : class {
			Remove(entity);
			if(save) {
				await SaveChangesAsync();
			}
		}
		#endregion
	}
}