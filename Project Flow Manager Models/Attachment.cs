namespace ProjectFlowManagerModels
{
    public class Attachment
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public string Type { get; set; }
        public string Filepath { get; set; }
        public int Filesize { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
