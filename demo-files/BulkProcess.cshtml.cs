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
    public class BulkProcessModel : PageModel
    {
        #region Private variables and DI constructor
        private readonly TrackServices _trackservices;
        private readonly PlaylistTrackServices _playlisttrackservices;
        [TempData]
        public string FeedBackMessage { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }

        //a get property that returns the result of the lamda action
        public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);
        public bool HasFeedBack => !string.IsNullOrWhiteSpace(FeedBackMessage);

        public BulkProcessModel(TrackServices trackservices, 
                                PlaylistTrackServices playlisttrackservices)
        {
            _trackservices = trackservices;
            _playlisttrackservices = playlisttrackservices;

        }
        #endregion

        [BindProperty(SupportsGet =true)]
        public string argsearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public string argvalue { get; set; }

        [BindProperty]
        public int addtrackid { get; set; }

        [BindProperty(SupportsGet = true)]
        public string playlistname { get; set; }


        [BindProperty]
        public List<TrackInfo> trackInfo { get; set; }

        //for two way binding MAKE SURE you have a bond property
        [BindProperty]
        public List<PLTrackInfo> pltrackInfo { get; set; }

        [BindProperty]
        public List<CTrackInfo> ctrackInfo { get; set; }
        #region Paginator
        private const int PAGE_SIZE = 10;
        public Paginator Pager { get; set; }
        #endregion
        public void OnGet(int? currentPage)
        {
            if (argsearch != null && argvalue !=null)
            {
                //installing paginator
                int pageNumber = currentPage.HasValue ? currentPage.Value : 1;
                // call your service to get the data & the total count
                PageState current = new(pageNumber, PAGE_SIZE);
                int totalcount;
                trackInfo = _trackservices.GetTracksArtistAlbum(argsearch,argvalue,
                                                        pageNumber, PAGE_SIZE,
                                                        out totalcount);
                Pager = new(totalcount, current);
            }
            if (playlistname != null)
            {
                pltrackInfo = _playlisttrackservices.GetPLTracksforUser(playlistname, "HansenB");
            }
        }
        public IActionResult OnPostAddTrack()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(playlistname))
                {
                    throw new Exception($"You need to have a playlist selected first");
                }
                _playlisttrackservices.Add_TrackToPLaylist(playlistname,
                    "HansenB", addtrackid);
            }
            catch (Exception ex)
            {
                ErrorMessage = GetInnerException(ex).Message;
                
            }
            return RedirectToPage(new
            {
                argsearch = argsearch,
                argvalue = argvalue,
                playlistname =playlistname
            }); 
        }
        public IActionResult OnPostFetch()
        {
            if (string.IsNullOrWhiteSpace(playlistname))
            {
                ErrorMessage = $"You need to have a playlist name";
                
            }
            return RedirectToPage(new
            {

                argsearch = argsearch,
                argvalue = argvalue,
                playlistname = playlistname
            });
        }
        public IActionResult OnPostRemove()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(playlistname))
                {
                    ErrorMessage = $"you need to have a playlist name";
                }
                else
                {
                     _playlisttrackservices.DeleteTracks(playlistname, "HansenB", ctrackInfo);
                     FeedBackMessage = "Tracks removed.";
                    //FeedBackMessage = $"{ctrackInfo.Count()}";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = GetInnerException(ex).Message;

            }

            //return Page();
            return RedirectToPage(new
            {

                argsearch = argsearch,
                argvalue = argvalue,
                playlistname = playlistname
            });
        }
        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }

    }
}
