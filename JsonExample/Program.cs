public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class Program
{
    public static void Main(string[] args)
    {
        var person = new Person
        {
            Name = "Alice",
            Age = 30
        };

        // Serialize to JSON
        string jsonString = System.Text.Json.JsonSerializer.Serialize(person);
        System.Console.WriteLine("Serialized JSON:");
        System.Console.WriteLine(jsonString);

        // Deserialize from JSON
        var deserializedPerson = System.Text.Json.JsonSerializer.Deserialize<Person>(jsonString);
        System.Console.WriteLine("Deserialized Person:");
        System.Console.WriteLine($"Name: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");
    }
}