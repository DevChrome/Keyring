using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Keyring.Models;
using Keyring.Services;

namespace Keyring.ViewModels;

[QueryProperty(nameof(CurrentUser), "currentUser")]
public partial class ProfilePageVM : ObservableObject
{
	[ObservableProperty]
	private User currentUser;

	[ObservableProperty]
	private int recordCount;

	private readonly IDatabaseService databaseService;
	public ProfilePageVM (IDatabaseService databaseService) => this.databaseService = databaseService;

	[RelayCommand]
	public async Task NavigatedTo ()
	{
		RecordCount = (await databaseService.GetEntries(CurrentUser.Id.Value)).Count();
	}

	[RelayCommand]
	public async Task ProfilePhoto ()
	{
		await Shell.Current.CurrentPage.DisplayAlert("Thamo zara!", "You can't set profiles pictures yet, itna time nahi bacha tha, sorry!", "Theek hai");
	}

	[RelayCommand]
	public async Task Logout ()
	{
		bool leave = await Shell.Current.CurrentPage.DisplayAlert("Log Out?", "Do you want to log out from the app?", "Yes", "No");
		if (leave)
			await Shell.Current.GoToAsync("../..", true);
	}

	[RelayCommand]
	public async Task DeleteUser ()
	{
		bool delete = await Shell.Current.CurrentPage.DisplayAlert("Delete account?", "Do you really want to delete your account?", "Yes", "No");

		if (delete)
			delete &= await Shell.Current.CurrentPage.DisplayAlert("Really delete account?", "Are you SURE you want to delete your account?", "YEAH", "NOPE");

		if (delete)
		{
			await databaseService.RemoveUser(CurrentUser);
			await Shell.Current.GoToAsync("../..", true);
		}
	}
}
