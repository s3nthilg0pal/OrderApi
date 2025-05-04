using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Order.Infrastructure.Data;

namespace Order.Application.Test.Common
{
    public class TestingWithDatabase
    {
        public WideWorldImportersContext Context { get; set; }
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<WideWorldImportersContext>()
                .UseInMemoryDatabase(TestContext.CurrentContext.Test.Name)
                .Options;

            Context = new WideWorldImportersContext(options);
            Context.Database.EnsureCreated();
        }

        [TearDown]
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
