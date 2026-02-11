using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Models;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase {
    private readonly UserDbContext _context;
    public UsersController(UserDbContext context) { _context = context; }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers() =>
        await _context.Users.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id) {
        var user = await _context.Users.FindAsync(id);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody] UserDto dto) {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var user = new User { Name = dto.Name, Email = dto.Email };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto dto) {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        user.Name = dto.Name;
        user.Email = dto.Email;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id) {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
