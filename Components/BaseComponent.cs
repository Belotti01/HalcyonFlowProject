using Radzen;

namespace HalcyonFlowProject.Components {
    public class BaseComponent<TSelf> : ComponentBase, IDisposable {
        [Inject]
        protected DialogService? DialogService { get; set; }
        [Inject]
        protected TooltipService? TooltipService { get; set; }
        [Inject]
        protected ILoggerFactory? LoggerFactory { get; set; }
        [Inject]
        protected IStringLocalizer<App>? Localizer { get; set; }

        protected ILogger<TSelf>? Logger { get; set; }


        protected override void OnInitialized() {
            base.OnInitialized();
            Logger = LoggerFactory?.CreateLogger<TSelf>();
        }

        protected void ShowTooltip(ElementReference element, string? content, TooltipOptions options = null!) {
            if (!string.IsNullOrWhiteSpace(content)) {
                TooltipService?.Open(element, Translate(content), options);
            }
        }

        protected string Translate(string text) {
            return Localizer?.GetString(text) ?? text;
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
