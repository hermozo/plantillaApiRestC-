namespace apirest.Helpers
{
    public class Href
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Href self { get; set; }
        public Href first { get; set; }
        public Href last { get; set; }
        public Href next { get; set; }
    }

    public class Meta
    {
        public int totalCount { get; set; }
        public int pageCount { get; set; }
        public int currentPage { get; set; }
        public int perPage { get; set; }
    }

    public class Serializer<T>
    {
        public IEnumerable<T> items { get; set; }
        public Links _links { get; set; }
        public Meta _meta { get; set; }
        public string? messagge { get; set; }
        public int status { get; set; }
    }
}
