using HalcyonFlowProject.Areas.Identity;
using HalcyonFlowProject.Data.Settings;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Radzen;

// *** From the default Blazor template, reformatted to be readable *** //

namespace HalcyonFlowProject {

	public static class Program {
		public static WebApplication? App { get; private set; }

		public static void Main(string[] args) {
			WebApplicationBuilder builder;

			builder = WebApplication.CreateBuilder(args);
			// Setup application, database connection & other services
			builder.Configure();
			
			App = builder.Build();
			// Setup HTTP, routing, authentication & mapping
			App.Configure();
			App.Run();
		}

		private static void Configure(this WebApplicationBuilder builder) {
			try {
				// Setup DB connection
				string connectionString = new DatabaseSettings(true).GetConnectionString();
				builder.Services.AddDbContextFactory<DB>(options => {
					try {
						options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
					}catch { }
				});
				builder.Services.AddDatabaseDeveloperPageExceptionFilter();
			}catch { }
			// Setup entity framework
			builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<DB>();
			// Setup razor & blazor
			builder.Services.AddRazorPages();
			builder.Services.AddServerSideBlazor();
			// Localization/translations
			builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
			builder.Services.AddScoped<IStringLocalizer<App>, StringLocalizer<App>>();
			// Inject authentication
			builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();
			// Radzen components
			builder.Services.AddScoped<DialogService>();
			builder.Services.AddScoped<TooltipService>();
			// Add other dependencies here
			// ...
		}

		private static void Configure(this WebApplication app) {
			// Configure the HTTP request pipeline.
			if(app.Environment.IsDevelopment()) {
				app.UseMigrationsEndPoint();
			} else {
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			// Setup routing
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			// Setup authentication
			app.UseAuthentication();
			app.UseAuthorization();
			// Setup mapping
			app.MapControllers();
			app.MapBlazorHub();
			app.MapFallbackToPage("/_Host");
		}
	}
}
