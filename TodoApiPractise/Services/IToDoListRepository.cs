using TodoApiPractise.Entities;


namespace TodoApiPractise.Services
{
    public interface IToDoListRepository
    {
        Task<IEnumerable<ToDoList>> GetToDoListAsync();       
        Task<ToDoList?> GetSpecificTodoAsync(int taskId);
        Task<IEnumerable<ToDoList>> GetIncomingToDoAsync(DateTime dateTime);
        Task AddToDoListAsync(ToDoList toDoList);
        Task<bool> ToDoListExistsAsync(int TaskId);
        void DeleteToDoList(ToDoList toDoListDto);      
        Task<bool> SaveChangesAsync();
    }
}
