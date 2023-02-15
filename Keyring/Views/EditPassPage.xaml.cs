using Keyring.ViewModels;

namespace Keyring;

public partial class EditPassPage : ContentPage
{
	public EditPassPage (EditPassPageVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	private void HandleNavigatedTo (object sender, NavigatedToEventArgs e)
	{
		((EditPassPageVM)BindingContext).NavigatedTo();
	}
}