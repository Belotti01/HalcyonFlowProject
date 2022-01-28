using Radzen;

namespace HalcyonFlowProject.Components {
    public class BaseComponent<TSelf> : ComponentBase, IDisposable {
        [Inject]
        protected SignInManager<User>? SignInManager { get; set; }
        [Inject]
        protected UserManager<User>? UserManager { get; set; }
        [Inject]
        protected DialogService? DialogService { get; set; }
        [Inject]
        protected TooltipService? TooltipService { get; set; }
        [Inject]
        protected ILoggerFactory? LoggerFactory { get; set; }
        [Inject]
        protected IStringLocalizer<App>? Localizer { get; set; }
        [Inject]
        protected AuthenticationStateProvider? Authentication { get; set; }

        protected ILogger<TSelf>? Logger { get; set; }


        protected override void OnInitialized() {
            base.OnInitialized();
            Logger = LoggerFactory?.CreateLogger<TSelf>();
        }

        /// <inheritdoc cref="TooltipService.Open(ElementReference, string, TooltipOptions)"/>
        protected void ShowTooltip(ElementReference element, string? content, TooltipOptions options = null!) {
            if (!string.IsNullOrWhiteSpace(content)) {
                TooltipService?.Open(element, Translate(content), options);
            }
        }

        protected string Translate(string text) {
            return Localizer?.GetString(text) ?? text;
        }

        /// <inheritdoc cref="AuthenticationStateProvider.GetAuthenticationStateAsync()"/>
        protected async Task<AuthenticationState?> GetAuthStateAsync() {
            // Getting the information everytime from this method rather than caching its result on initialization
            // is slower, but assures the validity of the authentication state received.
            return Authentication is null
                ? null
                : await Authentication.GetAuthenticationStateAsync();
        }

        protected async Task<bool> IsUserLoggedAsync() {
            return (await GetAuthStateAsync())?.User.Identity?.IsAuthenticated ?? false;
        }

        #region Finalization
        ~BaseComponent() {
            Dispose();
        }

        public virtual void Dispose() {
            Logger = null;

            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
