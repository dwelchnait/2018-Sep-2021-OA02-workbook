using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region Additional Namespaces
using ChinookSystem.BLL;
using ChinookSystem.Models;
#endregion


namespace WebApp.Pages
{
    public class ArtistQueryModel : PageModel
    {
        #region Constructor, Feedback and DI
        private readonly ArtistServices _artistservices;
        public ArtistQueryModel(ArtistServices artistservices)
        {
            _artistservices = artistservices;
        }

        [TempData]
        public string FeedBack { get; set; }
        #endregion

        [BindProperty(SupportsGet = true)]
        public int? artistid { get; set; }
        public List<SelectionList> allArtists { get; set; }
        public void OnGet()
        {
            allArtists = _artistservices.Artists_List();
        }
        public IActionResult OnPost()
        {
            FeedBack = $"You selected the artist with an id of {artistid.ToString()}";
            return RedirectToPage(new { artistid = artistid });
        }

    }
}
