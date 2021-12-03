
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
    public class AboutServices
    {
        #region Constructor and DI variable setup
        private readonly ChinookContext _context;

        internal AboutServices(ChinookContext context)
        {
            _context = context;
        }
        #endregion

        #region Queries
        public DatabaseVersion GetDataVersion()
        {
            DatabaseVersion results = _context.DbVersions
                                .Select(x => new DatabaseVersion
                                {
                                    Version = x.Major + "." + x.Minor + "." + x.Build,
                                    ReleaseDate = x.ReleaseDate
                                }
                                )
                                .FirstOrDefault();
            return results;
        }

        public List<NamedColor> ListHMTLColors()
        {
            List<NamedColor> colors = new List<NamedColor> {
                 new NamedColor("rgb(255, 0, 0)", "#FF0000", "RED", 1, true),
                new NamedColor("rgb(255, 192, 203)", "#FFC0CB", "PINK", 1, true),
                new NamedColor("rgb(255, 165, 0)", "#FFA500", "ORANGE", 1, false),
                new NamedColor("rgb(255, 255, 0)", "#FFFF00", "YELLOW", 1, true),
                new NamedColor("rgb(128, 0, 128)", "#800080", "PURPLE", 1, true),
                new NamedColor("rgb(0, 128, 0)", "#008000", "GREEN", 2, true),
                new NamedColor("rgb(0, 0, 255)", "#0000FF", "BLUE", 3, false),
                new NamedColor("rgb(165, 42, 42)", "#A52A2A", "BROWN", 4, false),
                new NamedColor("rgb(255, 255, 255)", "#FFFFFF", "WHITE", 4, true),
                new NamedColor("rgb(128, 128, 128)", "#808080", "GRAY", 4, true)
                };
            return colors;
        }

        public List<SelectionList> ColorWarmth()
        {
            List<SelectionList> warmth = new List<SelectionList>
            {
                new SelectionList(){ValueField = 1, DisplayField = "Hot" },
                new SelectionList(){ValueField = 2, DisplayField = "Warm" },
                new SelectionList(){ValueField = 3, DisplayField = "Cool" },
                new SelectionList(){ValueField = 4, DisplayField = "Cold" }

            };
            return warmth;
        }

        #endregion
    }
}
