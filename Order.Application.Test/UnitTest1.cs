using Bogus;
using Microsoft.EntityFrameworkCore;
using Order.Application.Test.Common;
using Order.Domain.Models;

namespace Order.Application.Test
{
    public class Tests : TestingWithDatabase
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            //Arrange
            var color = new ColorFaker().Generate();
            Context.Colors.Add(color);
            Context.SaveChanges();

            var test = Context.Colors.FirstOrDefault();

            //Assert
            Assert.That(test.ColorName , Is.EqualTo(color.ColorName));
        }

        [Test]
        public async Task Test12()
        {
            //Arrange
            var color = new ColorFaker().Generate();
            Context.Colors.Add(color);
            Context.SaveChanges();

            var test = await Context.Colors.ToListAsync();

            //Assert
            Assert.That(test.Count, Is.EqualTo(1));
        }
    }

    public class ColorFaker : Faker<Color>
    {
        public ColorFaker()
        {
            RuleFor(d => d.ColorName, f => f.Random.Word());
        }
    }
}
