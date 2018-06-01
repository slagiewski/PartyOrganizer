//using System;
//using System.Collections.Generic;
//using System.Linq;
//using PartyOrganizer.Core.Model;
//using PartyOrganizer.Core.Model.Party;
//using PartyOrganizer.Core.Repository.Interfaces;

//namespace PartyOrganizer.Core.Repository
//{
//    public class PartyRepository : IPartyRepository
//    {
//        private static List<PartyInfo> _parties = new List<PartyInfo>();

//        private static int IdCounter => _parties.Count; 

//        static PartyRepository()
//        {
//            var userRepository = new UserRepository();

//            var admin1 = new User()
//            {
//                Name = "Sławomir",
//                Online = true,
//                Surname = "Sławny",
//                Email = "slawny.slawek@gmail.com",
//                PhoneNumber = "48987654321",
//                Image = "247320/abstract-user-flat-4.png",
//                Friends = null
//            };
//            userRepository.Add(admin1);

//            var admin2 = new User()
//            {
//                ID = IdCounter,
//                Name = "Przemysław",
//                Online = true,
//                Surname = "Uprzejmy",
//                Email = "uprzejmy.przemek@gmail.com",
//                PhoneNumber = "48987654321",
//                Image = "247320/abstract-user-flat-4.png",
//                Friends = null
//            };
//            userRepository.Add(admin2);

//            for (int i = 0; i < 4; i++)
//            {
//                _parties.Add(new PartyInfo()
//                {
//                    ID = IdCounter,
//                    Name = $"Short Description about the {i + 1}. party",
//                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
//                                    " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam," +
//                                    " quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute" +
//                                    " irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur." +
//                                    " Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim" +
//                                    " id est laborum.",
//                    Location = "Wrocław, ul. xyz 1337 ",
//                    Admin = admin1,
//                    Participants = new List<User>
//                    {
//                        admin1,
//                        admin2
//                    },
//                    Image = "171448/cyberscooty-let-s-party-1.png",
//                    Date = DateTime.Today
//                });
//            }

//            _parties.Add(new PartyInfo()
//            {
//                ID = IdCounter,
//                Name = "Short Description about the 5. party",
//                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
//                                " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam," +
//                                " quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute" +
//                                " irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur." +
//                                " Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim" +
//                                " id est laborum.",
//                Location = " Wrocław, ul. xyz 1337 ",
//                Admin = admin2,
//                Participants = null,
//                Image = "171448/cyberscooty-let-s-party-1.png",
//                Date = DateTime.Today
//            });
//        }

//        public int Add(PartyInfo party)
//        {
//            party.ID = IdCounter;
//            _parties.Add(party);
//            return party.ID;
//        }

//        public IEnumerable<PartyInfo> GetAll()
//        {
//            return _parties;
//        }

//        public PartyInfo GetByID(int ID)
//        {
//            return _parties.FirstOrDefault(x => x.ID == ID);
//        }

//        public IEnumerable<PartyInfo> GetPartiesByUser(User user)
//        {
//            return _parties.FindAll(x => x.Admin.Equals(user));
//        }

//        public IEnumerable<PartyInfo> GetPartiesWithUser(User user)
//        {
//            /*var result = parties.Where(x => x.Participants != null)
//                                  .Where(x => x.Participants.All(z => !z.Equals(user)));*/

//            /*var result = parties.Where(x => x.Participants != null)
//                                .Where(x => x.Participants.Any(z => z.Equals(user)));*/

//            var result = from p in _parties
//                         where p.Participants != null
//                         from participants in p.Participants
//                         where participants.Equals(user)
//                         select p;

//            return result;
//        }

//        public void Remove(PartyInfo party)
//        {
//            _parties.Remove(party);
//        }

//        //temporary
//        public void RemoveAll()
//        {
//            _parties.Clear();
//        }

//    }
//}