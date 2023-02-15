using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Keyring.Models;
using Keyring.Services;

namespace Keyring.ViewModels;

[QueryProperty(nameof(CurrentUser), "currentUser")]
public partial class PasswordsPageVM : ObservableObject
{
	[ObservableProperty]
	private User currentUser;

	[ObservableProperty]
	private IEnumerable<Record> records;

	private readonly IDatabaseService databaseService;
	public PasswordsPageVM (IDatabaseService databaseService) => this.databaseService = databaseService;

	[RelayCommand]
	public async Task NavigatedTo ()
	{
		Records = await databaseService.GetEntries(CurrentUser.Id.Value);
	}

	[RelayCommand]
	public async Task GoToProfile ()
	{
		await Shell.Current.GoToAsync(nameof(ProfilePage), true, new Dictionary<string, object>()
		{
			{ "currentUser", CurrentUser }
		});
	}

	[RelayCommand]
	public async Task AddRecord ()
	{
		await Shell.Current.GoToAsync(nameof(EditPassPage), true, new Dictionary<string, object>()
		{
			{ "currentUser", CurrentUser },
			{ "currentRecord", null }
		});
	}

	[RelayCommand]
	public async Task ShowRecord (Record rec)
	{
		await Shell.Current.CurrentPage.DisplayAlert(rec.Website, rec.Note.Trim(), "OK");
	}

	[RelayCommand]
	public async Task EditRecord (Record rec)
	{
		await Shell.Current.GoToAsync(nameof(EditPassPage), true, new Dictionary<string, object>()
		{
			{ "currentUser", CurrentUser },
			{ "currentRecord", rec }
		});
	}

	[RelayCommand]
	public async Task DeleteRecord (Record rec)
	{
		bool delete = await Shell.Current.CurrentPage.DisplayAlert("Delete record?", "Are you sure you want to delete this record?", "Delete", "Cancel");
		if (delete)
		{
			await databaseService.RemoveRecord(rec);
			Records = await databaseService.GetEntries(CurrentUser.Id.Value);
		}
	}
}