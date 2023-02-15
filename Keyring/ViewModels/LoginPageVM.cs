using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Keyring.Helpers;
using Keyring.Models;
using Keyring.Services;

namespace Keyring.ViewModels;

public partial class LoginPageVM : ObservableObject
{
	[ObservableProperty]
	private string email;
	[ObservableProperty]
	private string clearPass;

	private readonly IDatabaseService databaseService;
	private readonly IHasherService hasherService;

	public LoginPageVM (IDatabaseService databaseService, IHasherService hasherService)
	{
		this.databaseService = databaseService;
		this.hasherService = hasherService;
	}

	public Task Loaded ()
	{
		User test = new()
		{
			Id = 69,
			EmailAddress = "test@admin.com",
			HashedPassword = hasherService.HashPassword("12345678"),
			SignUpTimestamp = DateTime.Now,
		};
		return databaseService.AddOrUpdateUser(test);
	}

	public void NavigatedTo ()
	{
		Email = ClearPass = "";
	}

	[RelayCommand]
	public async Task SignUp ()
	{
		if (!EmailValidator.IsValid(Email))
		{
			await Shell.Current.CurrentPage.DisplayAlert("Invalid email", "The entered email address doesn't seem to be valid.", "OK");
			return;
		}

		User user = new()
		{
			EmailAddress = Email,
			HashedPassword = hasherService.HashPassword(ClearPass),
			SignUpTimestamp = DateTime.Now,
		};

		bool exists = await databaseService.UserExists(user.EmailAddress);
		if (exists)
		{
			await Shell.Current.CurrentPage.DisplayAlert("User exists!", "A user with specified email already exists.", "OK");
			return;
		}

		await databaseService.AddOrUpdateUser(user);

		bool login = await Shell.Current.CurrentPage.DisplayAlert("Created user!", "A new account has been created. Log in?", "Yes", "No");
		if (login)
		{
			await Shell.Current.GoToAsync(nameof(PasswordsPage), true, new Dictionary<string, object>()
			{
				{ "currentUser", user }
			});
		}
		else
		{
			Email = ClearPass = "";
		}
	}

	[RelayCommand]
	public async Task Login ()
	{
		User user = await databaseService.MatchUser(Email, hasherService.HashPassword(ClearPass));

		if (user is not null)
		{
			await Shell.Current.GoToAsync(nameof(PasswordsPage), true, new Dictionary<string, object>()
			{
				{ "currentUser", user }
			});
		}
		else
		{
			await Shell.Current.CurrentPage.DisplayAlert("User not found!", "No user matched with the specified credentials.", "OK");
		}
	}
}