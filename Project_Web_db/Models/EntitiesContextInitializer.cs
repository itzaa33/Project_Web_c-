using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Project_Web_db.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;


namespace Project_Web_db.Models
{
    public class EntitiesContextInitializer 
    {

        private ApplicationDbContext _context;

        public EntitiesContextInitializer(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task Seed()
        {
            if (!_context.Personnels.Any())
            {
                _context.AddRange(
                     new Personnel
                     {
                         email = "test@hotmail.com",
                         password = BCrypt.Net.BCrypt.HashPassword("123456789"),
                         name = "a",
                         phone_number = "0123456789",
                         provider = "Normal",
                         state = "Admin",
                         status_ban = 0,
                         money = 0,
                         date = DateTime.Now,
                         date_update = DateTime.Now

                     },
                    new Personnel
                    {
                        email = "test1@hotmail.com",
                        password = BCrypt.Net.BCrypt.HashPassword("123456789"),
                        name = "b",
                        phone_number = "0123456789",
                        provider = "Normal",
                        state = "Ticketing",
                        status_ban = 0,
                        money = 0,
                        date = DateTime.Now,
                        date_update = DateTime.Now

                    },
                     new Personnel
                     {
                         email = "test2@hotmail.com",
                         password = BCrypt.Net.BCrypt.HashPassword("123456789"),
                         name = "c",
                         phone_number = "0123456789",
                         provider = "Normal",
                         state = "Driver",
                         status_ban = 0,
                         money = 0,
                         car_number = 1,
                         date = DateTime.Now,
                         date_update = DateTime.Now

                     },
                       new Personnel
                       {
                           email = "test3@hotmail.com",
                           password = BCrypt.Net.BCrypt.HashPassword("123456789"),
                           name = "d",
                           phone_number = "0123456789",
                           provider = "Normal",
                           state = "Driver",
                           status_ban = 0,
                           money = 0,
                           car_number = 2,
                           date = DateTime.Now,
                           date_update = DateTime.Now

                       },
                        new Personnel
                        {
                            email = "test4@hotmail.com",
                            password = BCrypt.Net.BCrypt.HashPassword("123456789"),
                            name = "e",
                            phone_number = "0123456789",
                            provider = "Normal",
                            state = "Driver",
                            status_ban = 0,
                            money = 0,
                            car_number = 3,
                            date = DateTime.Now,
                            date_update = DateTime.Now

                        },
                        new Personnel
                        {
                            email = "test5@hotmail.com",
                            password = BCrypt.Net.BCrypt.HashPassword("123456789"),
                            name = "f",
                            phone_number = "0123456789",
                            provider = "Normal",
                            state = "Driver",
                            status_ban = 0,
                            money = 0,
                            car_number = 4,
                            date = DateTime.Now,
                            date_update = DateTime.Now

                        },
                        new Personnel
                        {
                            email = "test6@hotmail.com",
                            password = BCrypt.Net.BCrypt.HashPassword("123456789"),
                            name = "g",
                            phone_number = "0123456789",
                            provider = "Normal",
                            state = "Driver",
                            status_ban = 0,
                            money = 0,
                            car_number = 5,
                            date = DateTime.Now,
                            date_update = DateTime.Now

                        },
                        new Personnel
                        {
                            email = "test7@hotmail.com",
                            password = BCrypt.Net.BCrypt.HashPassword("123456789"),
                            name = "h",
                            phone_number = "0123456789",
                            provider = "Normal",
                            state = "Driver",
                            status_ban = 0,
                            money = 0,
                            car_number = 6,
                            date = DateTime.Now,
                            date_update = DateTime.Now

                        },
                        new Personnel
                        {
                            email = "test8@hotmail.com",
                            password = BCrypt.Net.BCrypt.HashPassword("123456789"),
                            name = "i",
                            phone_number = "0123456789",
                            provider = "Normal",
                            state = "Driver",
                            status_ban = 0,
                            money = 0,
                            car_number = 7,
                            date = DateTime.Now,
                            date_update = DateTime.Now

                        },
                        new Personnel
                        {
                            email = "test9@hotmail.com",
                            password = BCrypt.Net.BCrypt.HashPassword("123456789"),
                            name = "j",
                            phone_number = "0123456789",
                            provider = "Normal",
                            state = "Driver",
                            status_ban = 0,
                            money = 0,
                            car_number = 8,
                            date = DateTime.Now,
                            date_update = DateTime.Now

                        },
                        new Personnel
                        {
                            email = "test10@hotmail.com",
                            password = BCrypt.Net.BCrypt.HashPassword("123456789"),
                            name = "k",
                            phone_number = "0123456789",
                            provider = "Normal",
                            state = "Driver",
                            status_ban = 0,
                            money = 0,
                            car_number = 9,
                            date = DateTime.Now,
                            date_update = DateTime.Now

                        },
                         new Personnel
                         {
                             email = "test11@hotmail.com",
                             password = BCrypt.Net.BCrypt.HashPassword("123456789"),
                             name = "k",
                             phone_number = "0123456789",
                             provider = "Normal",
                             state = "Driver",
                             status_ban = 0,
                             money = 0,
                             car_number = 10,
                             date = DateTime.Now,
                             date_update = DateTime.Now

                         }
                     );

                _context.SaveChanges();
            }

            
        }


    }
}
