using Microsoft.AspNetCore.Mvc;
using NetMantab.Models;
using NetMantab.Services;

namespace NetMantab.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUser(int id)
        {
            UserDto? todo = _userService.Get(id);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        [HttpGet("with-todos")]
        public ActionResult<UserDto> GetWithTodos()
        {
            List<UserAndTodoDto> users = _userService.GetWithTodoList();
            return Ok(users);
        }

        [HttpGet("with-todos-async")]
        public async Task<ActionResult<UserDto>> GetWithTodosAsync()
        {
            List<UserAndTodoDto> users = await _userService.GetWithTodoListAsync();
            return Ok(users);
        }

        [HttpPost]
        public ActionResult<User> PostUser(CreateUserDto user)
        {
            User created = _userService.Create(user);
            return Created("GetUser", created);
        }
    }
}