using Grpc.Net.Client;
using InventoryServer;
using System;

namespace InventoryClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Trying calling the gRPC server http://inventory-service:8080 !");

            LineItem item = new LineItem()
            {
                Quantity = 10
            };

            Order req = new Order()
            {
                OrderId = 12
            };

            req.Items.Add(item);

            try
            {
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

                using var channel = GrpcChannel.ForAddress("http://inventory-service:8080");
                //using var channel = GrpcChannel.ForAddress("https://localhost:5001");
                var client = new Inventory.InventoryClient(channel);
                var reply = client.UpdateInventory(req);
                Console.WriteLine(reply.Message);
            }
            catch( Exception ex)
            {
                Console.WriteLine("Exception Occured");
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            Console.WriteLine("Connection Successful");
        }
    }
}
