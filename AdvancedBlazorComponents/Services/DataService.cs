// Use your project's namespace
namespace AdvancedBlazorComponents.Services;

// This is a regular class, NOT static
public class DataService
{
    // This is a regular method, NOT static
    public List<string> GetData()
    {
        return new List<string>
        {
            "Item 1 (from service)",
            "Item 2 (from service)",
            "Item 3 (from service)",
            "Item 4 (from service)",
            "Item 5 (from service)"
        };
    }
}