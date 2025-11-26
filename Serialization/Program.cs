using System.IO;
using System.Xml.Serialization;
using System.Text.Json;

public class Person
{
    public string UserName { get; set; }
    public int UserAge { get; set; }
}

public class Program
{
    public static void Main()
    {
        Person userOne = new Person
        {
            UserName = "userOne",
            UserAge = 28
        };

        // Create or overwrite a file named "person.dat"
        using (FileStream fs = new FileStream("person.dat", FileMode.Create, FileAccess.Write))
        {
            // Example: write some binary data
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                writer.Write(userOne.UserName);
                writer.Write(userOne.UserAge);
            }
        }

        Console.WriteLine("Binary serialization to person.dat is complete.");

        // 1. Instantiate an XmlSerializer for the Person class
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person));

        // 2. Serialize the Person object and save it to an .xml file
        using (StreamWriter writer = new StreamWriter("person.xml"))
        {
            xmlSerializer.Serialize(writer, userOne);
        }

        // 3. Print confirmation message
        Console.WriteLine("XML serialization to person.xml is complete.");

        string jsonString = JsonSerializer.Serialize(userOne);
        File.WriteAllText("person.json", jsonString);

        Console.WriteLine("JSON serialization to person.json is complete.");
    }
}