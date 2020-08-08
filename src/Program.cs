using BeetleX.Redis;
using System;
using System.Threading.Tasks;

namespace Redis_Learning
{
    class Program
    {
        static async Task Main()
        {
            var redisStuff = new RedisStuff();
            var text = await redisStuff.GetStringAsync("test");

            Console.WriteLine(text);

            await redisStuff.CreateTestEntryAsync("Test Key", "Test Value");

            await redisStuff.ExpireInAsync("Test Key", TimeSpan.FromSeconds(10));

        }
    }

    internal class RedisStuff
    {
        public RedisStuff()
        {
            Redis.Default.DataFormater = new JsonFormater();
            Redis.Default.Host.AddWriteHost("192.168.0.17").Password = "pass123";
        }

        internal async Task<string> GetStringAsync(string key)
        {
            return await Redis.Get<string>(key);
        }

        internal async Task CreateTestEntryAsync(string key, string value)
        {
            await Redis.Set(key, value);
        }

        internal async Task ExpireInAsync(string key, TimeSpan time)
        {
            await Redis.Pexpire(key, (long)time.TotalMilliseconds);
        }
    }
}
