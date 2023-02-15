using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Keyring.Models;
using Keyring.Services;
using Mopups.Services;

namespace Keyring.ViewModels;

[QueryProperty(nameof(CurrentRecord), "currentRecord")]
[QueryProperty(nameof(CurrentUser), "currentUser")]
public partial class EditPassPageVM : ObservableObject
{
	[ObservableProperty]
	private Record currentRecord;

	[ObservableProperty]
	private User currentUser;

	public string PageTitle => CurrentRecord == null ? "Add Password" : "Edit Password";

	[ObservableProperty]
	private string icon;
	[ObservableProperty]
	private string website;
	[ObservableProperty]
	private string clearPass;
	[ObservableProperty]
	private string note;

	private readonly IDatabaseService databaseService;
	private readonly IconPopup iconPopup;
	public EditPassPageVM (IDatabaseService databaseService, IconPopup iconPopup)
	{
		this.databaseService = databaseService;
		this.iconPopup = iconPopup;
	}

	[RelayCommand]
	public void NavigatedTo ()
	{
		OnPropertyChanged(nameof(PageTitle));
		Icon = CurrentRecord?.Icon ?? IconPopupVM.Default;
		Website = CurrentRecord?.Website ?? "";
		ClearPass = CurrentRecord?.ClearPassword ?? "";
		Note = CurrentRecord?.Note ?? "";
	}

	[RelayCommand]
	public async Task Save ()
	{
		if (string.IsNullOrWhiteSpace(Website) || string.IsNullOrWhiteSpace(ClearPass))
		{
			await Shell.Current.CurrentPage.DisplayAlert("Empty Fields!", "Please enter both a website and a password.", "OK");
			return;
		}

		Record record = new();

		if (CurrentRecord is not null)
			record.Id = CurrentRecord.Id;

		record.UserID = CurrentUser.Id.Value;
		record.Icon = Icon;
		record.Website = Website;
		record.ClearPassword = ClearPass;
		record.Note = Note;
		record.Timestamp = DateTime.Now;

		await databaseService.AddOrUpdateRecord(record);

		await Shell.Current.GoToAsync("..", true);
	}

	[RelayCommand]
	public async Task GoBack ()
	{
		bool leave = await Shell.Current.CurrentPage.DisplayAlert("Leave?", "Do you want to leave without saving this entry?", "Yes", "No");
		if (leave)
			await Shell.Current.GoToAsync("..", true);
	}

	[RelayCommand]
	public async Task PickIcon ()
	{
		await MopupService.Instance.PushAsync(iconPopup);
		Icon = await iconPopup.PopupClosedTask;
	}
}