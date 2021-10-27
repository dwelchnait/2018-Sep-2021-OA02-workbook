using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region Additional Namespaces
using ChinookSystem.BLL;
using ChinookSystem.Models;
using Microsoft.Extensions.Logging;
#endregion

namespace WebApp.Pages
{
    public class AlbumListTableQueryModel : PageModel
    {
        #region Private variables and DI constructor
        private readonly AlbumServices _albumservices;
        private readonly GenreServices _genreservices;
        
        [TempData]
        public string FeedBackMessage { get; set; }

        public AlbumListTableQueryModel(AlbumServices albumservices, 
                                        GenreServices genreservices)
        {
            _albumservices = albumservices;
            _genreservices = genreservices;
            
        }
        #endregion

        [BindProperty]
        public List<AlbumItem> Albums { get; set; }

        [BindProperty]
        public List<SelectionList> Genres  { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? genreid { get; set; }

        
        public void OnGet()
        {
            //for your drop down list, you must retrieve the list on each pass
            Genres = _genreservices.Genre_List();
            if (genreid.HasValue && genreid > 0)
            {
                Albums = _albumservices.Albums_GetAlbumsByGenre((int)genreid);
            }
        }

        public IActionResult OnPost()
        {
            if(!genreid.HasValue || genreid == 0)
            {
                FeedBackMessage = "You did not select a genre";
            }
            return RedirectToPage(new { genreid = genreid });
        }
    }
}
