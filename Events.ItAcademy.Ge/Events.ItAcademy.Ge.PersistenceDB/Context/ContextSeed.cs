using Events.ItAcademy.Ge.Domain.POCO;
using Events.ItAcademy.Ge.PersistenceDB.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Ge.PersistenceDB.Context
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Basic.ToString()));
        }


        public static async Task SeedPublishersDatabase(EventsDbContext context)
        {
            if (!context.Publishers.Any())
            {
                await context.Publishers.AddAsync(new Publisher() { Name = "Nikusha Ovashvili", Email = "nikushaovashvili@gmail.com" });
                await context.Publishers.AddAsync(new Publisher() { Name = "Jane Doe", Email = "janedoe@gmail.com" });
                await context.Publishers.AddAsync(new Publisher() { Name = "John Doe", Email = "johndoe@gmail.com" });
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedEventsDatabase(EventsDbContext context)
        {
            if (!context.Events.Any())
            {
                await context.Events.AddAsync(new Event() { Name = "Tbilisi Teech Meetup", City = "Tbilisi", Address = "LOKAL Tbilisi, 47/17 Belinski street", PhotoPath = "78bc94ee-cc4d-4b02-8552-7ccff479857d_techMeetup.jpg", Date = DateTime.Now.AddDays(2), PublisherID = (context.Publishers.FirstOrDefault(p => p.Email == "nikushaovashvili@gmail.com")).PublisherID });
                await context.Events.AddAsync(new Event() { Name = "Food Park Fall21", City = "Tbilisi", Address = "Mushtaidi Park", PhotoPath = "847430d6-3b2f-437b-944b-197317f6dee3_foodFestival.jpg", Date = DateTime.Now.AddDays(5), PublisherID = (context.Publishers.FirstOrDefault(p => p.Email == "nikushaovashvili@gmail.com")).PublisherID });
                await context.Events.AddAsync(new Event() { Name = "Quiz Night", City = "Tbilisi", Address = "LOKAL Tbilisi, 47/17 Belinski street", PhotoPath = "feb99ba1-ec3f-4ac4-b53e-8869df4fd41d_quizNight.jpg", Date = DateTime.Now.AddDays(9),  PublisherID = (context.Publishers.FirstOrDefault(p => p.Email == "nikushaovashvili@gmail.com")).PublisherID });
                await context.Events.AddAsync(new Event() { Name = "Tbilisi Teech Meetup", City = "Tbilisi", Address = "LOKAL Tbilisi, 47/17 Belinski street", PhotoPath = "78bc94ee-cc4d-4b02-8552-7ccff479857d_techMeetup.jpg", Date = DateTime.Now.AddDays(2), PublisherID = (context.Publishers.FirstOrDefault(p => p.Email == "janedoe@gmail.com")).PublisherID });
                await context.Events.AddAsync(new Event() { Name = "Food Park Fall21", City = "Tbilisi", Address = "Mushtaidi Park", PhotoPath = "847430d6-3b2f-437b-944b-197317f6dee3_foodFestival.jpg", Date = DateTime.Now.AddDays(5), PublisherID = (context.Publishers.FirstOrDefault(p => p.Email == "johndoe@gmail.com")).PublisherID });
                await context.Events.AddAsync(new Event() { Name = "Quiz Night", City = "Tbilisi", Address = "LOKAL Tbilisi, 47/17 Belinski street", PhotoPath = "feb99ba1-ec3f-4ac4-b53e-8869df4fd41d_quizNight.jpg", Date = DateTime.Now.AddDays(9),  PublisherID = (context.Publishers.FirstOrDefault(p => p.Email == "janedoe@gmail.com")).PublisherID });
                await context.Events.AddAsync(new Event() { Name = "Tbilisi Teech Meetup", City = "Tbilisi", Address = "LOKAL Tbilisi, 47/17 Belinski street", PhotoPath = "78bc94ee-cc4d-4b02-8552-7ccff479857d_techMeetup.jpg", Date = DateTime.Now.AddDays(2), PublisherID = (context.Publishers.FirstOrDefault(p => p.Email == "johndoe@gmail.com")).PublisherID });
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedSudoAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new ApplicationUser();
            if(!userManager.Users.Any())
            {
                defaultUser = new ApplicationUser() { FirstName = "Nikusha", LastName = "Ovashvili", UserName = "nikushaovashvili@gmail.com", Email = "nikushaovashvili@gmail.com", EmailConfirmed = true };
                await userManager.CreateAsync(defaultUser, "Qwer123$");
                await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Basic.ToString());
                await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Moderator.ToString());
                await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Admin.ToString());

                defaultUser = new ApplicationUser() { FirstName = "John", LastName = "Doe", UserName = "johndoe@gmail.com", Email = "johndoe@gmail.com", EmailConfirmed = true };
                await userManager.CreateAsync(defaultUser, "Qwer123$");
                await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Basic.ToString());

                defaultUser = new ApplicationUser() { FirstName = "Jane", LastName = "Doe", UserName = "janedoe@gmail.com", Email = "janedoe@gmail.com", EmailConfirmed = true };
                await userManager.CreateAsync(defaultUser, "Qwer123$");
                await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Basic.ToString());
            }
        }
    }
}
