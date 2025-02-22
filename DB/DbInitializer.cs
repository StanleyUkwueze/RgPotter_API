﻿using Microsoft.Extensions.Configuration;
using RG_Potter_API.DB;
using RG_Potter_API.Models;
using RG_Potter_API.Services;
using System;

namespace RG_Potter_API
{
    internal class DbInitializer
    {
        private readonly bool _reset;
        private readonly IPasswordHash _hash;

        public DbInitializer(IConfiguration configuration, IPasswordHash hash)
        {
            _reset = bool.Parse(configuration["ResetDB"] ?? "true");
            _hash = hash;
        }

        internal void Initialize(PotterContext context)
        {
            if (_reset) context.Database.EnsureDeleted();

            if (!context.Database.EnsureCreated()) return;


            var houses = new[]
            {
                new House
                {
                    Id = "gryffindor",
                    Name = "Grifinória"
                },
                new House
                {
                    Id = "hufflepuff",
                    Name = "Lufa-Lufa"
                },
                new House
                {
                    Id = "slytherin",
                    Name = "Sonserina"
                },
                new House
                {
                    Id = "ravenclaw",
                    Name = "Corvinal"
                },
                new House
                {
                    Id = "none",
                    Name = "Sem casa"
                }
            };

            foreach (var house in houses) context.Houses.Add(house);

            var genders = new[]
            {
                new Gender { Pronoum = "a" },
                new Gender { Pronoum = "o" }
            };

            foreach (var gender in genders) context.Genders.Add(gender);

            var users = new[] 
            { 
                new User
                {
                    Name = "Felipe Beserra",
                    House_Id = "gryffindor",
                    Pronoum = "o",
                    Email = "felipe@adm.com",
                    Password = _hash.Of("SecurePassword")
                }
            };

            foreach (var user in users) context.Users.Add(user);

            context.SaveChanges();
        }
    }
}