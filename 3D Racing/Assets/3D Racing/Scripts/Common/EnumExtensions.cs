using System;

public static class EnumExtensions
{
    public static T Next<T>(this T src) where T : struct
    {
        if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

        T[] Arr = (T[])Enum.GetValues(src.GetType());
        int nextIndex = Array.IndexOf<T>(Arr, src) + 1;
        return (nextIndex == Arr.Length) ? Arr[0] : Arr[nextIndex];
    }

    public static T Previous<T>(this T src) where T : struct
    {
        if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

        T[] Arr = (T[])Enum.GetValues(src.GetType());
        int nextIndex = Array.IndexOf<T>(Arr, src) - 1;
        return (nextIndex == -1) ? Arr[Arr.Length - 1] : Arr[nextIndex];
    }
}
