#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _252936_MVC.Models;

namespace _252936_MVC.Data
{
    public class _252936_MVCContext : DbContext
    {
        public _252936_MVCContext (DbContextOptions<_252936_MVCContext> options)
            : base(options)
        {
        }

        public DbSet<_252936_MVC.Models.Movie> Movie { get; set; }
    }
}
