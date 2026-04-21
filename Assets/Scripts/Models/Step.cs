using System;

namespace Models
{
    [Serializable]
    public class Step
    {
        public string Text;
        public int ContinueKey;

        public bool Completed;
    }
}