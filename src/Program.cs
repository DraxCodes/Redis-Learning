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
            var text = await redisStuff.GetTest();

            if (text is null)
            {
                Console.WriteLine("IT FAILED!");
            }
            else
            {
                Console.WriteLine(text);
            }

            await redisStuff.CreateTest("Test Input");
            Console.WriteLine("YAY");
            Console.ReadLine();
        }
    }

    internal class RedisStuff
    {
        public RedisStuff()
        {
            Redis.Default.DataFormater = new JsonFormater();
            Redis.Default.Host.AddWriteHost("192.168.0.17").Password = "pass123";
        }

        internal async Task<string> GetTest()
        {
            return await Redis.Get<string>("test");
        }

        internal async Task CreateTest(string input)
        {
            await Redis.Set("Test Create", input);
        }
    }
}
