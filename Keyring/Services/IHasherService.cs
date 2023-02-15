using System.Security.Cryptography;
using System.Text;

namespace Keyring.Services;

public interface IHasherService
{
	string HashPassword (string clearPassword);
}

public class SHA256HasherService : IHasherService
{
	private readonly SHA256 sha = SHA256.Create();
	public string HashPassword (string clearPassword) => Encoding.ASCII.GetString(sha.ComputeHash(Encoding.ASCII.GetBytes(clearPassword)));
}
