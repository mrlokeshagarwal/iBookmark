using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBookmark.VM
{
    public class BookmarkVM
    {
        public int BookmarkId { get; set; }

        public int? FolderId { get; set; }
        [Required]
        [Url]
        public string Url { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Url]
        public string IconUrl { get; set; }


    }
}
