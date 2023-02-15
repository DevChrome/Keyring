using Keyring.ViewModels;

namespace Keyring;

public partial class ProfilePage : ContentPage
{
	public ProfilePage (ProfilePageVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	private async void HandleNavigatedTo (object sender, NavigatedToEventArgs e)
	{
		await ((ProfilePageVM)BindingContext).NavigatedTo();
	}
}