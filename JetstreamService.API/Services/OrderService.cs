using JetstreamService.API.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace JetstreamService.API.Services
{
    public interface IOrderService
    {
        List<ServiceOrder> GetAllOrders();
        ServiceOrder GetOrderById(string id);
        ServiceOrder CreateOrder(ServiceOrder order);
        void UpdateOrder(string id, ServiceOrder orderIn);
        void DeleteOrder(string id);
    }

    public class OrderService : IOrderService
    {
        private readonly IMongoCollection<ServiceOrder> _orders;

        public OrderService(IConfiguration config)
        {
            var client = new MongoClient(config.GetSection("MongoDB:ConnectionString").Value);
            var database = client.GetDatabase(config.GetSection("MongoDB:Database").Value);
            _orders = database.GetCollection<ServiceOrder>("ServiceOrders");

            // Index auf Kundenname für schnelle Suchabfragen
            var indexKeys = Builders<ServiceOrder>.IndexKeys.Ascending(o => o.Kundenname);
            _orders.Indexes.CreateOne(new CreateIndexModel<ServiceOrder>(indexKeys));
        }

        public List<ServiceOrder> GetAllOrders() =>
            _orders.Find(order => true).ToList();

        public ServiceOrder GetOrderById(string id) =>
            _orders.Find<ServiceOrder>(order => order.Id == id).FirstOrDefault();

        public ServiceOrder CreateOrder(ServiceOrder order)
        {
            _orders.InsertOne(order);
            return order;
        }

        public void UpdateOrder(string id, ServiceOrder orderIn) =>
            _orders.ReplaceOne(order => order.Id == id, orderIn);

        public void DeleteOrder(string id) =>
            _orders.DeleteOne(order => order.Id == id);
    }
}
