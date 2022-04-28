using Microsoft.EntityFrameworkCore;
using TodoApiPractise.Entities;

namespace TodoApiPractise.DbContexts
{
    public class ToDoListContext : DbContext
    {
        public ToDoListContext(DbContextOptions<ToDoListContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<ToDoList> ToDoLists { get; set; } = null!;
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoList>()
                .HasData(
               new ToDoList()
               {
                   Id = 1,
                   Title = "fhjhfjy",
                   Description = "The one with that big park.",
                   StartDate = new DateTime(2022, 3, 10),
                   EndDate = new DateTime(2022, 11, 10),
                   CompletedPercentage = 39,
                   Done = false

        },
               new ToDoList()
               {
                   Id = 2,
                   Title = "fjy",
                   Description = "The one with that big park.",
                   StartDate = new DateTime(2022, 1, 10),
                   EndDate = new DateTime(2022, 12, 10),
                   CompletedPercentage = 39,
                   Done = false
               },
               new ToDoList()
               {
                   Id = 3,
                   Title = "jy",
                   Description = "The one with that big park.",
                   StartDate = new DateTime(2022, 7, 10),
                   EndDate = new DateTime(2022, 10, 10),
                   CompletedPercentage = 39,
                   Done = false
               });
            base.OnModelCreating(modelBuilder);

        }
    }
}
