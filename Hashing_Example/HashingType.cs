using System;
using System.Collections.Generic;
using System.Text;

namespace Hashing_Example
{
    class HashingType
    {
        private string _type;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        private bool _managed;

        public bool Managed
        {
            get { return _managed; }
            set { _managed = value; }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
