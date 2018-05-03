using System;
using System.Collections.Generic;
using System.Linq;
using PartyOrganizer.Core.Repository.Interfaces;

namespace PartyOrganizer.Core.Repository
{
    public class UserRepository : IUserRepository
    {
        // TODO: 
        // 1. Add UserViewModel which doesn't contain ID etc 

        private static List<User> _users = new List<User>();

        private static int IdCounter => _users.Count;

        static UserRepository()
        {
            _users.Add(new User()
            {
                ID = IdCounter,
                Name = "Sebastian",
                Surname = "Łągiewski",
                Location = "Wrocław",
                Online = true,
                Email = "s3blag@gmail.com",
                PhoneNumber = "48123456789",
                ImagePath = "247324/abstract-user-flat-1.png",
                Friends = null
            });

            var friend1 = new User()
            {
                ID = IdCounter,
                Name = "Friend1",
                Location = "Wrocław",
                Surname = "Surname1",
                Online = true,
                Email = "s3blag1@gmail.com",
                PhoneNumber = "48123456781",
                ImagePath = "247324/abstract-user-flat-1.png",
                Friends = null
            };
            _users.Add(friend1);

            var friend2 = new User()
            {
                ID = IdCounter,
                Name = "Friend2",
                Surname = "Surname2",
                Location = "Wrocław",
                Online = true,
                Email = "s3blag2@gmail.com",
                PhoneNumber = "48123456782",
                ImagePath = "247324/abstract-user-flat-1.png",
                Friends = null
            };
            _users.Add(friend2);

            var friend3 = new User()
            {
                ID = IdCounter,
                Name = "Friend3",
                Surname = "Surname3",
                Location = "Wrocław",
                Online = true,
                Email = "s3blag3@gmail.com",
                PhoneNumber = "48123456783",
                ImagePath = "247324/abstract-user-flat-1.png",
                Friends = null
            };
            _users.Add(friend3);

            _users.Add(new User()
            {
                ID = IdCounter,
                Name = "Klaudia",
                Surname = "Łągiewska",
                Location = "Wrocław",
                Online = false,
                Email = "sl@gmail.com",
                PhoneNumber = "48123423139",
                ImagePath = "247319/abstract-user-flat-3.png",
                Friends = new List<User>
                {
                    friend1,
                    friend2,
                    friend3
                }
            });

            _users.Add(new User()
            {
                ID = IdCounter,
                Name = "Typical",
                Surname = "User",
                Location = "Wrocław",
                Online = false,
                Email = "wafel@gmail.com",
                PhoneNumber = "48123423000",
                ImagePath = "247320/abstract-user-flat-4.png",
                Friends = null
            });
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetByID(int ID)
        {
            return _users.FirstOrDefault(x => x.ID == ID);
        }

        public IEnumerable<User> GetFriends(User user)
        {
            return user.Friends;
        }

        // UserViewModel
        public int Add(User user)
        {
            user.ID = IdCounter;
            _users.Add(user);
            return user.ID;
        }

        public void Remove(User user)
        {
            _users.Remove(user);
        }
    }
}