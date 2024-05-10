namespace StylishWebsiteBE.Domain.ValueObjects {
    public class ImageValueObject {
        public string Uid { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Status => "done";
    }
}