using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR;

namespace HtmlBroadcast
{
    /// <summary>
    /// Persistent connection implementation.
    /// </summary>
    public class StreamingConnection : PersistentConnection
    {
        /// <summary>
        /// Handles the data that was received from the client
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected override System.Threading.Tasks.Task OnReceivedAsync(string clientId, string data)
        {
            // Broadcast the data back to the clients.
            // Note: this uses async mechanisms through the TPL
            return Connection.Broadcast(data);
        }
    }
}