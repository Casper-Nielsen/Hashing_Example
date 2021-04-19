using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Hashing_Example_Console
{
    class Hashing
    {
        private HMAC hmac;
        /// <summary>
        /// sets the hashing type
        /// </summary>
        /// <param name="hashingType">the hashing type</param>
        public void SetHashingType(string hashingType)
        {
            switch (hashingType)
            {
                case "sha1":
                    hmac = new HMACSHA1();
                    break;
                case "sha256":
                    hmac = new HMACSHA256();
                    break;
                case "sha384":
                    hmac = new HMACSHA384();
                    break;
                case "sha512":
                    hmac = new HMACSHA512();
                    break;
                case "md5":
                    hmac = new HMACMD5();
                    break;
                default:
                    hmac = new HMACSHA1();
                    break;
            }
        }

        /// <summary>
        /// hashes the message with the hashing type that is set using the key
        /// </summary>
        /// <param name="message">the message that will be hashed</param>
        /// <param name="key">the key for the hashing</param>
        /// <returns>the hashed data</returns>
        public byte[] ComputeMAC(byte[] message, byte[] key)
        {
            byte[] mac = new byte[0];
            try
            {
                hmac.Key = key;
                mac = hmac.ComputeHash(message);
            }
            catch { }

            return mac;
        }

        public bool Validate(byte[] mac1, byte[] mac2)
        {
            return mac1.Equals(mac2);
        }
    }
}
