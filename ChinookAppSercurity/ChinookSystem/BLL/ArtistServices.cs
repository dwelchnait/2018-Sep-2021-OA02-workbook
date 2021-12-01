using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional 
using ChinookSystem.Models;
using ChinookSystem.DAL;
using ChinookSystem.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
#endregion


namespace ChinookSystem.BLL
{
    public class ArtistServices
    {
        #region Constructor and DI variable setup
        private readonly ChinookContext _context;

        internal ArtistServices(ChinookContext context)
        {
            _context = context;
        }
        #endregion

        #region Queries
        public List<SelectionList> Artists_List()
        {
            //linq to entity therefore you need to access the DbSet in your
            //      context class
            IEnumerable<SelectionList> info = _context.Artists
                            .Select(x => new SelectionList
                            {
                                ValueField = x.ArtistId,
                                DisplayField = x.Name
                            })
                            .OrderBy(x => x.DisplayField);
            return info.ToList();
        }

        #endregion
    }
}
