using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdCooking.Controllers;
using NerdCooking.Models;
using Rhino.Mocks;

namespace NerdCooking.UnitTests
{
    [TestClass]
    public class WorkshopsControllerFixture
    {
        [TestMethod]
        public void TestCreateWithPost()
        {
            var workshopRepositoryStub = MockRepository.GenerateStub<IWorkshopRepository>();
            var membershipRepositoryStub = MockRepository.GenerateStub<IMembershipRepository>();
            var controller = new WorkshopsController(membershipRepositoryStub, workshopRepositoryStub);

            membershipRepositoryStub.Stub(repository => repository.GetMemberships())
                .Return(new Membership[]
                            {
                                new Membership()
                                    {
                                        IsConfirmed
                                            = true,
                                        Id = 1,
                                        IsOptional =
                                            false,
                                        UserName =
                                            "testuser"
                                    }
                            }.AsQueryable());

            var workshop = new Workshop();
            workshop.Title = "Test";
            workshop.Date = DateTime.Now;
            workshop.Description = "Test workshop";

            var result = controller.Create(workshop) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            
            // The workshop that gets saved in the database needs to have one reservation
            workshopRepositoryStub.AssertWasCalled(repository => repository.InsertOrUpdate(
                Arg<Workshop>.Matches(arg => arg.Reservations.Count == 1)));
        }
    }
}
