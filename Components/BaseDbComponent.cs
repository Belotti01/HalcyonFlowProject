using Microsoft.AspNetCore.Authorization;

namespace HalcyonFlowProject.Components {
    public class BaseDbComponent<TSelf> : BaseComponent<TSelf>, IDisposable {
        [Inject]
        protected IDbContextFactory<DB>? DbContextFactory { get; set; }
        protected DB? DbContext { get; set; }

        protected override void OnInitialized() {
            base.OnInitialized();
            DbContext = DbContextFactory?.CreateDbContext();
        }

        protected async Task<bool> CheckUserPermissions(Func<UserRole, bool> roleCheck) {
            if(!await IsUserLoggedAsync()) return false;
            if(DbContext is null) return false;
            AuthenticationState? identity = await GetAuthStateAsync();
            if(identity is null) return false;

            foreach(UserRole role in DbContext.UserRoles.AsEnumerable().Where(x => roleCheck.Invoke(x))) {
                if(identity.User.IsInRole(role.Name)) {
                    return true;
                }
            }
            return false;
        }

        #region Finalization
        ~BaseDbComponent() {
            Dispose();
        }

        public override void Dispose() {
            DbContext?.Dispose();
            DbContext = null;

            base.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
