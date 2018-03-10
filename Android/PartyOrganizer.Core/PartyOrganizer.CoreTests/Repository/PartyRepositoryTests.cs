using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartyOrganizer.Core.Model;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Core.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyOrganizer.Core.Repository.Tests
{
    [TestClass()]
    public class PartyRepositoryTests
    {
        IPartyRepository repository = new PartyRepository();


        [TestMethod()]
        public void AddTest()
        {
            //remove initialized values
            repository.Populate();

            var count = repository.GetAll() == null ? 0 : repository.GetAll().Count();
            var expected = count + 1;

            repository.Add(new Party()
            {
                ID = 3,
                ShortDescription = " Short Description about the first party",
                Description = " Full Description about the first party",
                Location = " Wrocław, ul. xyz 1337 ",
                Admin = new User()
                {
                    ID = 10,
                    Name = "Sebastian",
                    Surname = "Łągiewski",
                    Online = true,
                    Email = "s3blag@gmail.com",
                    PhoneNumber = "48123456789",
                    ImagePath = "247324/abstract-user-flat-1.png",
                    Friends = null
                },
                Participants = null,
                ImagePath = "171448/cyberscooty-let-s-party-1.png",
                Date = DateTime.Today
            });

            var actual = repository.GetAll().Count();
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod()]
        public void GetAllTest()
        {
            //currently initialized to 3 values
            repository.Populate();
            var expected = 3;


            var actual = repository.GetAll().Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetByIDTest()
        {
            repository.Populate();

            var expected = (new Party()
            {
                ID = 112,
                ShortDescription = " Short Description about the first party",
                Description = " Full Description about the first party",
                Location = " Wrocław, ul. xyz 1337 ",
                Admin = new User()
                {
                    ID = 10,
                    Name = "Sebastian",
                    Surname = "Łągiewski",
                    Online = true,
                    Email = "s3blag@gmail.com",
                    PhoneNumber = "48123456789",
                    ImagePath = "247324/abstract-user-flat-1.png",
                    Friends = null
                },
                Participants = null,
                ImagePath = "171448/cyberscooty-let-s-party-1.png",
                Date = DateTime.Today
            });
            repository.Add(expected);

            var actual = repository.GetByID(112);
            // :/
            Assert.AreEqual(true, expected.Equals(actual));
        }

        [TestMethod()]
        public void GetPartiesOrganizedByUserTest()
        {
            repository.Populate();
            var expected1 = 2;
            var expected2 = 1;

            var admin1 = new User()
            {
                ID = 3,
                Name = "Sławomir",
                Online = true,
                Surname = "Sławny",
                Email = "slawny.slawek@gmail.com",
                PhoneNumber = "48987654321",
                ImagePath = "247320/abstract-user-flat-4.png",
                Friends = null
            };

            var admin2 = new User()
            {
                ID = 4,
                Name = "Przemysław",
                Online = true,
                Surname = "Uprzejmy",
                Email = "uprzejmy.przemek@gmail.com",
                PhoneNumber = "48987654321",
                ImagePath = "247320/abstract-user-flat-4.png",
                Friends = null
            };

            var actual1 = repository.GetPartiesOrganizedByUser(admin1).Count();
            var actual2 = repository.GetPartiesOrganizedByUser(admin2).Count();

            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);

        }
        
        [TestMethod()]
        public void GetPartiesUserParticipateInTest()
        {
            repository.Populate();
            var user1 = new User()
            {
                ID = 1,
                Name = "Klaudia",
                Surname = "Łągiewska",
                Online = false,
                Email = "sl@gmail.com",
                PhoneNumber = "48123423139",
                ImagePath = "247319/abstract-user-flat-3.png",
                Friends = null
            };

            var user2 = new User()
            {
                ID = 5,
                Name = "Friend1",
                Surname = "Surname1",
                Online = true,
                Email = "s3blag@gmail.com",
                PhoneNumber = "48123456781",
                ImagePath = "247324/abstract-user-flat-1.png",
                Friends = null
            };

            var expected1 = 2;
            var expected2 = 0;

            var actual1 = repository.GetPartiesUserParticipateIn(user1) == null ? 0 : repository.GetPartiesUserParticipateIn(user1).Count();
            var actual2 = repository.GetPartiesUserParticipateIn(user2) == null ? 0 : repository.GetPartiesUserParticipateIn(user2).Count();

            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);

        }
        
        [TestMethod()]
        public void RemoveTest()
        {
            repository.Populate();
            var count = repository.GetAll().Count();
            var expected = count - 1;

            repository.Remove(new Party()
            {
                ID = 2,
                ShortDescription = " Short Description about the first party",
                Description = " Full Description about the first party",
                Location = " Wrocław, ul. xyz 1337 ",
                Admin = new User()
                {
                    ID = 4,
                    Name = "Przemysław",
                    Online = true,
                    Surname = "Uprzejmy",
                    Email = "uprzejmy.przemek@gmail.com",
                    PhoneNumber = "48987654321",
                    ImagePath = "247320/abstract-user-flat-4.png",
                    Friends = null
                },
                Participants = null,
                ImagePath = "171448/cyberscooty-let-s-party-1.png",
                Date = DateTime.Today
            });

            var result = repository.GetAll().Count();
            Assert.AreEqual(expected, result);
        }
    }
}