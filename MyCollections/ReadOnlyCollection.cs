using System;

namespace MyCollections
{
    public class ReadOnlyCollection : Exception
    {
        public ReadOnlyCollection(): base("Collection is immutable") {}
        public ReadOnlyCollection(String str) : base(str) {}
    }
}