namespace RFTools.Extensions;

public static class UnitConverter
{
    public static double  ToDecibelMilliwatts(this double power)
    {
        return 10 * Math.Log10(power);
    }
}