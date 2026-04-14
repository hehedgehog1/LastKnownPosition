using System;

namespace Models
{
    [Serializable]
    public class Ring
    {
        public int Id;
        public float Radius;
        public int Weight;
        public Location? Location;
        public Location? ChildLocation;
    }
}