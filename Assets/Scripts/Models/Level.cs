using System;

namespace Models
{
    [Serializable]
    public struct Level
    {
        public int Id;

        public bool IsTutorial;

        public MissingPerson MissingPerson;

    }
}