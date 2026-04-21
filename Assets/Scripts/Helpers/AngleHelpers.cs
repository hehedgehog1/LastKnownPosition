namespace Helpers
{
    public static class AngleHelpers
    {
        public static float Standardise(this float angle)
        {
            while (angle < 0f)
            {
                angle = 360 + angle;
            }
            
            while (angle >= 360)
            {
                angle = angle - 360;
            }

            return angle;
        }   
    }
}