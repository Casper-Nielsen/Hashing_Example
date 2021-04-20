using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace Hashing_Example
{
    class Hashing : IHashing
    {
        private HashAlgorithm hash;
        
        public Hashing()
        {
            SetHashingType("sha1");
        }
        public Hashing(string hashingType)
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
            hash = hashingType.ToLower() switch
            {
                "sha1" => new SHA1Managed(),
                "sha256" => new SHA256Managed(),
                "sha384" => new SHA384Managed(),
                "sha512" => new SHA512Managed(),
                _ => new SHA1Managed(),
            };
        }

        /// <summary>
        /// hashes the message with the hashing type that is set
        /// </summary>
        /// <param name="message">the message that will be hashed</param>
        /// <param name="key">not used</param>
        /// <returns>the hashed data</returns>
        public long ComputeMAC(ref byte[] message, byte[] key = null)
        {
            Stopwatch stopwatch = new Stopwatch();
            byte[] mac = new byte[0];
            try
            {
                stopwatch.Start();
                mac = hash.ComputeHash(message);
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
