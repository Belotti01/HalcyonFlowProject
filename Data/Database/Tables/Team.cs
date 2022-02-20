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

		public TeamAssignments[] GetAssignments(DB dbContext) {
			return dbContext.TeamAssignments.Where(x => x.TeamId == Id).ToArray();
		}
	}
}
