﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RG_Potter_API.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RG_Potter_API.Models
{
    public class User
    {
        private string email;
        [Key]
        [Required]
        public string Email { get => email; set { email = value.ToLower(); } }

        [Required]
        [StringLength(75, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        public string House_Id { get; set; } = "none";

        public House House { get; set; }

        [Required]
        public string Pronoum { get; set; }

        public Gender Gender { get; set; }

        [Required]
        public string Password { get; set; }

        public int LumusSuccesses { get; set; } = 0;

        public int LumusFails { get; set; } = 0;

        public void ValidateForeignKeys(ModelStateDictionary modelState, PotterContext context) 
        {
            if (!context.Houses.Any(h => h.Id == House_Id)) modelState.AddModelError(nameof(House_Id), "House ID invalid!");
            if (!context.Genders.Any(g => g.Pronoum == Pronoum)) modelState.AddModelError(nameof(Pronoum), "Pronoum invalid!");
        }
    }
}
