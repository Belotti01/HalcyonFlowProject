namespace HalcyonFlowProject.Data.Database.Tables {
    [Table("TeamAssignments")]
    public class TeamAssignments {
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Column("fk_team"), Required, ForeignKey("Teams")]
        public long TeamId { get; set; }
        [Column("fk_task"), Required, ForeignKey("Tasks")]
        public long TaskId { get; set; }
    }
}
