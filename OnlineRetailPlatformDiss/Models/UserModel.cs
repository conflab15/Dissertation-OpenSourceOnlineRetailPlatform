using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnlineRetailPlatformDiss.Models
{
    public class UserModel : IdentityUser
    {
        [Required, MaxLength(30)]
        public string? Forename { get; set; }
        [Required, MaxLength(30)]
        public string? Surname { get; set; }
        public string? AddressLine1 { get; set; }
        [Required, MaxLength(50)]
        public string? AddressLine2 { get; set; }
        [Required, MaxLength(30)]
        public string? Town { get; set; }
        [Required, MaxLength(30)]
        public string? County { get; set; }
        [Required, MaxLength(8)]
        public string? PostCode { get; set; }

        //If User is a part of  a Business, these variables will be used...
        public bool IsBusiness { get; set; }

    }
}
