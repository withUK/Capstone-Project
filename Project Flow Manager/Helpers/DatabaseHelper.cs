using Microsoft.EntityFrameworkCore;
using Project_Flow_Manager.Enums;
using ProjectFlowManagerModels;

namespace Project_Flow_Manager.Helpers
{
    public static class DatabaseHelper
    {
        public static ProjectAssessmentReport? GetProjectAssessmentReport(int? projectAssessmentReportId, InnovationManagerContext dbContext)
        {
            return dbContext.ProjectAssessmentReport
                            .Where(i => i.Id == projectAssessmentReportId)
                            .Include(i => i.Recommendations)
                            .Include(i => i.Attachments)
                            .Include(i => i.Comments)
                            .FirstOrDefault();
        }

        public static List<ProjectAssessmentReport> GetReportsForDecision(InnovationManagerContext dbContext)
        {
            return dbContext.ProjectAssessmentReport.Where(p => p.Status.Equals(EnumHelper.GetDisplayName(StatusEnum.EligibleForDecision))).ToList();
        }
    }
}
