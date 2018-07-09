namespace iBookmark.API.Controllers
{
    using iBookmark.Domain.AggregatesModel.BookmarkAggregate;
    using Microsoft.AspNetCore.Mvc;

    [Route("Bookmarks")]
    public class BookmarksController : Controller
    {
        /// <summary>
        /// Returns all the bookmarks based on userId and folderId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="folderId"></param>
        /// <returns></returns>
        [HttpGet("{userId}/{folderId}")]
        [ProducesResponseType( typeof(BookmarkModel),200)]
        [ProducesResponseType(204)]
        public IActionResult GetBookmark(int userId, int? folderId)
        {

            return new ObjectResult(new BookmarkModel { });
        }
    }
}
