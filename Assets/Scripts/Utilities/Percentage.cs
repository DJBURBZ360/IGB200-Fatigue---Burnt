public static class Percentage
{
    /// <summary>
    /// Returns the percentage based on from 0 to 100.
    /// </summary>
    public static float GetPercentage(float current, float original, float oldValue)
    {
        float difference = original - current;
        float percent = (difference / original) * 100;

        if (percent != oldValue) return percent;
        else return oldValue;
    }

    /// <summary>
    /// Returns the percentage based on from 100 to 0.
    /// </summary>
    public static float GetReversePercentage(float current, float original, float oldValue)
    {
        float difference = original - current;
        float percent = 100 - ((difference / original) * 100);

        if (percent != oldValue) return percent;
        else return oldValue;
    }
}
