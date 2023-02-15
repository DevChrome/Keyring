using SQLite;

namespace Keyring.Models;

[Table("Records")]
public class Record
{
	[PrimaryKey, AutoIncrement]
	public int? Id { get; set; }
	[Indexed]
	public int UserID { get; set; }
	public string Icon { get; set; }
	public string Website { get; set; }
	public string ClearPassword { get; set; }
	public DateTime Timestamp { get; set; }
	public string Note { get; set; }
}
