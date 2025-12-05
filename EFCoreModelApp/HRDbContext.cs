using Microsoft.EntityFrameworkCore;
using EFCoreModelApp.Models;

namespace EFCoreModelApp;

public class HRDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data source = hr_database.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId);

        modelBuilder.Entity<Department>().HasData(
            new Department { DepartmentId = 1, DepartmentName = "Engineering" },
            new Department { DepartmentId = 2, DepartmentName = "Human Resources" },
            new Department { DepartmentId = 3, DepartmentName = "Marketing" }
        );

        modelBuilder.Entity<Employee>().HasData(
            // Employees in the Engineering department (DepartmentId = 1)
            new Employee { EmployeeId = 1, Name = "Alice Johnson", Salary = 90000, DepartmentId = 1 },
            new Employee { EmployeeId = 2, Name = "Bob Williams", Salary = 85000, DepartmentId = 1 },

            // Employee in the Human Resources department (DepartmentId = 2)
            new Employee { EmployeeId = 3, Name = "Charlie Brown", Salary = 60000, DepartmentId = 2 },

            // Employee in the Marketing department (DepartmentId = 3)
            new Employee { EmployeeId = 4, Name = "Diana Prince", Salary = 70000, DepartmentId = 3 }
        );
    }
}