#nullable disable
using Microsoft.EntityFrameworkCore;
using Project_Flow_Manager_Models;
using ProjectFlowManagerModels;

/// <summary>
/// TODO
/// </summary>
public class InnovationManagerContext : DbContext
{
    public InnovationManagerContext(DbContextOptions<InnovationManagerContext> options)
        : base(options)
    {
    }

    public DbSet<Innovation> Innovation { get; set; }

    public DbSet<ProcessStep> ProcessStep { get; set; }

    public DbSet<Approval> Approval { get; set; }

    public DbSet<ProjectAssessmentReport> ProjectAssessmentReport { get; set; }

    public DbSet<Recommendation> Recommendation { get; set; }

    public DbSet<Effort>? Effort { get; set; }

    public DbSet<ResourceRequest>? ResourceRequest { get; set; }

    public DbSet<TeamResource>? TeamResource { get; set; }

    public DbSet<TechnologyResource>? TechnologyResource { get; set; }

    public DbSet<Comment>? Comment { get; set; }

    public DbSet<Tag>? Tag { get; set; }
}
