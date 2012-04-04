using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdCookingDemo1.Models;

namespace NerdCookingDemo1.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var context = new NerdCookingContext();
            var workshop = new Workshop()
                               {
                                   Date = DateTime.Now,
                                   Description = "Sample workshop",
                                   Title = "Sample"
                               };

            context.Workshops.Add(workshop);
            context.SaveChanges();

            var foundResult = context.Workshops.FirstOrDefault(item => item.Id == workshop.Id);

            Assert.IsNotNull(foundResult);
        }
    }
}
