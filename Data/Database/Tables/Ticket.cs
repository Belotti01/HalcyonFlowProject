namespace HalcyonFlowProject.Data.Database.Tables {
    [Table("Ticket")]
    public class Ticket {
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
    }
}
