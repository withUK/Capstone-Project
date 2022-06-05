using Project_Flow_Manager_Models;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;

namespace Project_Flow_Manager.Helpers
{
    public static class DocumentCreationHelper
    {
        private const float FONTSIZE = 11f;
        private const string FONTFAMILY = "Verdana";

        public static WordDocument CreateDocument()
        {
            WordDocument document = new WordDocument();
            SetStyles(document);
            return document;
        }

        public static WSection CreatePage(WordDocument document)
        {
            WSection page = document.AddSection() as WSection;
            page.PageSetup.Margins.All = 70;
            page.PageSetup.PageSize = new Syncfusion.Drawing.SizeF(612, 792);
            return page;
        }

        public static void CreateHeader(WSection page, string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                FileStream imageStream = new FileStream("wwwroot/img/lcc-logo-color.png", FileMode.Open, FileAccess.Read);
                
                IWParagraph paragraph = page.HeadersFooters.Header.AddParagraph();
                paragraph.ApplyStyle("Header");
                paragraph.AppendPicture(imageStream);
                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Right;
                paragraph = page.HeadersFooters.Header.AddParagraph();
                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Right;

                WTextRange textRange = paragraph.AppendText(title) as WTextRange;
            }
        }

        public static void AddTextToPage(WSection page, string style, string text)
        {
            IWParagraph paragraph = page.AddParagraph();
            paragraph.ApplyStyle(style);
            WTextRange textRange = paragraph.AppendText(text) as WTextRange;
        }

        public static void AddProcessStepsToPage(WSection page, List<ProcessStep> steps)
        {
            IWParagraph paragraph = page.AddParagraph();

            IWTable table = page.AddTable();
            table.ResetCells(steps.Count, 2);
            table.TableFormat.Borders.BorderType = BorderStyle.None;
            table.TableFormat.IsAutoResized = true;

            for (int i = 0; i < steps.Count; i++)
            {
                paragraph = table[i, 0].AddParagraph();
                paragraph.AppendText(steps[i].OrderPosition.ToString());
                paragraph = table[i, 1].AddParagraph();
                paragraph.AppendText(steps[i].Value.ToString());
            }
        }

        public static void AddPictureToPage(WSection page, string filePath, string caption)
        {
            IWParagraph paragraph = page.AddParagraph();
            paragraph.ApplyStyle("Image Caption");
            FileStream imageStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            IWPicture picture = paragraph.AppendPicture(imageStream);
            picture.TextWrappingStyle = TextWrappingStyle.Square;
            picture.VerticalOrigin = VerticalOrigin.Paragraph;
            picture.VerticalPosition = 4.5f;
            picture.HorizontalOrigin = HorizontalOrigin.Column;
            picture.HorizontalPosition = -2.15f;
            AddTextToPage(page, "Image Caption", caption);
        }

        public static MemoryStream SaveDocumentToStream(WordDocument document)
        {
            MemoryStream stream = new MemoryStream();
            document.Save(stream, FormatType.Docx);
            stream.Position = 0;
            return stream;
        }

        private static void SetStyles(WordDocument document)
        {
            WParagraphStyle normal = document.AddParagraphStyle("Normal") as WParagraphStyle;
            normal.CharacterFormat.FontName = FONTFAMILY;
            normal.CharacterFormat.FontSize = FONTSIZE;
            normal.ParagraphFormat.BeforeSpacing = 0;
            normal.ParagraphFormat.AfterSpacing = 8;
            normal.ParagraphFormat.LineSpacing = 13.8f;

            WParagraphStyle header = document.AddParagraphStyle("Header") as WParagraphStyle;
            normal.CharacterFormat.FontName = FONTFAMILY;
            normal.CharacterFormat.FontSize = FONTSIZE - 1;
            normal.ParagraphFormat.BeforeSpacing = 0;
            normal.ParagraphFormat.AfterSpacing = 8;
            normal.ParagraphFormat.LineSpacing = 13.8f;

            WParagraphStyle title = document.AddParagraphStyle("Title") as WParagraphStyle;
            title.ApplyBaseStyle("Normal");
            title.CharacterFormat.FontName = FONTFAMILY;
            title.CharacterFormat.FontSize = FONTSIZE * 6;
            title.CharacterFormat.TextColor = Syncfusion.Drawing.Color.FromArgb(46, 116, 181);
            title.ParagraphFormat.BeforeSpacing = 350;
            title.ParagraphFormat.AfterSpacing = 0;
            title.ParagraphFormat.Keep = true;
            title.ParagraphFormat.KeepFollow = true;
            title.ParagraphFormat.OutlineLevel = OutlineLevel.Level1;

            WParagraphStyle h1 = document.AddParagraphStyle("Heading 1") as WParagraphStyle;
            h1.ApplyBaseStyle("Normal");
            h1.CharacterFormat.FontName = FONTFAMILY;
            h1.CharacterFormat.FontSize = (float)(FONTSIZE + 11);
            h1.CharacterFormat.TextColor = Syncfusion.Drawing.Color.FromArgb(46, 116, 181);
            h1.ParagraphFormat.BeforeSpacing = 12;
            h1.ParagraphFormat.AfterSpacing = 0;
            h1.ParagraphFormat.Keep = true;
            h1.ParagraphFormat.KeepFollow = true;
            h1.ParagraphFormat.OutlineLevel = OutlineLevel.Level1;

            WParagraphStyle h2 = document.AddParagraphStyle("Heading 2") as WParagraphStyle;
            h2.ApplyBaseStyle("Normal");
            h2.CharacterFormat.FontName = FONTFAMILY;
            h2.CharacterFormat.FontSize = (float)(FONTSIZE + 9);
            h2.CharacterFormat.TextColor = Syncfusion.Drawing.Color.FromArgb(46, 116, 181);
            h2.ParagraphFormat.BeforeSpacing = 10;
            h2.ParagraphFormat.AfterSpacing = 0;
            h2.ParagraphFormat.Keep = true;
            h2.ParagraphFormat.KeepFollow = true;
            h2.ParagraphFormat.OutlineLevel = OutlineLevel.Level1;

            WParagraphStyle imageCaption = document.AddParagraphStyle("Image Caption") as WParagraphStyle;
            imageCaption.ApplyBaseStyle("Normal");
            imageCaption.CharacterFormat.FontName = FONTFAMILY;
            imageCaption.CharacterFormat.FontSize = (float)(FONTSIZE - 2);
            imageCaption.CharacterFormat.TextColor = Syncfusion.Drawing.Color.FromArgb(46, 116, 181);
            imageCaption.ParagraphFormat.BeforeSpacing = 2;
            imageCaption.ParagraphFormat.AfterSpacing = 5;
            imageCaption.ParagraphFormat.Keep = true;
            imageCaption.ParagraphFormat.KeepFollow = true;
            imageCaption.ParagraphFormat.OutlineLevel = OutlineLevel.Level1;
        }
    }
}
