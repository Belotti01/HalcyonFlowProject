namespace HalcyonFlowProject.Data.Database.Tables {
	[Table("Teammates")]
	public class Teammates {
		[Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }
		[Column("fk_user"), ForeignKey("Users"), Required]
		public long UserId { get; set; }
		[Column("fk_team"), ForeignKey("Teams"), Required]
		public long TeamId { get; set; }
		[Column("Role")]
		public TeammateRole Role { get; set; } = TeammateRole.User;

		public Task<Team?> GetTeamAsync(DB dbContext) {
			return dbContext.Teams.SingleOrDefaultAsync(x => x.Id == TeamId);
		}
		public Task<User?> GetUserAsync(DB dbContext) {
			return dbContext.Users.SingleOrDefaultAsync(x => x.Id == UserId);
		}
	}

	public enum TeammateRole {
		/// <summary>Basic user. Has no team management privileges.</summary>
		User,
		/// <summary>Can add and remove users from the team.</summary>
		Manager,
		/// <summary>Can add and remove users from the team, and edit the role of other teammates.</summary>
		Leader
	}
}
