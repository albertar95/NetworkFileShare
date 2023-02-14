namespace NetworkFileShareUI.ViewModels
{
    public class FileViewModel
    {
        public Guid Id { get; set; }
        public Guid FolderId { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public string FileSource { get; set; }
        public string FileExt { get; set; }
    }
}
