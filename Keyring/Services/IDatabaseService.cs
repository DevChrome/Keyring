using Keyring.Models;
using SQLite;
using Record = Keyring.Models.Record;

namespace Keyring.Services;

public interface IDatabaseService
{
	Task CreateDatabase ();
	Task DeleteDatabase ();

	Task<bool> UserExists (string email);
	Task<User> MatchUser (string email, string clearPass);
	Task AddOrUpdateUser (User user);
	Task RemoveUser (User user);

	Task<IEnumerable<Record>> GetEntries (int userID);
	Task AddOrUpdateRecord (Record record);
	Task RemoveRecord (Record record);
}

public class SqliteDatabaseService : IDatabaseService, IAsyncDisposable
{
	private const string DatabaseKeyStoredInTheWorstWayPossible = "123";
	private static readonly string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Keyring.db");

	private SQLiteAsyncConnection connection;

	public async Task CreateDatabase ()
	{
		if (connection is null)
		{
			SQLiteConnectionString connString = new(dbPath, true, DatabaseKeyStoredInTheWorstWayPossible);
			connection = new SQLiteAsyncConnection(connString);

			await connection.CreateTableAsync<User>();
			await connection.CreateTableAsync<Record>();
		}
	}

	public async Task DeleteDatabase ()
	{
		if (connection is not null)
		{
			await connection.CloseAsync();
			connection = null;
			File.Delete(dbPath);
		}
	}

	public async Task<bool> UserExists (string email)
	{
		await CreateDatabase();
		return await connection.Table<User>().Where(u => u.EmailAddress == email).CountAsync() > 0;
	}

	public async Task<User> MatchUser (string email, string hashedPass)
	{
		await CreateDatabase();
		return await connection.Table<User>().Where(u => u.EmailAddress == email && u.HashedPassword == hashedPass).FirstOrDefaultAsync();
	}

	public async Task AddOrUpdateUser (User user)
	{
		await CreateDatabase();
		await connection.InsertOrReplaceAsync(user);
	}

	public async Task RemoveUser (User user)
	{
		await connection.Table<Record>().DeleteAsync(e => e.UserID == user.Id);
		await connection.DeleteAsync(user);
	}

	public async Task<IEnumerable<Record>> GetEntries (int userID)
	{
		await CreateDatabase();
		return await connection.Table<Record>().Where(e => e.UserID == userID).ToArrayAsync();
	}

	public async Task AddOrUpdateRecord (Record record)
	{
		await CreateDatabase();
		await connection.InsertOrReplaceAsync(record);
	}

	public async Task RemoveRecord (Record record)
	{
		await CreateDatabase();
		await connection.DeleteAsync(record);
	}

	public ValueTask DisposeAsync ()
	{
		return new(connection?.CloseAsync());
	}
}
