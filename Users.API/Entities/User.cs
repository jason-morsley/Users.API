using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Users.API.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTimeOffset DateOfBirth { get; set; }

        public string Location { get; set; }

        //Email(s)
        //Phone/Mobile Number(s)
        //Address(Work, Home, etc)
    }
}
