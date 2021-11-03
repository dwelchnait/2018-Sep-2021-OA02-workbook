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
using WebApp.Helpers;
#endregion


namespace WebApp.Pages
{
    public class CRUDAlbumModel : PageModel
    {
        #region Service variable(s), FeedBack and DI constructor
        private readonly AlbumServices _albumservices;
        private readonly ArtistServices _artistservices;

        [TempData]
        public string FeedBackMessage { get; set; }

        public CRUDAlbumModel(AlbumServices albumservices,
                              ArtistServices artistservices)
        {
            _albumservices = albumservices;
            _artistservices = artistservices;
        }
        #endregion

        #region Form Variables
        [BindProperty(SupportsGet = true)]
        public int? albumid { get; set; }

        [BindProperty]
        public List<SelectionList> Artists { get; set; }

        [BindProperty]
        public AlbumItem Album { get; set; }
        #endregion
        public void OnGet()
        {
            Artists = _artistservices.Artists_List();
            if(albumid.HasValue)
            {
                Album = _albumservices.Albums_GetAlbumById((int)albumid);
            }
        }
        
        public IActionResult OnPostUpdate()
        {
            try
            {
                int rowaffected = _albumservices.UpdateAlbum(Album);
                if(rowaffected > 0)
                {
                    FeedBackMessage = "Album has been updated";
                }
                else
                {
                    FeedBackMessage = "No album update. Album does not exist";
                }
            }
            catch(Exception ex)
            {
                FeedBackMessage = ex.Message;
            }
            return RedirectToPage(new {albumid = albumid });
        }
        public IActionResult OnPostDelete()
        {
            try
            {
                int rowaffected = _albumservices.DeleteAlbum(Album);
                if (rowaffected > 0)
                {
                    FeedBackMessage = "Album has been removed";
                }
                else
                {
                    FeedBackMessage = "No album remove. Album does not exist";
                }
            }
            catch (Exception ex)
            {
                FeedBackMessage = ex.Message;
            }
            return RedirectToPage(new { albumid = albumid });
        }
    }
}
