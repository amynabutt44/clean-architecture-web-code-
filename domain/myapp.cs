using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace domain.Models
{
    public class myapp : IdentityUser
    {
        
        public string firstName { get; set; }


        public string lastName { get; set; }

     
        public string city { get; set; }

        // The 'country' property is not validated
        public string country { get; set; }
    }
}
