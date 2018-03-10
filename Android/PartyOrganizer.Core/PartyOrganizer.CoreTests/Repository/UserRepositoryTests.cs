using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartyOrganizer.Core.Repository.Interfaces;
using System.Linq;


namespace PartyOrganizer.Core.Repository.Tests
{
    [TestClass()]
    public class UserRepositoryTests
    {

        // there are currently 3 values repository is initialized to
        IUserRepository repository = new UserRepository();

        [TestMethod()]
        public void GetAllTest()
        {
            repository.Populate();
            var expected = 3;

            var actual = repository.GetAll().Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetByIDTest()
        {
            repository.Populate();
            var expected = new User()
            {
                ID = 123,
                Name = "Klaudia",
                Surname = "Łągiewska",
                Online = false,
                Email = "sl@gmail.com",
                PhoneNumber = "48123423139",
                ImagePath = "247319/abstract-user-flat-3.png",
                Friends = null
            };

            repository.Add(expected);
            var actual = repository.GetByID(123);
            // :/
            Assert.AreEqual(true, expected.Equals(actual));
        }

        [TestMethod()]
        public void GetFriendsTest()
        {
            repository.Populate();
            var expected1 = 0;
            var expected2 = 3;

            var actual1 = repository.GetByID(0).Friends == null ? 0 : repository.GetByID(0).Friends.Count();
            var actual2 = repository.GetByID(1).Friends == null ? 0 : repository.GetByID(1).Friends.Count();

            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
        }

        [TestMethod()]
        public void AddTest()
        {
            repository.Populate();
            var count = repository.GetAll().Count();
            var expected = count + 1;

            repository.Add(new User()
            {
                ID = 10,
                Name = "Sebastian",
                Surname = "Łągiewski",
                Online = true,
                Email = "s3blag@gmail.com",
                PhoneNumber = "48123456789",
                ImagePath = "247324/abstract-user-flat-1.png",
                Friends = null
            });

            var actual = repository.GetAll().Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void RemoveTest()
        {
            repository.Populate();
            var count = repository.GetAll().Count();
            var expected = count - 1;

            repository.Remove(new User()
            {
                ID = 1,
                Name = "Klaudia",
                Surname = "Łągiewska",
                Online = false,
                Email = "sl@gmail.com",
                PhoneNumber = "48123423139",
                ImagePath = "247319/abstract-user-flat-3.png",
                Friends = null
            });

            var actual = repository.GetAll().Count();
            Assert.AreEqual(expected, actual);
        }
    }
}