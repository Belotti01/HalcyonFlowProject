namespace HalcyonFlowProject.Data.Database.Tables {
	[Table("Users")]
	public class User : IdentityUser<long> {
		[Column("fullname")]
		public string FullName { get; set; } = string.Empty;
		[Column("fk_role"), ForeignKey("UserRoles")]
		public long RoleId { get; set; }
		
	}
}
