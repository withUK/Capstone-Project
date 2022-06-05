using ProjectFlowManagerModels;
using System.ComponentModel.DataAnnotations;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Project_Flow_Manager.Helpers;

namespace Project_Flow_Manager_Models
{
    public class Innovation : Submission
    {
        public Innovation()
        {
            Status = "New";
        }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "Process Duration")]
        public int ProcessDuration { get; set; }

        [Display(Name = "Number Of People Included")]
        public int NumberOfPeopleIncluded { get; set; }

        [Display(Name = "Process Type")]
        public string ProcessType { get; set; }

        [Display(Name = "Required Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RequiredDate { get; set; }

        [Display(Name = "Process Steps")]
        public virtual ICollection<ProcessStep>? ProcessSteps { get; set; }
        public virtual ICollection<Technology>? Technologies { get; set; }
        
        public int? ApprovalId { get; set; }
        public virtual Approval? Approval { get; set; }

        public WordDocument GenerateWordDocument()
        {
            WordDocument document = DocumentCreationHelper.CreateDocument();

            WSection frontPage = DocumentCreationHelper.CreatePage(document);
            WSection innovationPage = DocumentCreationHelper.CreatePage(document);

            DocumentCreationHelper.CreateHeader(frontPage, this.Title);
            DocumentCreationHelper.AddTextToPage(frontPage, "Title", this.Title);
            innovationPage = CreateSubmissionContentPage(innovationPage, this.Title);

            return document;
        }

        public WSection CreateSubmissionContentPage(WSection innovationPage, string title)
        {
            DocumentCreationHelper.CreateHeader(innovationPage, title);

            DocumentCreationHelper.AddTextToPage(innovationPage, "Heading 1", "Innovation Submission");
            if (!string.IsNullOrEmpty(Title))
            {
                DocumentCreationHelper.AddTextToPage(innovationPage, "Normal", Title);
            }

            if (!string.IsNullOrEmpty(CreatedBy))
            {
                DocumentCreationHelper.AddTextToPage(innovationPage, "Normal", ($"Submitted by : {CreatedBy}"));
            }

            if (!string.IsNullOrEmpty(Created.ToString()))
            {
                DocumentCreationHelper.AddTextToPage(innovationPage, "Normal", ($"Submitted on : {Created.ToString("dddd, MMMM dd yyyy")}"));
            }

            if (!string.IsNullOrEmpty(Description))
            {
                DocumentCreationHelper.AddTextToPage(innovationPage, "Heading 2", "Description");
                DocumentCreationHelper.AddTextToPage(innovationPage, "Normal", Description);
            }

            if (!string.IsNullOrEmpty(ProcessType))
            {
                DocumentCreationHelper.AddTextToPage(innovationPage, "Normal", ($"Process type : {ProcessType}"));
            }
            if (!string.IsNullOrEmpty(ProcessDuration.ToString()))
            {
                DocumentCreationHelper.AddTextToPage(innovationPage, "Normal", ($"Process duration : {ProcessDuration}"));
            }
            if (!string.IsNullOrEmpty(NumberOfPeopleIncluded.ToString()))
            {
                DocumentCreationHelper.AddTextToPage(innovationPage, "Normal", ($"Number of people included : {NumberOfPeopleIncluded}"));
            }

            if (Technologies != null && Technologies.Count > 0)
            {
                DocumentCreationHelper.AddTextToPage(innovationPage, "Heading 2", "Technologies");
                string tech = null;
                for (int i = 0; i < Technologies.Count; i++)
                {
                    if (i == Technologies.Count - 1)
                    {
                        tech = string.Concat(tech, Technologies.ElementAt(i).Name);
                    }
                    else
                    {
                        tech = string.Concat(tech, ", ", Technologies.ElementAt(i).Name);
                    }
                }
                DocumentCreationHelper.AddTextToPage(innovationPage, "Normal", tech);
            }

            if (ProcessSteps != null && ProcessSteps.Count > 0)
            {
                DocumentCreationHelper.AddTextToPage(innovationPage, "Heading 2", "Process Steps");
                DocumentCreationHelper.AddProcessStepsToPage(innovationPage, ProcessSteps.ToList());
            }

            return innovationPage;
        }
    }
}
