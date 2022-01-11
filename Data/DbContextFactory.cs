namespace HalcyonFlowProject.Data {
    public class DbContextFactory : IDbContextFactory<DB> {
        protected DbContextOptions<DB> Options = new();

        public DB CreateDbContext() {
            return new DB(Options);
        }
    }
}
