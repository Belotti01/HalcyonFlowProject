namespace HalcyonFlowProject.Data.Database.Tables {
    [Table("UserRole")]
    public class UserRole {
        
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Column("name"), Required, MaxLength(45)]
        public string Name { get; set; } = string.Empty;
    
    }
}
