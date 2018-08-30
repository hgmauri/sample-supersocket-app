using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

namespace Socket.Domain.Infra
{
    public class MyAppServer : AppServer<AppSession>
    {
        public MyAppServer()
            : base(new CommandLineReceiveFilterFactory(Encoding.Default, new CustomStringParser()))
        {
        }
    }

    public class CustomStringParser : IRequestInfoParser<StringRequestInfo>
    {
        public StringRequestInfo ParseRequestInfo(string source)
        {
            return new StringRequestInfo("", source, new[] { "" });
        }
    }
}
