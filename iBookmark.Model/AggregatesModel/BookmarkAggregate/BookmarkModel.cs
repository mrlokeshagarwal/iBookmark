namespace iBookmark.Domain.AggregatesModel.BookmarkAggregate
{
    using iBookmark.Model.AggregatesModel;


    public class BookmarkModel: Entity, IAggregateRoot
    {
        private int _containerId;
        private string _url;
        private string _title;
        private string _iconUrl;

        public int BookmarkId { get; private set; }

        public int ContainerId { get; private set; }
        public string Url { get; private set; }
        public string Title { get; private set; }
        public string IconUrl { get; private set; }

        public void Add(string url, string title, string iconUrl, int? containerId)
        {
            _url = url;
            _title = title;
            _iconUrl = iconUrl;
            _containerId = containerId ?? 0;
        }
    }
}
