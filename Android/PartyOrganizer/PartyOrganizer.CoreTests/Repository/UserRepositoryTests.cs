using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartyOrganizer.Core.Repository.Interfaces;
using System.Linq;


namespace PartyOrganizer.Core.Repository.Tests
{

    /// <summary>
    /// Very basic (low quality)Unit Tests atm
    /// </summary>
    [TestClass()]
    public class UserRepositoryTests
    {

        // there are currently 3 values repository is initialized to
        IUserRepository repository = new UserRepository();

        [TestMethod()]
        public void Should_Return_All_Users()
        {   
            // currentl there are 3 values
            var expected = 3;

            var actual = repository.GetAll().Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Given_existing_userID_should_return_proper_user()
        {
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
        public void Given_Non_Existing_UserID_Should_Return_Null()
        {
            var actual = repository.GetByID(1230);
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void GetFriendsTest()
        {
            var expected1 = 0;
            var expected2 = 3;

            var actual1 = repository.GetByID(0).Friends == null ? 0 : repository.GetByID(0).Friends.Count();
            var actual2 = repository.GetByID(1).Friends == null ? 0 : repository.GetByID(1).Friends.Count();

            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
        }

        [TestMethod()]
        public void Given_new_user_should_return_proper_string()
        {
            var newUser = new User()
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
            var expected = "Klaudia Łągiewska";

            var actual = newUser.ToString();
            // :/
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AddTest()
        {
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