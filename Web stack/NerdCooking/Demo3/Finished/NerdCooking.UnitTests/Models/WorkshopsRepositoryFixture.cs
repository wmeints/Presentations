using System;
using System.Data.Entity;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdCooking.Models;

namespace NerdCooking.UnitTests.Models
{
    [TestClass]
    public class WorkshopsRepositoryFixture
    {
        [ClassInitialize]
        public static void InitializeTestFixture(TestContext context)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<NerdCookingContext>());
        }

        [TestMethod]
        public void TestInsertWorkshopWithReservations()
        {
            var workshop = new Workshop();
            workshop.Date = DateTime.Now;
            workshop.Title = "Sample workshop";
            workshop.Description = "Sample";

            var reservation = new Reservation();
            reservation.UserName = "testuser";

            workshop.Reservations.Add(reservation);

            var repository = new WorkshopRepository();
            repository.InsertOrUpdate(workshop);
            repository.Save();

            var results = repository.Find(workshop.Id);

            Assert.IsNotNull(results);
            Assert.AreEqual(1,results.Reservations.Count);
        }

        [TestMethod]
        public void TestUpdateWorkshop()
        {
            var workshop = new Workshop();
            workshop.Date = DateTime.Now;
            workshop.Title = "Sample workshop";
            workshop.Description = "Sample";

            var reservation = new Reservation();
            reservation.UserName = "testuser";

            workshop.Reservations.Add(reservation);

            var repository = new WorkshopRepository();
            repository.InsertOrUpdate(workshop);
            repository.Save();

            var foundWorkshop = repository.Find(workshop.Id);

            foundWorkshop.Reservations.Add(new Reservation() { UserName = "testuser2"});

            repository.InsertOrUpdate(foundWorkshop);
            repository.Save();

            var results = repository.Find(foundWorkshop.Id);

            Assert.AreEqual(2,results.Reservations.Count);
        }
    }
}
