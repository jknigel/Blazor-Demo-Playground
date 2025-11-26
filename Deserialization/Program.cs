using System;
using System.IO;
using System.Xml.Serialization;
using System.Text.Json;
using System.Diagnostics;

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

        // Binary serialization
        using (FileStream fs = new FileStream("person.dat", FileMode.Create, FileAccess.Write))
        using (BinaryWriter writer = new BinaryWriter(fs))
        {
            writer.Write(userOne.UserName);
            writer.Write(userOne.UserAge);
        }
        Console.WriteLine("Binary serialization to person.dat is complete.");

        // Binary deserialization with error handling
        Person binaryPerson = null;
        Stopwatch binaryStopwatch = Stopwatch.StartNew();
        try
        {
            using (var fs = new FileStream("person.dat", FileMode.Open))
            using (var reader = new BinaryReader(fs))
            {
                binaryPerson = new Person
                {
                    UserName = reader.ReadString(),
                    UserAge = reader.ReadInt32()
                };
            }
            binaryStopwatch.Stop();

            if (binaryPerson != null && !string.IsNullOrEmpty(binaryPerson.UserName) && binaryPerson.UserAge > 0)
            {
                Console.WriteLine($"Binary Deserialization - UserName: {binaryPerson.UserName}, UserAge: {binaryPerson.UserAge}");
                Console.WriteLine("Binary data is valid and complete.");
            }
            else
            {
                Console.WriteLine("Binary data is incomplete or invalid.");
            }
        }
        catch (Exception ex)
        {
            binaryStopwatch.Stop();
            Console.WriteLine($"Binary deserialization failed: {ex.Message}");
        }
        Console.WriteLine($"Binary Elapsed Time = {binaryStopwatch.ElapsedMilliseconds}ms");


        // XML serialization
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person));
        using (StreamWriter writer = new StreamWriter("person.xml"))
        {
            xmlSerializer.Serialize(writer, userOne);
        }
        Console.WriteLine("XML serialization to person.xml is complete.");

        // XML deserialization with error handling
        Person XMLPerson = null;
        Stopwatch XMLstopwatch = Stopwatch.StartNew();
        try
        {
            using (var fs = new FileStream("person.xml", FileMode.Open))
            {
                XMLPerson = (Person)xmlSerializer.Deserialize(fs);
            }
            XMLstopwatch.Stop();

            if (XMLPerson != null && !string.IsNullOrEmpty(XMLPerson.UserName) && XMLPerson.UserAge > 0)
            {
                Console.WriteLine($"XML Deserialization - UserName: {XMLPerson.UserName}, UserAge: {XMLPerson.UserAge}");
                Console.WriteLine("XML data is valid and complete.");
            }
            else
            {
                Console.WriteLine("XML data is incomplete or invalid.");
            }
        }
        catch (Exception ex)
        {
            XMLstopwatch.Stop();
            Console.WriteLine($"XML deserialization failed: {ex.Message}");
        }
        Console.WriteLine($"XML Elapsed Time = {XMLstopwatch.ElapsedMilliseconds}ms");


        // JSON serialization
        string jsonString = JsonSerializer.Serialize(userOne);
        File.WriteAllText("person.json", jsonString);
        Console.WriteLine("JSON serialization to person.json is complete.");

        // JSON deserialization with error handling
        Person JSONPerson = null;
        Stopwatch JSONstopwatch = Stopwatch.StartNew();
        try
        {
            using (var fs = new FileStream("person.json", FileMode.Open))
            {
                JSONPerson = JsonSerializer.Deserialize<Person>(fs);
            }
            JSONstopwatch.Stop();

            if (JSONPerson != null && !string.IsNullOrEmpty(JSONPerson.UserName) && JSONPerson.UserAge > 0)
            {
                Console.WriteLine($"JSON Deserialization - UserName: {JSONPerson.UserName}, UserAge: {JSONPerson.UserAge}");
                Console.WriteLine("JSON data is valid and complete.");
            }
            else
            {
                Console.WriteLine("JSON data is incomplete or invalid.");
            }
        }
        catch (Exception ex)
        {
            JSONstopwatch.Stop();
            Console.WriteLine($"JSON deserialization failed: {ex.Message}");
        }
        Console.WriteLine($"JSON Elapsed Time = {JSONstopwatch.ElapsedMilliseconds}ms");
    }
}
