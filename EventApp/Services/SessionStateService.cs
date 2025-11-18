namespace EventApp.Services;

// This class will manage state for a single user session.
public class SessionStateService
{
    // A simple way to track the current user. In a real app, this would
    // be a more complex user object after a login.
    public string? CurrentUserName { get; private set; }

    // This set will store the IDs of the events the current user is attending.
    // A HashSet is used because it's very efficient for checking if an item exists.
    private readonly HashSet<int> _attendedEventIds = new();

    public bool IsUserLoggedIn => !string.IsNullOrEmpty(CurrentUserName);

    // A method to "log in" a user.
    public void Login(string name)
    {
        CurrentUserName = name;
    }

    // A method to register the user for an event (part of the attendance tracker).
    public void AttendEvent(int eventId)
    {
        _attendedEventIds.Add(eventId);
    }

    // A method to check if the user is attending a specific event.
    public bool IsAttending(int eventId)
    {
        return _attendedEventIds.Contains(eventId);
    }
}