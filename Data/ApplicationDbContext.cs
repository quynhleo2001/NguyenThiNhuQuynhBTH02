using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NguyenThiNhuQuynhBTH02.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<NguyenThiNhuQuynhBTH02.Models.Student> Student { get; set; } = default!;

        public DbSet<NguyenThiNhuQuynhBTH02.Models.Employee> Employee { get; set; } = default!;

         public DbSet<NguyenThiNhuQuynhBTH02.Models.Customer> Customer { get; set; } = default!;

        public DbSet<NguyenThiNhuQuynhBTH02.Models.Person> Person { get; set; } = default!;

        public DbSet<NguyenThiNhuQuynhBTH02.Models. Faculty> Faculty { get; set; } = default!;

       
    }
