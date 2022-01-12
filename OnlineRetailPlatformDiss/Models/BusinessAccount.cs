﻿using System.ComponentModel.DataAnnotations;

namespace OnlineRetailPlatformDiss.Models
{
    public class BusinessAccount
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(100)]
        public string BusinessName { get; set; }  
        [Required, MaxLength(500)]
        public string BusinessDesc { get; set; }
        [Required, MaxLength(50)]
        public string AddressLine1 { get; set; }
        [Required, MaxLength(50)]
        public string AddressLine2 { get; set; }    
        [Required, MaxLength(30)]
        public string Town { get; set; }    
        [Required, MaxLength(30)]
        public string County { get; set; }
        [Required, MaxLength(8)]
        public string PostCode { get; set; }
        public User Manager { get; set; }

        //Variable of a User Model who logs in and manages the account? 

    }
}