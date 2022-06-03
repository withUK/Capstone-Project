using Microsoft.EntityFrameworkCore;
using Project_Flow_Manager.Enums;
using Project_Flow_Manager_Models;
using ProjectFlowManagerModels;

namespace Project_Flow_Manager.Helpers
{
    public static class DatabaseHelper
    {
        public static ProjectAssessmentReport? GetProjectAssessmentReport(int? projectAssessmentReportId, InnovationManagerContext dbContext)
        {
            return dbContext.ProjectAssessmentReport
                .Include(p => p.Approvals)
                .Include(p => p.Recommendations)
                .ThenInclude(r => r.Effort)
                .Include(p => p.Innovation)
                .Include(p => p.Innovation.ProcessSteps)
                .Include(p => p.Comments)
                .Include(p => p.Tags)
                .Include(p => p.Innovation.Approval)
                .FirstOrDefault(m => m.Id == projectAssessmentReportId);
        }

        public static List<Innovation> GetCurrentUserInnovationSubmissions(string currentUser, InnovationManagerContext dbContext)
        {
            return dbContext.Innovation
                .Where(p => p.CreatedBy.Equals(currentUser))
                .ToList();
        }

        public static List<Innovation> GetInnovationSubmissionsForApproval(InnovationManagerContext dbContext)
        {
            return dbContext.Innovation
                .Where(i => i.Status.Equals(EnumHelper.GetDisplayName(StatusEnum.New)))
                .ToList();
        }

        public static List<ProjectAssessmentReport> GetAssessmentReports(InnovationManagerContext dbContext)
        {
            return dbContext.ProjectAssessmentReport
                .Where(p => p.Status.Equals(EnumHelper.GetDisplayName(StatusEnum.AwaitingFurtherRecommendations)) || p.Status.Equals(EnumHelper.GetDisplayName(StatusEnum.New)) || p.Status.Equals(EnumHelper.GetDisplayName(StatusEnum.EligibleForDecision)))
                .Include(p => p.Approvals)
                .Include(p => p.Recommendations)
                .ThenInclude(r => r.Effort)
                .Include(p => p.Innovation)
                .Include(p => p.Innovation.ProcessSteps)
                .Include(p => p.Comments)
                .Include(p => p.Tags)
                .Include(p => p.Innovation.Approval)
                .ToList();
        }

        public static List<ProjectAssessmentReport> GetReportsForDecision(InnovationManagerContext dbContext)
        {
            return dbContext.ProjectAssessmentReport
                .Where(p => p.Status.Equals(EnumHelper.GetDisplayName(StatusEnum.EligibleForDecision)) || p.Status.Equals(EnumHelper.GetDisplayName(StatusEnum.AwaitingAdditionalApproval)))
                .Include(p => p.Approvals)
                .Include(p => p.Recommendations)
                .ThenInclude(r => r.Effort)
                .Include(p => p.Innovation)
                .Include(p => p.Innovation.ProcessSteps)
                .Include(p => p.Comments)
                .Include(p => p.Tags)
                .Include(p => p.Innovation.Approval)
                .ToList();
        }
    }
}
