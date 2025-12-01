using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")] // This will set the base route to "/api/users"
public class UsersController : ControllerBase
{
    // A static list to act as our simple, in-memory database.
    // 'static' ensures the data persists between API calls.
    private static List<User> _users = new List<User>
    {
        new User { Id = 1, Name = "Alice Johnson", Email = "alice.j@techhive.com" },
        new User { Id = 2, Name = "Bob Williams", Email = "bob.w@techhive.com" }
    };
    private static int _nextId = 3;

    // --- CRUD ENDPOINTS ---

    // GET: /api/users
    // Retrieves a list of all users.
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAllUsers()
    {
        return Ok(_users);
    }

    // GET: /api/users/{id}
    // Retrieves a specific user by their ID.
    [HttpGet("{id}")]
    public ActionResult<User> GetUserById(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound(); // Returns a 404 error if the user isn't found.
        }
        return Ok(user);
    }

    // POST: /api/users
    // Adds a new user to the collection.
    [HttpPost]
    public ActionResult<User> AddUser([FromBody] User newUser)
    {
        if (newUser == null)
        {
            return BadRequest("User data is null.");
        }
        
        newUser.Id = _nextId++;
        _users.Add(newUser);

        // Returns a "201 Created" status with the location of the new user.
        return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
    }

    // PUT: /api/users/{id}
    // Updates an existing user's details.
    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
        {
            return NotFound();
        }

        existingUser.Name = updatedUser.Name;
        existingUser.Email = updatedUser.Email;

        return NoContent(); // Returns a "204 No Content" success status.
    }

    // DELETE: /api/users/{id}
    // Removes a user by their ID.
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var userToRemove = _users.FirstOrDefault(u => u.Id == id);
        if (userToRemove == null)
        {
            return NotFound();
        }

        _users.Remove(userToRemove);
        
        return NoContent(); // Returns a "204 No Content" success status.
    }
}