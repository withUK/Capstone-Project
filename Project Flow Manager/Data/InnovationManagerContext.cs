#nullable disable
using Microsoft.EntityFrameworkCore;
using Project_Flow_Manager_Models;

public class InnovationManagerContext : DbContext
    {
        public InnovationManagerContext (DbContextOptions<InnovationManagerContext> options)
            : base(options)
        {
        }

    public DbSet<Innovation> Innovation { get; set; }

    public DbSet<ProcessStep> ProcessStep { get; set; }
}
