using System;
using Microsoft.EntityFrameworkCore;

namespace Maria_Sons.Models
{
	public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        public DbSet<CallLogs> CallLogs { get; set; }
    }

    
}

