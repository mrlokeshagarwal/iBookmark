using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBookmark.Model
{
    public class FolderModel
    {
        public int FolderId { get; set; }

        public string FolderName { get; set; }

        public List<BookmarkModel> Bookmarks { get; set; }
    }
}
