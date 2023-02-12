using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Maria_Sons.Models
{
	public class ApplicationDbContext : IdentityDbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        public DbSet<CallLogs> CallLogs { get; set; }
    }

    
}

