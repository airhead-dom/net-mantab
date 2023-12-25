using System.Data;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NetMantab.Data;
using NetMantab.Models;
using NuGet.Protocol;

namespace NetMantab.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly AppADOConnection _adoConn;
        private readonly IMapper _mapper;

        public UserService(AppDbContext context, AppADOConnection adoConn, IMapper mapper)
        {
            _context = context;
            _adoConn = adoConn;
            _mapper = mapper;
        }

        public UserDto? Get(int id)
        {
            User? user = _context.User.Where(t => t.ID == id).FirstOrDefault();

            if (user == null)
                return null;

            return _mapper.Map<UserDto>(user);
        }

        public List<UserAndTodoDto> GetWithTodoList()
        {
            SqlConnection conn = _adoConn.GetConnection();

            string firstCmdSql = "select * from [User]";
            string secondCmdSql = "select Title, Description from Todo where OwnedBy = @OwnerId order by [Order]";

            SqlCommand firstCmd = new SqlCommand(firstCmdSql, conn);
            SqlCommand secondCmd = new SqlCommand(secondCmdSql, conn);
            secondCmd.Parameters.Add("@OwnerId", SqlDbType.Int);

            List<UserAndTodoDto> results = new List<UserAndTodoDto>();

            conn.Open();

            SqlDataReader firstReader = firstCmd.ExecuteReader();

            while (firstReader.Read())
            {
                UserAndTodoDto user = new UserAndTodoDto();
                user.Name = firstReader.GetString(1);
                user.Todos = new List<UserAndTodoDto.Todo>();

                secondCmd.Parameters["@OwnerId"].Value = firstReader.GetInt32(0);

                SqlDataReader secondReader = secondCmd.ExecuteReader();

                while (secondReader.Read())
                {
                    UserAndTodoDto.Todo todo = new UserAndTodoDto.Todo(secondReader.GetString(0), secondReader.GetString(1));
                    user.Todos.Add(todo);
                }

                secondReader.Close();

                results.Add(user);
            }

            firstReader.Close();
            conn.Close();

            return results;
        }


        public async Task<List<UserAndTodoDto>> GetWithTodoListAsync()
        {
            SqlConnection conn = _adoConn.GetConnection();

            string firstCmdSql = "select * from [User]";
            string secondCmdSql = "select Title, Description from Todo where OwnedBy = @OwnerId order by [Order]";

            SqlCommand firstCmd = new SqlCommand(firstCmdSql, conn);


            List<UserAndTodoDto> results = new List<UserAndTodoDto>();

            await conn.OpenAsync();

            SqlDataReader firstReader = firstCmd.ExecuteReader();

            List<int> ids = new List<int>();

            while (firstReader.Read())
            {
                UserAndTodoDto user = new UserAndTodoDto();
                user.Name = firstReader.GetString(1);
                user.Todos = new List<UserAndTodoDto.Todo>();

                results.Add(user);
                ids.Add(firstReader.GetInt32(0));
            }

            firstReader.Close();

            ids.ForEach(async id =>
            {
                SqlCommand secondCmd = new SqlCommand(secondCmdSql, conn);
                secondCmd.Parameters.Add("@OwnerId", SqlDbType.Int);
                secondCmd.Parameters["@OwnerId"].Value = id;

                SqlDataReader secondReader = await secondCmd.ExecuteReaderAsync();

                while (secondReader.Read())
                {
                    UserAndTodoDto.Todo todo = new UserAndTodoDto.Todo(secondReader.GetString(0), secondReader.GetString(1));
                    results.ElementAt(id).Todos?.Add(todo);
                }

                secondReader.Close();
            });

            await conn.CloseAsync();
            
            return results;
        }

        public User Create(CreateUserDto dto)
        {
            User user = _mapper.Map<User>(dto);

            _context.User.Add(user);
            _context.SaveChanges();

            return user;
        }
    }
}