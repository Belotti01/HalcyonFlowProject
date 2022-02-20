namespace HalcyonFlowProject.Data.Database.Tables {
	[Table("UserAssignments")]
	public class UserAssignment {
		[Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }
		[Column("fk_user"), Required, ForeignKey("Teams")]
		public long UserId { get; set; }
		[Column("fk_task"), Required, ForeignKey("Tasks")]
		public long TaskId { get; set; }

		[Column("comment"), Required, DataType(DataType.Text)]
		public string Comment { get; set; } = string.Empty;
		[Column("assignment_date"), Required]
		public DateTime AssignmentDate { get; set; } = DateTime.Now.ToUniversalTime();
		[Column("due_date")]
		public DateTime DueDate { get; set; } = default;

		public ValueTask<User?> GetUserAsync(DB dbContext) {
			return dbContext.Users.FindAsync(UserId);
		}

		public ValueTask<ProjectTask?> GetTaskAsync(DB dbContext) {
			return dbContext.Tasks.FindAsync(TaskId);
		}
	}
}
