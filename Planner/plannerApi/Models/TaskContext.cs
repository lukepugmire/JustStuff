using Microsoft.EntityFrameworkCore;

namespace plannerApi.Models;

public class taskContext : DbContext
{
    public taskContext(DbContextOptions<taskContext> options)
        : base(options)
    {
    }
    public DbSet<taskModel> Tasks { get; set; } = null!;
}

