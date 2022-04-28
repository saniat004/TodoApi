using Microsoft.EntityFrameworkCore;
using TodoApiPractise.DbContexts;
using TodoApiPractise.Entities;
using System.Linq;

namespace TodoApiPractise.Services
{
    public class ToDoListRepository : IToDoListRepository


    {
        private readonly ToDoListContext _context;

        public ToDoListRepository(ToDoListContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<ToDoList>> GetToDoListAsync()
        {
            return await _context.ToDoLists.OrderBy(c => c.StartDate).ToListAsync();
            
        }
        public async Task AddToDoListAsync(ToDoList toDoList)
        {
           await _context.ToDoLists.AddAsync(toDoList);
        }

        public async Task<ToDoList?> GetSpecificTodoAsync(int taskId)
        {
            return await _context.ToDoLists.Where(c => c.Id == taskId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ToDoList>> GetIncomingToDoAsync(DateTime dateTime)
        {
            return await _context.ToDoLists.Where(c => c.StartDate == dateTime).ToListAsync();
        }


        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }


        public async Task<bool> ToDoListExistsAsync(int TaskId)
        {
            return await _context.ToDoLists.AnyAsync(c=>c.Id == TaskId);

        }

        public void DeleteToDoList(ToDoList toDoListDto)
        {
            _context.ToDoLists.Remove(toDoListDto);
        }

    }
}
