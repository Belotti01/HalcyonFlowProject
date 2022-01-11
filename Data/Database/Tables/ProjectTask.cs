namespace HalcyonFlowProject.Data.Database.Tables {
	[Table("Task")]
	public class ProjectTask {
		[Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }

		[Column("fk_creator"), ForeignKey("User"), Required]
		public long CreatorId { get; set; }
		[Column("fk_ticket"), ForeignKey("Ticket")]
		public long TicketId { get; set; } = -1;

		[Column("title"), MinLength(1), MaxLength(255), Required]
		public string Title { get; set; } = string.Empty;
		[Column("description"), DataType(DataType.Text), Required]
		public string Description { get; set; } = string.Empty;

		public ValueTask<User?> GetCreatorAsync(DB dbContext) {
			return dbContext.UserData.FindAsync(CreatorId);
        }

		public ValueTask<Ticket?> GetSourceTicketAsync(DB dbContext) {
			return TicketId < 0
				? ValueTask.FromResult<Ticket?>(null)
				: dbContext.Tickets.FindAsync(TicketId);
        }
	}
}
