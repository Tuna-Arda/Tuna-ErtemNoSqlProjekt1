using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace JetstreamService.API.Models
{
    public enum ServiceStatus
    {
        Offen,
        InArbeit,
        Abgeschlossen
    }

    public enum Dienstleistung
    {
        KleinerService,
        GrosserService,
        RennskiService,
        BindungMontierenUndEinstellen,
        FellZuschneiden,
        Heisswachsen
    }

    public class ServiceOrder
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Kundenname { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Prioritaet { get; set; }
        public Dienstleistung Dienstleistung { get; set; }
        public ServiceStatus Status { get; set; }
    }
}
