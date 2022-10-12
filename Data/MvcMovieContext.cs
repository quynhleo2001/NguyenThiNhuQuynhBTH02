using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NguyenThiNhuQuynhBTH02.Models;

    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<NguyenThiNhuQuynhBTH02.Models.Student> Student { get; set; } = default!;

        public DbSet<NguyenThiNhuQuynhBTH02.Models.Employee>? Employee { get; set; }

        public DbSet<NguyenThiNhuQuynhBTH02.Models.Customer>? Customer { get; set; }

        public DbSet<NguyenThiNhuQuynhBTH02.Models.Person>? Person { get; set; }
    }
