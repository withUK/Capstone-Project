using System.ComponentModel.DataAnnotations;

namespace Project_Flow_Manager.Enums
{
    public enum SubmissionTypeEnum
    {
        [Display(Name = "Innovations")]
        Innovation,
        [Display(Name = "ProjectAssessmentReports")]
        ProjectAssessmentReport,
        [Display(Name = "Recommendations")]
        Recommendation,
        [Display(Name = "ResourceRequests")]
        ResourceRequest
    }
}
