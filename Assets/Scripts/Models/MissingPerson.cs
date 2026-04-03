using System;
using System.Collections.Generic;

namespace Models
{
    [Serializable]
    public struct MissingPerson
    {
        public Location Location;
        public List<Ring> Rings;
    }
}