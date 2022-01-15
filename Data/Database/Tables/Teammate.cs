namespace HalcyonFlowProject.Data.Database.Tables {
    [Table("Teammate")]
    public class Teammate {
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Column("fk_user"), ForeignKey("User"), Required]
        public string UserId { get; set; } = string.Empty;
        [Column("fk_team"), ForeignKey("Team"), Required]
        public long TeamId { get; set; }
        //TODO: boolean team privilegies

        public Task<Team?> GetTeamAsync(DB dbContext) {
            return dbContext.Teams.SingleOrDefaultAsync(x => x.Id == TeamId);
        }
        public Task<User?> GetUserAsync(DB dbContext) {
            return dbContext.UserData.SingleOrDefaultAsync(x => x.Id == UserId);
        }
    }
}
