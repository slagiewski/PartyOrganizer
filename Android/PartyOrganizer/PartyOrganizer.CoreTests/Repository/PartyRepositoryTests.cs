using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartyOrganizer.Core.Model;
using PartyOrganizer.Core.Repository.Interfaces;
using System;
using System.Linq;

namespace PartyOrganizer.Core.Repository.Tests
{
    /// <summary>
    /// Very basic(low quality) Unit Tests atm
    /// </summary>
    [TestClass()]
    public class PartyRepositoryTests
    {

        //IPartyRepository repository = new PartyRepository();

        //[TestMethod()]
        //public void Given_user_that_participates_in_a_party_should_return_non_zero_value()
        //{
        //    var user = new User()
        //    {
        //        ID = 1,
        //        Name = "Klaudia",
        //        Surname = "Łągiewska",
        //        Online = false,
        //        Email = "sl@gmail.com",
        //        PhoneNumber = "48123423139",
        //        ImagePath = "247319/abstract-user-flat-3.png",
        //        Friends = null
        //    };

        //    var expected = 4;

        //    var actual = repository.GetPartiesWithUser(user) == null ? 0 : repository.GetPartiesWithUser(user).Count();

        //    Assert.AreEqual(expected, actual);

        //}

        //[TestMethod()]
        //public void Given_user_that_doesnt_participate_in_a_party_should_return_zero_value()
        //{
        //    var user = new User()
        //    {
        //        ID = 5,
        //        Name = "Friend1",
        //        Surname = "Surname1",
        //        Online = true,
        //        Email = "s3blag@gmail.com",
        //        PhoneNumber = "48123456781",
        //        ImagePath = "247324/abstract-user-flat-1.png",
        //        Friends = null
        //    };

        //    var expected = 0;

        //    var actual = repository.GetPartiesWithUser(user) == null ? 0 : repository.GetPartiesWithUser(user).Count();

        //    Assert.AreEqual(expected, actual);

        //}

        ////[TestMethod()]
        ////public void Should_Return_All_Parties()
        ////{
        ////    //currently initialized with 5 values

        ////    var expected = 5;


        ////    var actual = repository.GetAll().Count();

        ////    Assert.AreEqual(expected, actual);
        ////}


        //[TestMethod()]
        //public void AddTest()
        //{
        //    var count = repository.GetAll() == null ? 0 : repository.GetAll().Count();
        //    var expected = count + 1;

        //    repository.Add(new Party()
        //    {
        //        Name = " Short Description about the test party",
        //        Description = " Full Description about the first party",
        //        Location = " Wrocław, ul. xyz 1337 ",
        //        Admin = new User()
        //        {
        //            Name = "Sebastian",
        //            Surname = "Łągiewski",
        //            Online = true,
        //            Email = "s3blag@gmail.com",
        //            PhoneNumber = "48123456789",
        //            ImagePath = "247324/abstract-user-flat-1.png",
        //            Friends = null
        //        },
        //        Participants = null,
        //        ImagePath = "171448/cyberscooty-let-s-party-1.png",
        //        Date = DateTime.Today
        //    });

        //    var actual = repository.GetAll().Count();
        //    Assert.AreEqual(expected, actual);
        //}
        
        
        //[TestMethod()]
        //public void Given_Added_Party_Should_Return_It_Back()
        //{
        //    var expected = new Party()
        //    {
        //        ID = 112,
        //        Name = " Short Description about the first party",
        //        Description = " Full Description about the first party",
        //        Location = " Wrocław, ul. xyz 1337 ",
        //        Admin = new User()
        //        {
        //            ID = 10,
        //            Name = "Sebastian",
        //            Surname = "Łągiewski",
        //            Online = true,
        //            Email = "s3blag@gmail.com",
        //            PhoneNumber = "48123456789",
        //            ImagePath = "247324/abstract-user-flat-1.png",
        //            Friends = null
        //        },
        //        Participants = null,
        //        ImagePath = "171448/cyberscooty-let-s-party-1.png",
        //        Date = DateTime.Today
        //    };

        //    repository.Add(expected);

        //    var actual = repository.GetByID(112);
        //    // :/
        //    Assert.AreEqual(true, expected.Equals(actual));
        //}

        //[TestMethod()]
        //public void Given_Not_Added_Party_Should_Return_null()
        //{
        //    var actual = repository.GetByID(1111);
        //    Assert.IsNull(actual);
        //}

        //[TestMethod()]
        //public void GetPartiesOrganizedByUserTest()
        //{
        //    var expected1 = 4;
        //    var expected2 = 1;

        //    var admin1 = new User()
        //    {
        //        ID = 3,
        //        Name = "Sławomir",
        //        Online = true,
        //        Surname = "Sławny",
        //        Email = "slawny.slawek@gmail.com",
        //        PhoneNumber = "48987654321",
        //        ImagePath = "247320/abstract-user-flat-4.png",
        //        Friends = null
        //    };

        //    var admin2 = new User()
        //    {
        //        ID = 4,
        //        Name = "Przemysław",
        //        Online = true,
        //        Surname = "Uprzejmy",
        //        Email = "uprzejmy.przemek@gmail.com",
        //        PhoneNumber = "48987654321",
        //        ImagePath = "247320/abstract-user-flat-4.png",
        //        Friends = null
        //    };

        //    var actual1 = repository.GetPartiesByUser(admin1).Count();
        //    var actual2 = repository.GetPartiesByUser(admin2).Count();

        //    Assert.AreEqual(expected1, actual1);
        //    Assert.AreEqual(expected2, actual2);

        //}
        

        //[TestMethod()]
        //public void RemoveTest()
        //{
        //    var count = repository.GetAll().Count();
        //    var expected = count - 1;

        //    repository.Remove(new Party()
        //    {
        //        ID = 2,
        //        ShortDescription = " Short Description about the first party",
        //        Description = " Full Description about the first party",
        //        Location = " Wrocław, ul. xyz 1337 ",
        //        Admin = new User()
        //        {
        //            ID = 4,
        //            Name = "Przemysław",
        //            Online = true,
        //            Surname = "Uprzejmy",
        //            Email = "uprzejmy.przemek@gmail.com",
        //            PhoneNumber = "48987654321",
        //            ImagePath = "247320/abstract-user-flat-4.png",
        //            Friends = null
        //        },
        //        Participants = null,
        //        ImagePath = "171448/cyberscooty-let-s-party-1.png",
        //        Date = DateTime.Today
        //    });

        //    var result = repository.GetAll().Count();
        //    Assert.AreEqual(expected, result);
        //}
    }
}