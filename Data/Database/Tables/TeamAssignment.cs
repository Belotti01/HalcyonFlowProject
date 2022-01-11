namespace HalcyonFlowProject.Data.Database.Tables {
    [Table("TeamAssignment")]
    public class TeamAssignment {
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Column("fk_team"), Required, ForeignKey("Team")]
        public long TeamId { get; set; }
        [Column("fk_task"), Required, ForeignKey("Task")]
        public long TaskId { get; set; }
    }
}
