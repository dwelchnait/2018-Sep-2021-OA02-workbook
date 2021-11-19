using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChinookSystem.BLL;
using ChinookSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Helpers;

namespace WebApp.Pages
{
    public class BulkProcessModel : PageModel
    {
        #region Private variables and DI constructor
        private readonly TrackServices _trackservices;

        [TempData]
        public string FeedBackMessage { get; set; }

        public BulkProcessModel(TrackServices trackservices)
        {
            _trackservices = trackservices;

        }
        #endregion

        //routed value from another page
        [BindProperty(SupportsGet = true)]
        public string argsearch { get; set; }

        //routed value from another page
        [BindProperty(SupportsGet = true)]
        public string argvalue { get; set; }

        //fouted value local
        [BindProperty(SupportsGet = true)]
        public string playlist { get; set; }

        [BindProperty]
        public int addtrackid { get; set; }

        [BindProperty]
        public List<TrackInfo> trackInfo { get; set; }

        //paging
        private const int PAGE_SIZE = 5;
        public Paginator Pager { get; set; }

        public void OnGet(int? currentPage)
        {
            if(argsearch != null && argvalue != null)
            {
                int pageNumber = currentPage.HasValue ? currentPage.Value : 1;
                PageState current = new(pageNumber, PAGE_SIZE);
                int totalcount;
                trackInfo = _trackservices.Tracks_GetByArtistAlbum(argsearch, argvalue,
                                            pageNumber, PAGE_SIZE, out totalcount);
                Pager = new Paginator(totalcount, current);
            }
        }
    }
}
