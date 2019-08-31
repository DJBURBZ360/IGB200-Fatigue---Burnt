public static class Percentage
{
    public static float GetPercentage(float current, float original, float oldValue)
    {
        float difference = original - current;
        float percent = (difference / original) * 100;

        if (percent != oldValue) return percent;
        else return oldValue;
    }
}
