namespace iBookmark.Domain.AggregatesModel.BookmarkAggregate
{
    using iBookmark.Model.AggregatesModel;


    public class BookmarkModel: Entity, IAggregateRoot
    {
        public int BookmarkId { get; set; }
        public int ContainerId { get; set; }
        public string BookmarkUrl { get; set; }
        public string BookmarkTitle { get; set; }
        public string BookmarkIconUrl { get; set; }
        public int UserId { get; set; }
    }
}
