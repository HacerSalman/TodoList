using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Model.Entities;

namespace TodoList.Model.Context
{
    public class TodoListDBContext:DbContext
    {
        #region Constructor

        public TodoListDBContext(DbContextOptions<TodoListDBContext> options)
            : base(options)
        {

        }

        #endregion Constructor

        #region ModelCreating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User       
            modelBuilder.Entity<User>()
     .Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().HasIndex(u => u.CreatedDate);
            modelBuilder.Entity<User>().HasIndex(u => u.UpdatedDate);
            #endregion

            #region List
            modelBuilder.Entity<List>()
.Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<List>().HasOne(u => u.ListType).WithMany()
          .HasForeignKey(u => u.Type)
           .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<List>().HasIndex(u => u.CreatedDate);
            modelBuilder.Entity<List>().HasIndex(u => u.UpdatedDate);
            #endregion

            #region ListType
            modelBuilder.Entity<ListType>()
  .Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ListType>().HasIndex(u => u.Name).IsUnique();
            modelBuilder.Entity<ListType>().HasIndex(u => u.CreatedDate);
            modelBuilder.Entity<ListType>().HasIndex(u => u.UpdatedDate);
            #endregion          

        }

        #endregion ModelCreating


        #region OnConfiguring

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        #endregion OnConfiguring


        #region DbSets
        public DbSet<User> User { get; set; }
        public DbSet<List> List { get; set; }
        public DbSet<ListType> ListType { get; set; }       
        #endregion DbSets   
    }
}
