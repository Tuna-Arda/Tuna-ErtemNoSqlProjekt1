using JetstreamService.API.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace JetstreamService.API.Services
{
    public interface IUserService
    {
        User GetUser(string benutzername, string passwort);
    }

    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IConfiguration config)
        {
            var client = new MongoClient(config.GetSection("MongoDB:ConnectionString").Value);
            var database = client.GetDatabase(config.GetSection("MongoDB:Database").Value);
            _users = database.GetCollection<User>("Users");

            // Seed: Zwei Benutzer anlegen, falls keine vorhanden
            if(_users.Find(user => true).CountDocuments() == 0)
            {
                _users.InsertMany(new List<User>{
                    new User { Benutzername = "admin", Passwort = "admin123", Rolle = UserRole.Admin },
                    new User { Benutzername = "mitarbeiter", Passwort = "mitarbeiter123", Rolle = UserRole.Mitarbeiter }
                });
            }
        }

        public User GetUser(string benutzername, string passwort)
        {
            return _users.Find(u => u.Benutzername == benutzername && u.Passwort == passwort).FirstOrDefault();
        }
    }
}
