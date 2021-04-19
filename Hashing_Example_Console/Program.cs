using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Hashing_Example_Console
{
    class Program
    {
        public static object StopWatch { get; private set; }

        static void Main(string[] args)
        {
            List<string> times = new List<string>();
            bool managed = false;
            string type = "";
            string message = "";
            byte[] keyBytes = new byte[0];
            byte[] messageBytes = new byte[0];
            byte[] hashed = new byte[0];
            do
            {
                // Getting if it is managed
                Console.WriteLine("managed?(y/n)");
                managed = Console.ReadLine().ToLower() == "y";

                // Getting the encryption type
                Console.WriteLine("what encryption type do you use");
                Console.WriteLine("sha1/sha256/sha384/sha512/md5");
                type = GetType(Console.ReadLine());

                Hashing hashing = new Hashing();
                if (!managed)
                {
                    // If it is not managed get a key
                    hashing.SetHashingType(type);
                    Console.WriteLine("insert key (ASCII)");
                    keyBytes = Encoding.ASCII.GetBytes(Console.ReadLine());
                }
                else
                {
                    // Else set kashing type to a managed version
                    hashing.SetHashingType(type + "managed");
                }
                // Gets the message
                Console.WriteLine("message (ASCII)");
                message = Console.ReadLine();
                messageBytes = Encoding.ASCII.GetBytes(message);

                Stopwatch stopWatch = new Stopwatch();
                
                // Times the hashing event
                stopWatch.Start();
                hashed = hashing.ComputeMAC(messageBytes, keyBytes);
                stopWatch.Stop();
                // Writes the MAC out in ascii and hex
                Console.WriteLine("MAC:");
                Console.WriteLine("(ASCII)" + Encoding.ASCII.GetString(hashed));
                Console.WriteLine("(HEX)" + BitConverter.ToString(hashed));
                // Writes the time
                Console.WriteLine("time: " + stopWatch.ElapsedTicks + "Tick");
                // Adds the time to a list
                times.Add(type + " time: " + stopWatch.ElapsedTicks + "Tick");
            } while (Console.ReadLine() != "end");
            
            // Writes each time sat
            foreach (string item in times)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();

        }

        static string GetType(string input)
        {
            if (input.CompareTo("sha1") == 0)
            {
                return "sha1";

            }
            else if (input.CompareTo("sha256") == 0)
            {
                return "sha256";

            }
            else if (input.CompareTo("sha384") == 0)
            {
                return "sha384";

            }
            else if (input.CompareTo("sha512") == 0)
            {
                return "sha512";

            }
            else if (input.CompareTo("md5") == 0)
            {
                return "md5";

            }
            else
            {
                return "sha1";
            }
        }
    }
}
