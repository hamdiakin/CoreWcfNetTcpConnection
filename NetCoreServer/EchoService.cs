using System.Threading.Tasks;
using Contract;
using CoreWCF;

namespace NetCoreServer
{
    public class EchoService : IEchoService
    {
        public string Echo(string text)
        {
            System.Console.WriteLine($"Received {text} from client!");
            return text;
        }

        public string ComplexEcho(EchoMessage text)
        {
            System.Console.WriteLine($"Received {text.Text} from client!");
            System.Console.WriteLine($"Numero {text.TextId} from client!");
            return text.Text;
        }

        public async Task<string> EchoTrial(string text)
        {
            // Simulate asynchronous operation (e.g., I/O-bound task)
            await Task.Delay(1000); // Simulate delay for demonstration

            System.Console.WriteLine($"Received {text} asynchronously from client!");
            return $"Async: {text}";
        }

        public string FailEcho(string text)
            => throw new FaultException<EchoFault>(new EchoFault() { Text = "WCF Fault OK" }, new FaultReason("FailReason"));

        [AuthorizeRole("CoreWCFGroupAdmin")]
        public string EchoForPermission(string echo)
        {
            return echo;
        }
    }
}
