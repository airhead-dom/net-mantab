using Microsoft.AspNetCore.Mvc;
using NetMantab.Models;
using NetMantab.Services;

namespace NetMantab.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodosController(TodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet("{id}")]
        public ActionResult<TodoDto> GetTodo(int id)
        {
            TodoDto? todo = _todoService.Get(id);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        [HttpPost]
        public ActionResult<TodoDto> PostTodo(CreateTodoDto todo)
        {
            TodoDto created = _todoService.Create(todo);
            return Created("GetTodo", created);
        }
    }
}
