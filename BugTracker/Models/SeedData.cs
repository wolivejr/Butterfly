using System;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace BugTracker.Models
{

    public class SeedData
    {

        enum Roles
        {
            Administrator,
            Contributor,
            User
        }

        public async Task Initialize(IServiceProvider serviceProvider, 
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            using ApplicationDbContext context = new(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            async Task DatabaseUpdate(ApplicationDbContext context, UserManager<IdentityUser> userManager)
            {

                await Task.Run(() => PopulateRoles(context));
                await Task.Run(() => context.SaveChangesAsync());
                await Task.Run(() => PopulateUsers(context));
                await Task.Run(() => context.SaveChangesAsync());
                await Task.Run(() => AssignPasswordsAndRoles(context, userManager, roleManager));
                await Task.Run(() => context.SaveChangesAsync());
                await Task.Run(() => PopulateBugs(context));
                await Task.Run(() => context.SaveChangesAsync());

            }

            async void PopulateRoles(ApplicationDbContext context)
            {
                if (context.Roles.AnyAsync().Result) { return; }

                IdentityRole Administrator = new()
                {
                    Name = Roles.Administrator.ToString(),
                    NormalizedName = Roles.Administrator.ToString().ToUpper()
                };

                IdentityRole Contributor = new()
                {
                   Name = Roles.Contributor.ToString(),
                   NormalizedName = Roles.Contributor.ToString().ToUpper()
                };

                IdentityRole User = new()
                {
                    Name = Roles.User.ToString(),
                    NormalizedName = Roles.User.ToString().ToUpper()
                };

                await Task.Run(() => context.Roles.AddRangeAsync(Administrator, Contributor, User));

            }

            async void PopulateUsers(ApplicationDbContext context)
            {
                if (context.Users.AnyAsync().Result) { return; }

                IdentityUser Administrator = new()
                {
                    UserName = "Administrator",
                    NormalizedUserName = "ADMINISTRATOR",
                    Email = "admin@bugtracker.net",
                    NormalizedEmail = "ADMIN@BUGTRACKER.NET",
                    EmailConfirmed = true
                };

                IdentityUser Contributor = new()
                {
                    UserName = "Contributor",
                    NormalizedUserName = "CONTRIBUTOR",
                    Email = "contributor@bugtracker.net",
                    NormalizedEmail = "CONTRIBUTOR@BUGTRACKER.NET",
                    EmailConfirmed = true
                };

                IdentityUser User = new()
                {
                    UserName = "User",
                    NormalizedUserName = "USER",
                    Email = "user@bugtracker.net".ToLower(),
                    NormalizedEmail = "USER@BUGTRACKER.NET",
                    EmailConfirmed = true

                };

                await Task.Run(() => context.Users.AddRangeAsync(Administrator, Contributor, User));

            }

            async void PopulateBugs(ApplicationDbContext context)
            {
                if (context.Bug.AnyAsync().Result) { return; }

                Bug BugOne = new()
                {
                    Status = "Closed",
                    Description = "The index page no longer displays the list of bugs",
                    Priority = "Critical",
                    Category = "Crash",
                    Submitter = "Administrator",
                    SubmissionDate = DateTime.Today.ToShortDateString(),
                    SubmissionTime = DateTime.Today.ToShortTimeString(),
                    Solution = "Re-migrated the database with updated information",
                    SolutionUser = "Administrator",
                    SolutionDate = DateTime.Now.ToShortDateString(),
                    SolutionTime = DateTime.Now.ToShortTimeString()
                };

                Bug BugTwo = new()
                {
                    Status = "Closed",
                    Description = "Cannot view submitted bugs without logging in",
                    Priority = "Medium",
                    Category = "Graphical",
                    Submitter = "Contributor",
                    SubmissionDate = DateTime.Today.ToShortDateString(),
                    SubmissionTime = DateTime.Today.ToShortTimeString(),
                    Solution = "Accidentally put the authorization attribute above the wrong function.",
                    SolutionUser = "Administrator",
                    SolutionDate = DateTime.Now.ToShortDateString(),
                    SolutionTime = DateTime.Now.ToShortTimeString()
                };

                Bug BugThree = new()
                {
                    Status = "Open",
                    Description = "Editting details of a bug replaces the original submitter's details with the editor's.",
                    Priority = "Low",
                    Category = "Graphical",
                    Submitter = "User",
                    SubmissionDate = DateTime.Today.ToShortDateString(),
                    SubmissionTime = DateTime.Today.ToShortTimeString(),
                    SolutionUser = null,
                    SolutionDate = null,
                    SolutionTime = null
                };

                await Task.Run(() => context.Bug.AddRangeAsync(BugOne, BugTwo, BugThree));

            }

            async void AssignPasswordsAndRoles(ApplicationDbContext context,
                UserManager<IdentityUser> userManager,
                RoleManager<IdentityRole> roleManager)
            {
                if (context.UserRoles.AnyAsync().Result) { return; }

                IdentityUser Administrator = await (userManager.FindByEmailAsync("admin@bugtracker.net"));
                IdentityUser Contributor = await (userManager.FindByEmailAsync("contributor@bugtracker.net"));
                IdentityUser User = await (userManager.FindByEmailAsync("user@bugtracker.net"));

                await Task.Run(() => userManager.AddPasswordAsync(Administrator, "T1m3f0rd4m4g3c0n7r0l!"));
                await Task.Run(() => userManager.AddPasswordAsync(Contributor, "@L3tm3h3lppl$"));
                await Task.Run(() => userManager.AddPasswordAsync(User, "Ju$7l00k1ng"));
                await Task.Run(() => userManager.AddToRoleAsync(Administrator, Roles.Administrator.ToString()));
                await Task.Run(() => userManager.AddToRoleAsync(Contributor, Roles.Contributor.ToString()));
                await Task.Run(() => userManager.AddToRoleAsync(User, Roles.User.ToString()));

            }

            await Task.Run(() => DatabaseUpdate(context, userManager));

        }
    }
}
