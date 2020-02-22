using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyServer.Hubs
{
    public class StreamHub : Hub
    {
        public async IAsyncEnumerable<string> SendDataRow()
        {
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(100);
                yield return i.ToString();
            }
        }
        
    }
}
