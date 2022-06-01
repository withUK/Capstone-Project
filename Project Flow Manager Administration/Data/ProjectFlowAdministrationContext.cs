#nullable disable
using Microsoft.EntityFrameworkCore;
using Project_Flow_Manager_Models;

public class ProjectFlowAdministrationContext : DbContext
{
    public ProjectFlowAdministrationContext(DbContextOptions<ProjectFlowAdministrationContext> options)
        : base(options)
    {
    }

    public DbSet<Technology> Technology { get; set; }

    public DbSet<Status> Status { get; set; }

    public DbSet<ProcessType> ProcessType { get; set; }

    public DbSet<EffortMeasure>? EffortMeasure { get; set; }

    public DbSet<Team>? Team { get; set; }
}
