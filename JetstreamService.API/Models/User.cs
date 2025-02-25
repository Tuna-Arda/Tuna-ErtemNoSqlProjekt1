using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace JetstreamService.API.Models
{
    public enum UserRole
    {
        Admin,
        Mitarbeiter
    }

    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Benutzername { get; set; }
        public string Passwort { get; set; } // In Produktion: bitte gehasht speichern!
        public UserRole Rolle { get; set; }
    }
}
