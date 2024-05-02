using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Contract;

namespace NetCoreClient
{
    public class Program
    {
        public const int NETTCP_PORT = 8089;

        static async Task Main(string[] args)
        {
            Console.Title = "WCF .Net Core Client";
            await CallNetTcpBinding($"net.tcp://localhost:{NETTCP_PORT}");
        }

        private static async Task CallNetTcpBinding(string hostAddr)
        {
            IClientChannel channel = null;

            var binding = new NetTcpBinding();

            var factory = new ChannelFactory<IEchoService>(binding, new EndpointAddress($"{hostAddr}/EchoService/netTcp"));
            factory.Open();
            try
            {
                IEchoService client = factory.CreateChannel();
                channel = client as IClientChannel;
                channel.Open();
                //var result = await client.Echo("Hello World!");
                var mes = new EchoMessage
                {
                    Text = "Hello",
                    TextId = 1,
                };
                var result = await client.ComplexEcho(mes); // Invoke the asynchronous method
                channel.Close();
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                factory.Abort(); // Abort factory
                if (channel != null && channel.State != CommunicationState.Closed)
                    channel.Abort(); // Abort channel
            }
            finally
            {
                factory.Close(); // Close factory
            }

        }
    }
}
