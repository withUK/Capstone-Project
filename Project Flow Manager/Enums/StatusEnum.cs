using System.ComponentModel.DataAnnotations;

namespace Project_Flow_Manager.Enums
{
    public enum StatusEnum
    {
        [Display(Name = "New")]
        New,
        [Display(Name = "Eligible for descision")]
        EligibleForDescision,
        [Display(Name = "Awaiting further recommendations")]
        AwaitingFurtherRecommendations,
        [Display(Name = "Awaiting allocation of resourse")]
        AwaitingAllocationOfResource,
        [Display(Name = "Awaiting additional approval")]
        AwaitingAdditionalApproval,
        [Display(Name = "Passed to developement")]
        PassedToDevelopement
    }
}
