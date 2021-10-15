using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class BasicDataMovementModel : PageModel
    {
        //data field
        public string MyName;

        public void OnGet()
        { 
            //execute in response to a Get request from the user (browser)
            //when the page is first accessed, it issues a Get Request
            //when the page is refreshed, WITHOUT a Post, it issues a Get Request
            Random rnd = new Random();
            int oddeven = rnd.Next(0, 25);
            if (oddeven % 2 == 0)
            {
                MyName = $"Don is {oddeven}";
            }
            else
            {
                MyName = null;
            }
        }
    }
}
