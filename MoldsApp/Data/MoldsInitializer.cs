﻿using MoldsApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoldsApp.Data
{
    class MoldsInitializer : CreateDatabaseIfNotExists<ApplicationContext>
    {
        protected override void Seed(ApplicationContext db)
        {
            Molds m1 = new Molds 
            { Id = 1, 
                Type = "К",
                Name = "К20х12х6",
                Kus = "1,11", 
                Matrix_Amount = 1, 
                Punch_Amount = 2, 
                Ejector_Amount = 1, 
                Centre_Amount = null, 
                Punch_Size = "22,2", 
                Center_Size = 13.33, 
                Matrix_Height = 26, 
                Press = "Пресс 292", 
                Note = null};

            Molds m2 = new Molds
            {
                Id = 2,
                Type = "К",
                Name = "К20х12х6",
                Kus = "1,15",
                Matrix_Amount = 1,
                Punch_Amount = 4,
                Ejector_Amount = 1,
                Centre_Amount = null,
                Punch_Size = "22,2",
                Center_Size = 13.33,
                Matrix_Height = 26,
                Press = "Пресс 293",
                Note = null
            };

            db.Molds.Add(m1);
            db.Molds.Add(m2);
            db.SaveChanges();
        }
    }
}
