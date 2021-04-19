using System;
using System.Collections.Generic;
using System.Text;

namespace Hashing_Example
{
    class HashingType
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public HashingType() { }
        public HashingType(string name,int id) {
            this.Id = id;
            this.Name = name;
        }
    }
}
