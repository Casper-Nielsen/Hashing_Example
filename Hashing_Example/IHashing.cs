using System;
using System.Collections.Generic;
using System.Text;

namespace Hashing_Example
{
    interface IHashing
    {
        void SetHashingType(string hashingType);
        long ComputeMAC(ref byte[] message, byte[] key);
        bool Validate(byte[] mac1, byte[] mac2);
    }
}
