using Keyring.Services;
using Keyring.ViewModels;
using Mopups.Hosting;

namespace Keyring;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp ()
	{
		var builder = MauiApp.CreateBuilder();

		builder.UseMauiApp<App>();

		builder.ConfigureFonts(fonts =>
		{
			fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			fonts.AddFont("Coda-Regular.ttf", "Coda");
			fonts.AddFont("bootstrap-icons.ttf", "BootstrapIcons");
		});

		builder.ConfigureMopups();

		builder.Services.AddSingleton<IHasherService, SHA256HasherService>();
		builder.Services.AddSingleton<IDatabaseService, SqliteDatabaseService>();

		builder.Services.AddTransient<LoginPageVM>();
		builder.Services.AddTransient<LoginPage>();

		builder.Services.AddTransient<PasswordsPageVM>();
		builder.Services.AddTransient<PasswordsPage>();

		builder.Services.AddTransient<EditPassPageVM>();
		builder.Services.AddTransient<EditPassPage>();

		builder.Services.AddTransient<ProfilePageVM>();
		builder.Services.AddTransient<ProfilePage>();

		builder.Services.AddTransient<IconPopupVM>();
		builder.Services.AddTransient<IconPopup>();

		return builder.Build();
	}
}
