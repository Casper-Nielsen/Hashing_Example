using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Hashing_Example
{
    class HmacHashing : IHashing
    {
        private HMAC hmac;

        public HmacHashing()
        {
            SetHashingType("sha1");
        }
        public HmacHashing(string hashingType)
        {
            SetHashingType(hashingType);
        }

        /// <summary>
        /// sets the hashing type
        /// </summary>
        /// <param name="hashingType">the hashing type</param>
        public void SetHashingType(string hashingType)
        {
            // Switch case to find the hashing class
            hmac = hashingType switch
            {
                "sha1" => new HMACSHA1(),
                "sha256" => new HMACSHA256(),
                "sha384" => new HMACSHA384(),
                "sha512" => new HMACSHA512(),
                "md5" => new HMACMD5(),
                _ => new HMACSHA1(),
            };
        }

        /// <summary>
        /// hashes the message with the hashing type that is set using the key
        /// </summary>
        /// <param name="message">the message that will be hashed</param>
        /// <param name="key">the key for the hashing</param>
        /// <returns>the hashed data</returns>
        public long ComputeMAC(ref byte[] message, byte[] key)
        {
            Stopwatch stopwatch = new Stopwatch();
            byte[] mac = new byte[0];
            try
            {
                hmac.Key = key;
                stopwatch.Start();
                mac = hmac.ComputeHash(message);
                stopwatch.Stop();
            }
            catch { }

            message = mac;
            return stopwatch.ElapsedTicks;
        }

        /// <summary>
        /// looks if the 2 byte arrays is the same
        /// </summary>
        /// <param name="mac1">the first array</param>
        /// <param name="mac2">the second array</param>
        /// <returns>if it is the same</returns>
        public bool Validate(byte[] mac1, byte[] mac2)
        {
            return mac1.SequenceEqual(mac2);
        }
    }
}
