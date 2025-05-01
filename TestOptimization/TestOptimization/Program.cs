internal class Program
{
    static void Main()
    {
        DictionaryMemoryTest();
    }

    static void DictionaryMemoryTest()
    {
        GC.Collect();
        long beforeList = GC.GetTotalMemory(true);

        var list = new List<KeyValuePair<int, string>>();
        for (int i = 0; i < 10000; i++)
        {
            list.Add(new KeyValuePair<int, string>(i, "value_" + i));
        }

        long afterList = GC.GetTotalMemory(true);
        Console.WriteLine($"List Memory Used: {afterList - beforeList} bytes");

        GC.Collect();
        long beforeDict = GC.GetTotalMemory(true);

        var dict = new Dictionary<int, string>();
        for (int i = 0; i < 10000; i++)
        {
            dict[i] = "value_" + i;
        }

        long afterDict = GC.GetTotalMemory(true);
        Console.WriteLine($"Dictionary Memory Used: {afterDict - beforeDict} bytes");
    }
}

