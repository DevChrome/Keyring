using Keyring.ViewModels;

namespace Keyring;

public partial class PasswordsPage : ContentPage
{
	public PasswordsPage (PasswordsPageVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	private async void HandleNavigatedTo (object sender, NavigatedToEventArgs e)
	{
		await ((PasswordsPageVM)BindingContext).NavigatedTo();
	}
}