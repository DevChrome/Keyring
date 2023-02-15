using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using BI = Keyring.Helpers.BootstrapIcons;

namespace Keyring.ViewModels;

public partial class IconPopupVM : ObservableObject
{
	public const string Default = BI.Circle;

	public static readonly string[] Icons =
	{
		Default,
		BI.Google, BI.Facebook, BI.Instagram, BI.Discord, BI.Reddit,
		BI.Twitter, BI.Vimeo, BI.Android, BI.Apple, BI.Bank, BI.Laptop,
	};

	[ObservableProperty]
	private string pickedIcon = Default;

	[RelayCommand]
	public async Task Done ()
	{
		await MopupService.Instance.PopAsync();
	}
}
