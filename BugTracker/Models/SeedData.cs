using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BugTracker.Models
{
    public class SeedData
    {

        public static void Initialize(IServiceProvider serviceProvider) {

            using ApplicationDbContext context = new(
             serviceProvider.GetRequiredService<
                 DbContextOptions<ApplicationDbContext>>());

            PopulateUsers(context);
            PopulateBugs(context);

            void PopulateUsers(ApplicationDbContext context)
            {
                if (context.Users.Any())
                {
                    Console.WriteLine("Users in DB.");
                    return;
                }

                context.Users.AddRange(
                    new Microsoft.AspNetCore.Identity.IdentityUser
                    {
                        UserName = "Administrator",
                        NormalizedUserName = "ADMINISTRATOR",
                        Email = "Admin@BugTracker.Net",
                        NormalizedEmail = "ADMIN@BUGTRACKER.NET",
                        EmailConfirmed = true

                    },

                    new Microsoft.AspNetCore.Identity.IdentityUser
                    {
                        UserName = "Contributor",
                        NormalizedUserName = "CONTRIBUTOR",
                        Email = "Contributor@BugTracker.Net",
                        NormalizedEmail = "Contributor@BUGTRACKER.NET",
                        EmailConfirmed = true

                    },

                    new Microsoft.AspNetCore.Identity.IdentityUser
                    {
                        UserName = "User",
                        NormalizedUserName = "USER",
                        Email = "User@BugTracker.net",
                        NormalizedEmail = "USER@BUGTRACKER.NET",
                        EmailConfirmed = true
                    });
            }

            context.SaveChanges();

            void PopulateBugs(ApplicationDbContext context)
            {
                if (context.Bug.Any())
                {
                    Console.WriteLine("Bugs in DB.");
                    return;
                }

                context.Bug.AddRange(
                    new Bug
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

                    },
                    
                    new Bug
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

                    },

                    new Bug
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

                    }
                    );

                context.SaveChanges();

            }
        }
    }
}
