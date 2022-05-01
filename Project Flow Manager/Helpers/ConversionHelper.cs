using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;
using Syncfusion.Pdf;

namespace Project_Flow_Manager.Helpers
{
    public class ConversionHelper
    {
        private FileStream _documentStream;
        private DocIORenderer _render = new DocIORenderer();
        private PdfDocument _pdfDocument;
        private MemoryStream outputStream;

        public PdfDocument ConvertWordToPDF(string FilePath)
        {
            WordDocument wordDocument = new WordDocument(_documentStream, Syncfusion.DocIO.FormatType.Automatic);

            return _pdfDocument;
        }
    }
}
