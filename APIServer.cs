using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TetrisApiServer
{
    public class APIServer
    {
        static public void Init(ServerOptions options)
        {
            DBRedis.Init(options.RedisName, options.RedisAddress);
        }
    }
}
