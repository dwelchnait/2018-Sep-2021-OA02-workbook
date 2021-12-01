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
        private readonly PlaylistTrackServices _playlisttrackservices;

        [TempData]
        public string FeedBackMessage { get; set; }
        public bool HasFeedBack => !string.IsNullOrWhiteSpace(FeedBackMessage);

        [TempData]
        public string ErrorMessage { get; set; }

        public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

        public BulkProcessModel(TrackServices trackservices,
                                PlaylistTrackServices playlisttrackservices)
        {
            _trackservices = trackservices;
            _playlisttrackservices = playlisttrackservices;
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
        public string playlistname { get; set; }

        [BindProperty]
        public int addtrackid { get; set; }

        [BindProperty]
        public List<TrackInfo> trackInfo { get; set; }

        [BindProperty]
        public List<PLTrackInfo> pltrackInfo { get; set; }

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

            if(playlistname != null)
            {
                pltrackInfo = _playlisttrackservices.Tracks_GetPlaylistforUser(playlistname, "HansenB");
            }
        }

        public IActionResult OnPostFetch()
        {
            if (string.IsNullOrWhiteSpace(playlistname))
            {
                ErrorMessage = "Enter a playlist name to fetch.";
            }
            return RedirectToPage(new
            {
                argsearch = argsearch,
                argvalue = argvalue,
                playlistname = playlistname
            });
        }
        
        public IActionResult OnPostAddTrack()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(playlistname))
                {
                    throw new Exception("You need to have a playlist select first");
                }
                //TODO: add track to playlist
                _playlisttrackservices.PlaylistTrack_AddTrack(playlistname,
                                                              "HansenB",
                                                              addtrackid);
                FeedBackMessage = "adding the track";
            }
            catch(Exception ex)
            {
                ErrorMessage = GetInnerException(ex).Message;
            }
            return RedirectToPage(new
            {
                argsearch = argsearch,
                argvalue = argvalue,
                playlistname = playlistname
            });
        }

        private Exception GetInnerException(Exception ex)
        {
            while(ex.InnerException !=null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }
    }
}
