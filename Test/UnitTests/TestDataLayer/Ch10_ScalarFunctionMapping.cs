﻿// Copyright (c) 2020 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.Linq;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using Test.Chapter10Listings.EfCode;
using Test.TestHelpers;
using TestSupport.EfHelpers;
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions.AssertExtensions;

namespace Test.UnitTests.TestDataLayer
{
    public class Ch10_ScalarFunctionMapping
    {
        public Ch10_ScalarFunctionMapping(ITestOutputHelper output)
        {
            _output = output;

            _options = this.CreateUniqueClassOptions<Chapter10EfCoreContext>();
            using (var context = new Chapter10EfCoreContext(_options))
            {
                if (context.Database.EnsureCreated())
                {
                   //new database, so seed it with function and books
                    context.AddUdfToDatabase();

                    context.AddRange(EfTestData.CreateDummyBooks(setBookId: false));
                    context.SaveChanges();
                }
            }
        }

        private readonly ITestOutputHelper _output;

        private readonly DbContextOptions<Chapter10EfCoreContext> _options;

        private class Dto
        {
            public int BookId { get; set; }
            public string Title { get; set; }
            public double? AveVotes { get; set; }
        }

        [Fact]
        public void TestFilterUsingUdfOk()
        {
            //SETUP
            using (var context = new Chapter10EfCoreContext(_options))
            {
                //ATTEMPT
                var query = context.Books
                    .Where(x => 
                        MyUdfMethods.AverageVotes(x.BookId) >= 2.5);
                var books = query.ToList();

                //VERIFY
                _output.WriteLine(query.ToQueryString());
                books.Count.ShouldEqual(6);
            }
        }

        [Fact]
        public void TestUdfAverageIsCorrectOk()
        {
            //SETUP
            using (var context = new Chapter10EfCoreContext(_options))
            {
                //ATTEMPT
                var bookAndVotes = context.Books.Select(x => new Dto //#A
                {
                    BookId = x.BookId,
                    Title = x.Title,
                    AveVotes = MyUdfMethods.AverageVotes(x.BookId)  //#B
                }).ToList();
                /***************************************************************
                #A This is a normal EF Core query on the Books table
                #B This calls your scalar valued UDF using its representing method
                 **************************************************************/

                //VERIFY
                var softAve = context.Books.Include(x => x.Reviews).OrderBy(p => p.BookId)
                    .Select(x => x.Reviews.Any() ? (double?)x.Reviews.Average(y => y.NumStars) : null).ToList();
                bookAndVotes.OrderBy(p => p.BookId).Select(x => x.AveVotes).ShouldEqual(softAve);
            }
        }

        [Fact]
        public void TestUdfWorksOk()
        {
            //SETUP
            using (var context = new Chapter10EfCoreContext(_options))
            {
                //ATTEMPT
                var bookAndVotesQuery = context.Books.Select(x => new Dto
                {
                    BookId = x.BookId,
                    Title = x.Title,
                    AveVotes = MyUdfMethods.AverageVotes(x.BookId)
                });

                //VERIFY
                _output.WriteLine(bookAndVotesQuery.ToQueryString());
            }
        }
    }
}