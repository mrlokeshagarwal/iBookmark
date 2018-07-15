namespace iBookmark.API.Controllers
{
    using iBookmark.Domain.AggregatesModel.BookmarkAggregate;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;

    //[Authorize(Policy = "AddBookmark")]
    [Route("Bookmarks")]
    public class BookmarksController : Controller
    {
        private IBookmarkRepository _bookmarkRepository;
        public BookmarksController(IBookmarkRepository bookmarkRepository)
        {
            this._bookmarkRepository = bookmarkRepository;
        }
        /// <summary>
        /// Returns all the bookmarks based on userId and folderId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="containerId"></param>
        /// <returns></returns>
        [HttpGet("{userId}/{containerId}")]
        [ProducesResponseType(typeof(BookmarkModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBookmarkAsync(int userId, int containerId)
        {
            var bookmarks = await this._bookmarkRepository.GetBookmarksAsync(userId, containerId).ConfigureAwait(false);
            if (bookmarks.Any())
                return new ObjectResult(bookmarks);
            else
                return new ObjectResult(new List<BookmarkModel>());
        }

        /// <summary>
        /// Inserts new bookmark into system
        /// </summary>
        /// <param name="bookmark"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> InsertBookmark([FromBody]BookmarkModel bookmark)
        {
            bookmark.BookmarkId = 0;
            int bookmarkId = await this._bookmarkRepository.InsertUpdateBookmarkAsync(bookmark).ConfigureAwait(false);
            return Ok(bookmarkId);
        }

        /// <summary>
        /// Updates Bookmark details
        /// </summary>
        /// <param name="bookmark"></param>
        /// <param name="bookmarkId"></param>
        /// <returns></returns>
        [HttpPut("{bookmarkId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateBookmark([FromBody]BookmarkModel bookmark, int bookmarkId)
        {
            bookmark.BookmarkId = bookmarkId;
            await this._bookmarkRepository.InsertUpdateBookmarkAsync(bookmark).ConfigureAwait(false);
            return Ok(bookmarkId);
        }
    }
}
