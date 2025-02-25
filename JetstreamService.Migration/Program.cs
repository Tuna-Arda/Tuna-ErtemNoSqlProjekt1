using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MongoDB.Driver;
using JetstreamService.API.Models;


namespace JetstreamService.Migration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starte Datenmigration von SQL nach MongoDB...");

            // SQL-Verbindungszeichenfolge – bitte ggf. anpassen
            string sqlConnectionString = "Server=localhost;Database=JetstreamService;User Id=sa;Password=TunaCan2005;";
            // MongoDB-Verbindungszeichenfolge (ersetze <password> mit dem tatsächlichen Passwort)
            string mongoConnectionString = "mongodb://MyMigrationUser:<password>@localhost:27017";
            string mongoDatabaseName = "JetstreamDB";

            // Verbindung zu MongoDB
            var mongoClient = new MongoClient(mongoConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDatabaseName);
            var ordersCollection = mongoDatabase.GetCollection<ServiceOrder>("ServiceOrders");

            List<ServiceOrder> serviceOrders = new List<ServiceOrder>();

            // Daten aus der SQL-Datenbank lesen
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                sqlConnection.Open();
                string query = "SELECT Id, Kundenname, Email, Telefon, Prioritaet, Dienstleistung, Status FROM ServiceOrders";
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ServiceOrder order = new ServiceOrder
                            {
                                // Id wird von MongoDB generiert; weitere Felder werden aus SQL übernommen
                                Kundenname = reader["Kundenname"].ToString(),
                                Email = reader["Email"].ToString(),
                                Telefon = reader["Telefon"].ToString(),
                                Prioritaet = reader["Prioritaet"].ToString(),
                                Dienstleistung = Enum.TryParse(reader["Dienstleistung"].ToString().Replace(" ", ""), out Dienstleistung d) ? d : Dienstleistung.KleinerService,
                                Status = Enum.TryParse(reader["Status"].ToString(), out ServiceStatus s) ? s : ServiceStatus.Offen
                            };
                            serviceOrders.Add(order);
                        }
                    }
                }
            }

            // Daten in MongoDB einfügen
            if (serviceOrders.Count > 0)
            {
                ordersCollection.InsertMany(serviceOrders);
                Console.WriteLine($"{serviceOrders.Count} Datensätze erfolgreich migriert.");
            }
            else
            {
                Console.WriteLine("Keine Datensätze zur Migration gefunden.");
            }

            Console.WriteLine("Datenmigration abgeschlossen.");
        }
    }
}
