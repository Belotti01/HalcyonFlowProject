namespace HalcyonFlowProject.Data.Database.Tables {
	[Table("Teams")]
	public class Team {
		[Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }
		[Column("name"), Required]
		public string Name { get; set; } = string.Empty;

		public Teammates[] GetTeammates(DB dbContext) {
			return dbContext.Teammates.Where(x => x.TeamId == Id).ToArray();
		}

		public User?[] GetMembers(DB dbContext) {
			return GetTeammates(dbContext)
				.Select(x => {
					Task<User?> t = x.GetUserAsync(dbContext);
					t.Wait();
					return t.Result;
				})
				.ToArray();				
		}

		public TeamAssignments[] GetAssignments(DB dbContext) {
			return dbContext.TeamAssignments.Where(x => x.TeamId == Id).ToArray();
		}
	}
}
