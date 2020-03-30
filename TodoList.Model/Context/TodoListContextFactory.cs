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
            var connectionString = "Your connection string";
            var optionsBuilder = new DbContextOptionsBuilder<TodoListDBContext>();
            optionsBuilder.UseMySql(connectionString);
            optionsBuilder.EnableSensitiveDataLogging();

            return new TodoListDBContext(optionsBuilder.Options);
        }
    }
}
