using Syncfusion.DocIO.DLS;

namespace Project_Flow_Manager_Models
{
    public interface ISubmission
    {
        public WordDocument GenerateWordDocument();

        public WSection CreateSubmissionContentPage(WSection innovationPage, string title);
    }
}
