using System;
using System.Collections.Generic;
using System.Linq;
using PartyOrganizer.Core.Model;
using PartyOrganizer.Core.Repository.Interfaces;

namespace PartyOrganizer.Core.Repository
{
    public class PartyRepository : IPartyRepository
    {
        private List<Party> parties = new List<Party>();
        

        public PartyRepository()
        {
        }

        public void Add(Party entity)
        {
            parties.Add(entity);
        }

        public IEnumerable<Party> GetAll()
        {
            return parties;
        }

        public Party GetByID(int ID)
        {
            return parties.FirstOrDefault(x => x.ID == ID);
        }

        public IEnumerable<Party> GetPartiesOrganizedByUser(User user)
        {
            return parties.FindAll(x => x.Admin.Equals(user));
        }

        public IEnumerable<Party> GetPartiesUserParticipateIn(User user)
        {
            /*var result = from p in parties
                         where p.Participants != null
                         from part in p.Participants
                         where part.Equals(user)
                         select p;   */

            /*var result = parties.Where(x => x.Participants != null)
                                            .Where(x => x.Participants
                                            .All(z => !z.Equals(user)));*/

            var result = parties.Where(x => x.Participants != null)
                                .Where(x => x.Participants
                                .Any(z => z.Equals(user)));

            return result;
        }

        public void Remove(Party entity)
        {
            parties.Remove(entity);
        }

        //only for unit tests purposes, it will be removed in the final ver
        public void RemoveAll()
        {
            parties.Clear();
        }

        public void Populate()
        {
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

            for (int i = 0; i < 2; i++)
            {
                Add(new Party()
                {
                    ID = i,
                    ShortDescription = " Short Description about the first party",
                    Description = " Full Description about the first party",
                    Location = " Wrocław, ul. xyz 1337 ",
                    Admin = admin1,
                    Participants = new List<User>
                    {
                        new User()
                        {
                            ID = 0,
                            Name = "Sebastian",
                            Surname = "Łągiewski",
                            Online = true,
                            Email = $"s3blag{i}@gmail.com",
                            PhoneNumber = "48123456789",
                            ImagePath = "247324/abstract-user-flat-1.png",
                            Friends = null
                        },
                        new User()
                        {
                            ID = 1,
                            Name = "Klaudia",
                            Surname = "Łągiewska",
                            Online = false,
                            Email = $"sl{i}@gmail.com",
                            PhoneNumber = "48123423139",
                            ImagePath = "247319/abstract-user-flat-3.png",
                            Friends = null
                        }
                    },
                    ImagePath = "171448/cyberscooty-let-s-party-1.png",
                    Date = DateTime.Today
                });
            }
            Add(new Party()
            {
                ID = 2,
                ShortDescription = " Short Description about the first party",
                Description = " Full Description about the first party",
                Location = " Wrocław, ul. xyz 1337 ",
                Admin = admin2,
                Participants = null,
                ImagePath = "171448/cyberscooty-let-s-party-1.png",
                Date = DateTime.Today
            });
        }
    }
}