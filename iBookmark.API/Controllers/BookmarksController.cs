using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iBookmark.Model;

namespace iBookmark.API.Controllers
{
    [Route("api/[controller]")]
    public class BookmarksController : Controller
    {
        /// <summary>
        /// Returns all the bookmarks based on userId and folderId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="folderId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType( typeof(BookmarkModel),200)]
        [ProducesResponseType(204)]
        public IActionResult GetBookmark(int userId, int? folderId)
        {

            return new ObjectResult(new BookmarkModel { });
        }
    }
}
