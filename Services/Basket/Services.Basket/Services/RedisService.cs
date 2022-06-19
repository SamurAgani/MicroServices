using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Basket.Services
{
    public class RedisService
    {
        public readonly string _host;
        public readonly int _port;

        private ConnectionMultiplexer connectionMultiplexer;

        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;
        }
        public void Connect() => connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");

        public IDatabase GetDb(int db = 1) => connectionMultiplexer.GetDatabase(db);

    }
}
