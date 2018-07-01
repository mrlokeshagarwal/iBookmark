using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBookmark.Model
{
    public class BookmarkModel
    {
        public int BookmarkId { get; set; }

        public int? FolderId { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string IconUrl { get; set; }
    }
}
