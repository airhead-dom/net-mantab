using NetMantab.Data;
using NetMantab.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace NetMantab.Services
{
    public class TodoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TodoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public TodoDto? Get(int id)
        {
            Todo? todo = _context.Todo.Where(t => t.ID == id).FirstOrDefault();

            if (todo == null)
                return null;

            return _mapper.Map<TodoDto>(todo);
        }

        public TodoDto Create(CreateTodoDto dto)
        {
            Todo todo = _mapper.Map<Todo>(dto);

            todo.Order = GetLastOrder();

            _context.Todo.Add(todo);
            _context.SaveChanges();

            return _mapper.Map<TodoDto>(todo);
        }

        

        private int GetLastOrder()
        {
            var todo = _context.Todo.FromSqlInterpolated($"select top 1 * from Todo order by [Order] desc").Select(t => new { Order = t.Order + 1 }).FirstOrDefault();

            if (todo == null)
                return 1;

            return todo.Order;
        }
    }
}