namespace HalcyonFlowProject.Data.Database.Tables {
    [Table("Teammates")]
    public class Teammates {
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Column("fk_user"), ForeignKey("Users"), Required]
        public long UserId { get; set; }
        [Column("fk_team"), ForeignKey("Teams"), Required]
        public long TeamId { get; set; }
        //TODO: boolean team privilegies

        public Task<Team?> GetTeamAsync(DB dbContext) {
            return dbContext.Teams.SingleOrDefaultAsync(x => x.Id == TeamId);
        }
        public Task<User?> GetUserAsync(DB dbContext) {
            return dbContext.Users.SingleOrDefaultAsync(x => x.Id == UserId);
        }
    }
}
