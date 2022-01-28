namespace HalcyonFlowProject.Data.Database.Tables {
    [Table("Roles")]
    public class Role : IdentityRole<long> {
        [Column("is_default_role")]
        public bool IsDefault { get; set; }


        [Column("is_administrator")]
        public bool IsAdministrator { get; set; }
        [Column("is_manager")]
        public bool IsManager { get; set; }
        [Column("can_create_projects")]
        public bool CanCreateProjects { get; set; }
        [Column("can_create_tasks")]
        public bool CanCreateTasks { get; set; }
        [Column("can_create_tickets")]
        public bool CanCreateTickets { get; set; }
        [Column("can_assign_tasks")]
        public bool CanAssignTasks { get; set; }
        [Column("can_comment")]
        public bool CanComment { get; set; } = true;
    }
}
