using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class BulkProcessModel : PageModel
    {
        //routed value from another page
        [BindProperty(SupportsGet = true)]
        public string argsearch { get; set; }

        //routed value from another page
        [BindProperty(SupportsGet = true)]
        public string argvalue { get; set; }

        //fouted value local
        [BindProperty(SupportsGet = true)]
        public string playlist { get; set; }

        public void OnGet()
        {
        }
    }
}
