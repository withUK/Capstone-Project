#nullable disable
using Microsoft.EntityFrameworkCore;
using Project_Flow_Manager_Models;
using ProjectFlowManagerModels;

public class InnovationManagerContext : DbContext
    {
        public InnovationManagerContext (DbContextOptions<InnovationManagerContext> options)
            : base(options)
        {
        }

    public DbSet<Innovation> Innovation { get; set; }

    public DbSet<ProcessStep> ProcessStep { get; set; }

    public DbSet<ProjectFlowManagerModels.Approval> Approval { get; set; }

    public DbSet<ProjectFlowManagerModels.ProjectAssessmentReport> ProjectAssessmentReport { get; set; }

    public DbSet<Project_Flow_Manager_Models.Recommendation> Recommendation { get; set; }
}
