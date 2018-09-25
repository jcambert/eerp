using System;
using System.Collections.Generic;
using System.Text;
using Auth.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth.Api.Data
{
    public class PingDbContext : IdentityDbContext<PingUser>
    {
        public PingDbContext(DbContextOptions<PingDbContext> options)
            : base(options)
        {
        }
    }
}
