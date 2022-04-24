#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project_Flow_Manager_Models;

    public class ProjectFlowAdministrationContext : DbContext
    {
        public ProjectFlowAdministrationContext (DbContextOptions<ProjectFlowAdministrationContext> options)
            : base(options)
        {
        }

        public DbSet<Project_Flow_Manager_Models.Technology> Technology { get; set; }

        public DbSet<Project_Flow_Manager_Models.Status> Status { get; set; }

        public DbSet<Project_Flow_Manager_Models.ProcessType> ProcessType { get; set; }
    }
