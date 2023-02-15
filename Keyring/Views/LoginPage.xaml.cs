using Keyring.ViewModels;

namespace Keyring;

public partial class LoginPage : ContentPage
{
	public LoginPage (LoginPageVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	private async void HandleLoaded (object sender, EventArgs e)
	{
		await ((LoginPageVM)BindingContext).Loaded();
	}

	private void HandleNavigatedTo (object sender, NavigatedToEventArgs e)
	{
		((LoginPageVM)BindingContext).NavigatedTo();
	}
}