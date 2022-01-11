namespace HalcyonFlowProject.Data.Database.Tables {
    [Table("Team")]
    public class Team {
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Column("name"), Required]
        public string Name { get; set; } = string.Empty;

        public Teammate[] GetTeammates(DB dbContext) {
            return dbContext.Teammates.Where(x => x.TeamId == Id).ToArray();
        }

        public TeamAssignment[] GetAssignments(DB dbContext) {
            return dbContext.TeamAssignments.Where(x => x.TeamId == Id).ToArray();
        }
    }
}
