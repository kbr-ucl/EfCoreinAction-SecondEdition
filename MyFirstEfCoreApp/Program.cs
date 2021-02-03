// Copyright (c) 2020 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace MyFirstEfCoreApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(
                "Commands: l (list), u (change url), r (resetDb) and e (exit) - add -l to first two for logs");
            Console.Write(
                "Checking if database exists... ");
            Console.WriteLine(Commands.WipeCreateSeed(true) ? "created database and seeded it." : "it exists.");


            using (var context = new AppDbContext())
            {
                var singleBook = context.Books
                    .Include(book => book.Author)
                    .Single(book => book.Title == "Quantum Networking");

                ShowChanged(context);

                singleBook.Author.WebUrl = "https://Some.new.url.com";

                ShowChanged(context);

                context.SaveChanges();

                Console.WriteLine("... SaveChanges called.");
            }
        }

        private static void ShowChanged(AppDbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries())
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State} ");
            }
        }
    }
}