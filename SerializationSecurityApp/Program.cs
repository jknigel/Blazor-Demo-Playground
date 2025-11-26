using System.Text.Json;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

public class Program
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public void EncryptData()
        {
            Password = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Password));
        }

        public string GenerateHash()
        {
            string data = $"{Name}|{Email}|{Password}";
            using (SHA256 sha = SHA256.Create())
            {
                byte[] hashBytes = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(data));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }

    public static string SerializeUserData(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
        {
            Console.WriteLine("Invalid Data!");
            return string.Empty;
        }
        user.EncryptData();
        string hash = user.GenerateHash();
        return JsonSerializer.Serialize(user);
    }

    public static User DeserializeUserData(string jsonData, bool isTrustedSource)
    {
        if (!isTrustedSource)
        {
            Console.WriteLine("Deserialization Blocked! Suspicious Attempt.");
            return null;
        }
        return JsonSerializer.Deserialize<User>(jsonData);
    }

    public static void Main()
    {
        User sampleUser = new User
        {
            Name = "Alice",
            Email = "alice@example.com",
            Password = "SecurePasswordJoke123"
        };

        string serializedData = SerializeUserData(sampleUser);
        Console.WriteLine($"Serialized Data: {serializedData}");

        User deserializedDataTrusted = DeserializeUserData(serializedData, true);
        if (deserializedDataTrusted != null)
        {
            Console.WriteLine($"Deserialized Data (Trusted): {deserializedDataTrusted.Name}");
        }
        

        User deserializedDataUntrusted = DeserializeUserData(serializedData, false);
        if (deserializedDataUntrusted != null)
        {
            Console.WriteLine($"Deserialized Data (Untrusted): {deserializedDataUntrusted.Name}");
        }
    }
}