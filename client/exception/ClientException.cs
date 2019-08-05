namespace ImportAPIClient.Client
{
    public class ClientException : System.Exception
    {
        private readonly System.Net.HttpStatusCode StatusCode;

        public ClientException(System.Net.HttpStatusCode statusCode) {
            this.StatusCode = statusCode;
        }
        public ClientException(System.Net.HttpStatusCode statusCode, string message) : base(message) {
            this.StatusCode = statusCode;
        }
        public ClientException(System.Net.HttpStatusCode statusCode, string message, System.Exception inner) : base(message, inner) {
            this.StatusCode = statusCode;
        }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected ClientException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public System.Net.HttpStatusCode GetStatusCode()
        {
            return StatusCode;
        }
    }
}