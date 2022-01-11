namespace HalcyonFlowProject.Data.Database.Tables {
	[Table("User")]
	public class User {
		[Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }

		[ForeignKey("aspnetusers"), Column("fk_aspnetusers"), StringLength(255), Required]
		public string AspNetUserId { get; set; }

		[Column("username"), StringLength(45)]
		public string Username { get; set; }
		[Column("fullname"), Required, StringLength(60)]
		public string Fullname { get; set; }

		public async Task<Teammate[]> GetAsTeammateAsync(DB dbContext) {
			return dbContext.Teammates.Where(x => x.UserId == Id).ToArray();
        }
	}
}
