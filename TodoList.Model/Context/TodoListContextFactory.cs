using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Model.Context
{
    public class TodoListContextFactory : IDesignTimeDbContextFactory<TodoListDBContext>
    {
        public TodoListDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TodoListDBContext>();
            optionsBuilder.UseMySql(Environment.GetEnvironmentVariable("TODOLIST_DB_CONNECTION"));
            optionsBuilder.EnableSensitiveDataLogging();

            return new TodoListDBContext(optionsBuilder.Options);
        }
    }
}
