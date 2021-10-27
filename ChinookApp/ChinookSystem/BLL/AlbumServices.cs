using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region Additional 
using ChinookSystem.Models;
using ChinookSystem.DAL;
#endregion

namespace ChinookSystem.BLL
{
    public class AlbumServices
    {
        #region Constructor and DI variable setup
        private readonly ChinookContext _context;

        internal AlbumServices(ChinookContext context)
        {
            _context = context;
        }
        #endregion

        #region Queries
        public AlbumItem Albums_GetAlbumById(int albumid)
        {
            //linq to entity therefore you need to access the DbSet in your
            //      context class
            AlbumItem info = _context.Albums
                            .Where(x => x.AlbumId == albumid)
                            .Select(x => new AlbumItem
                            {
                                AlbumId = x.AlbumId,
                                Title = x.Title,
                                ArtistId = x.ArtistId,
                                ReleaseYear = x.ReleaseYear,
                                ReleaseLabel = x.ReleaseLabel
                            }).FirstOrDefault();
            return info;
        }

        public List<AlbumItem> Albums_GetAlbumsByGenre(int genreid)
        {
            List<AlbumItem> albums = _context.Tracks
                                .Where(x => x.GenreId == genreid &&
                                        x.AlbumId.HasValue)
                                .Select(x => new AlbumItem
                                {
                                    AlbumId = (int)x.AlbumId,
                                    Title = x.Album.Title,
                                    ArtistId = x.Album.ArtistId,
                                    ReleaseYear = x.Album.ReleaseYear,
                                    ReleaseLabel = x.Album.ReleaseLabel
                                })
                                .Distinct()
                                .OrderBy(x => x.Title)
                                .ToList();
            return albums;
        }
        #endregion
    }
}
