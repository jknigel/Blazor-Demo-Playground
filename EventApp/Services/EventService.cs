namespace EventApp.Services;

using EventApp.Models;

// This service will manage our list of events.
public class EventService
{
    // A private list to act as our in-memory database.
    private readonly List<Event> _events = new()
    {
        new Event { Id = 1, Name = "Tech Conference 2025", Date = new DateTime(2025, 10, 22), Location = "New York, NY" },
        new Event { Id = 2, Name = "Annual Company Summit", Date = new DateTime(2025, 11, 15), Location = "San Francisco, CA" },
        new Event { Id = 3, Name = "Marketing Web Expo", Date = new DateTime(2026, 1, 30), Location = "Chicago, IL" }
    };

    private int _nextId = 4;
    private readonly List<Registration> _registrations = new();

    // A public method to get all events.
    public List<Event> GetAllEvents() => _events;

    // A public method to get a single event by its ID.
    public Event? GetEventById(int id)
    {
        return _events.FirstOrDefault(e => e.Id == id);
    }

    public void AddEvent(Event newEvent)
    {
        newEvent.Id = _nextId++; // Assign a new unique ID
        _events.Add(newEvent);
    }

    public void RegisterAttendee(Registration registration)
    {
        // In a real application, you would save this to a database.
        // For now, we just add it to our in-memory list.
        _registrations.Add(registration);
        Console.WriteLine($"New registration for Event ID {registration.EventId}: {registration.AttendeeName}");
    }
}