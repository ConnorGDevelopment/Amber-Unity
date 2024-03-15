using System;

[Serializable]
public static class DisplayHelpers
{
    public static string ShowSign(int val)
    {
        return val >= 0 ? $"+{val}" : $"{val}";
    }

    public static string StatShorthand(CharStatName statName)
    {
        return statName.ToString().Substring(0, 3).ToUpper();
    }
}