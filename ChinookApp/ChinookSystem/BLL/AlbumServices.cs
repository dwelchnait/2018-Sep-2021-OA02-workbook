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

        public List<AlbumItem> Albums_GetAlbumsByGenre(int genreid,
                                            int pageNumber,
                                            int pagesize,
                                            out int totalcount)
        {
            IEnumerable<AlbumItem> albums = _context.Tracks
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
                                .OrderBy(x => x.Title);
            //Determine the size of the whole collection w.r.t. the query
            totalcount = albums.Count();
            //limit the actually nummber of records returned from the database
            // depending on the page number and page size
            //calcuate the number of rows to skip
            //page 1= skip 0 rows, page 2 = skip Page Size;
            //  page n = skip (n - 1) * page size
            int skipRows = (pageNumber - 1) * pagesize;
            //the query has yet to be actually executed
            //Linq queries are "Lazy Loaders"
            //We will force the execution on sql by using .ToList()
            //we will inform sql to Skip(n rows) and Take(pagesize rows)
            return albums.Skip(skipRows).Take(pagesize).ToList();
        }
        #endregion
    }
}
