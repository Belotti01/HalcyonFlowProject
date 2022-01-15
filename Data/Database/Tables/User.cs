namespace HalcyonFlowProject.Data.Database.Tables {
	[Table("aspnetusers")]
	public class User {
		[Column("id"), MaxLength(255), Key, NotNull]
		public string Id { get; set; } = string.Empty;
		[Column("UserName"), MaxLength(256)]
		public string? Username { get; set; }
		[Column("NormalizedUserName"), MaxLength(256)]
		public string? NormalizedUsername { get; set; }
		[Column("Email"), MaxLength(256)]
		public string? Email { get; set; }
		[Column("NormalizedEmail"), Required, StringLength(60)]
		public string? NormalizedEmail { get; set; }
		[Column("EmailConfirmed"), MaxLength(1), NotNull]
		public int EmailConfirmed { get; set; } = 0;
		[Column("PasswordHash"), NotNull]
		public string? PasswordHash { get; set; }
		[Column("SecurityStamp")]
		public string? SecurityStamp { get; set; }
		[Column("ConcurrencyStamp")]
		public string? ConcurrencyStamp { get; set; }
		[Column("PhoneNumber")]
		public string? PhoneNumber { get; set; }
		[Column("PhoneNumberConfirmed"), MaxLength(1), NotNull]
		public int PhoneNumberConfirmed { get; set; } = 0;
		[Column("TwoFactorEnabled"), MaxLength(1), NotNull]
		public int TwoFactorEnabled { get; set; } = 0;
		[Column("LockoutEnd"), DataType(DataType.DateTime)]
		public DateTime? LockoutEnd { get; set; }
		[Column("LockoutEnabled"), MaxLength(1), NotNull]
		public int LockoutEnabled { get; set; } = 0;
		[Column("AccessFailedCount"), NotNull]
		public int AccessFailedCount { get; set; } = 0;

		public Teammate[] GetAsTeammateAsync(DB dbContext) {
			return string.IsNullOrWhiteSpace(Id)
				? Array.Empty<Teammate>()
				: dbContext.Teammates.Where(x => x.UserId == Id).ToArray();
        }
	}
}
