﻿namespace HalcyonFlowProject.Components {
    public class BaseDbComponent<TSelf> : BaseComponent<TSelf>, IDisposable {
        [Inject]
        protected IDbContextFactory<DB>? DbContextFactory { get; set; }
        protected DB? DbContext { get; set; }

        protected override void OnInitialized() {
            base.OnInitialized();
            DbContext = DbContextFactory?.CreateDbContext();
        }

        public override void Dispose() {
            DbContext?.Dispose();
            DbContext = null;
            base.Dispose();
        }


    }
}