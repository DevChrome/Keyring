namespace Keyring;

public partial class AppShell : Shell
{
	public AppShell ()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(PasswordsPage), typeof(PasswordsPage));
		Routing.RegisterRoute(nameof(EditPassPage), typeof(EditPassPage));
		Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
	}
}
