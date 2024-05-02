using CoreWCF;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Contract
{
    [DataContract]
    public class EchoFault
    {
        private string text;

        [DataMember]
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }

    [ServiceContract]
    public interface IEchoService
    {
        [OperationContract]
        string Echo(string text);

        [OperationContract]
        string ComplexEcho(EchoMessage text);

        [OperationContract]
        [FaultContract(typeof(EchoFault))]
        string FailEcho(string text);

        [OperationContract]
        string EchoForPermission(string text);

        [OperationContract]
        Task<string> EchoTrial(string text); // New asynchronous method
    }

    [DataContract]
    public class EchoMessage
    {
        [DataMember]
        public string Text { get; set; }
    }
}
