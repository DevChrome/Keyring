using SQLite;

namespace Keyring.Models;

[Table("Users")]
public class User
{
	[PrimaryKey, AutoIncrement]
	public int? Id { get; set; }
	public string EmailAddress { get; set; }
	public string HashedPassword { get; set; }
	public DateTime SignUpTimestamp { get; set; }
}