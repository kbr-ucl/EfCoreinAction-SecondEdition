// Copyright (c) 2020 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace MyFirstEfCoreApp
{
    public class AppDbContext : DbContext
    {



        public ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        }
        );

        private const string ConnectionString = //#A
            @"Server=localhost;
             Database=MyFirstEfCoreDb;
             Trusted_Connection=True";

        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString); //#B
            optionsBuilder.UseLoggerFactory(loggerFactory);
        }
    }

    /********************************************************
       Add-Migration InitialMigration
       Update-Database
     ********************************************************/
}