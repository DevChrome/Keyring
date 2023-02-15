using Keyring.ViewModels;
using Mopups.Pages;

namespace Keyring;

public partial class IconPopup : PopupPage
{
	private TaskCompletionSource<string> tcs;
	public Task<string> PopupClosedTask => tcs.Task;

	public IconPopup (IconPopupVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	protected override void OnAppearing ()
	{
		base.OnAppearing();
		tcs = new TaskCompletionSource<string>();
	}

	protected override void OnDisappearing ()
	{
		base.OnDisappearing();
		tcs.SetResult(((IconPopupVM)BindingContext).PickedIcon);
	}
}