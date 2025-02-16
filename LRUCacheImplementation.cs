using System;
using System.Collections.Generic;

public class LRUCache
{
    private readonly int capacity;
    private readonly Dictionary<int, (int value, DateTime timeStamp)> cache;
    private readonly SortedSet<(DateTime timeStamp, int key)> timeStamps;

    public LRUCache(int capacity)
    {
        this.capacity = capacity;
        cache = new Dictionary<int, (int value, DateTime timeStamp)>();
        timeStamps = new SortedSet<(DateTime, int)>();
    }

    public int Get(int key)
    {
        if (!cache.ContainsKey(key))
        {
            return -1;
        }

        var (value, oldTimeStamp) = cache[key];
        timeStamps.Remove((oldTimeStamp, key));
        var newTimeStamp = DateTime.UtcNow;
        cache[key] = (value, newTimeStamp);
        timeStamps.Add((newTimeStamp, key));

        return value;
    }

    public void Put(int key, int value)
    {
        if (cache.ContainsKey(key))
        {
            var (_, oldTimeStamp) = cache[key];
            timeStamps.Remove((oldTimeStamp, key));
        }
        else if (cache.Count >= capacity)
        {
            var oldest = timeStamps.Min;
            timeStamps.Remove(oldest);
            cache.Remove(oldest.key);
        }

        var newTimeStamp = DateTime.UtcNow;
        cache[key] = (value, newTimeStamp);
        timeStamps.Add((newTimeStamp, key));
    }
}

public class Program
{
    public static void Main()
    {
        LRUCache cache = new LRUCache(2);
        cache.Put(1, 1);
        cache.Put(2, 2);
        Console.WriteLine(cache.Get(1)); // returns 1
        cache.Put(3, 3); // evicts key 2
        Console.WriteLine(cache.Get(2)); // returns -1 (not found)
        cache.Put(4, 4); // evicts key 1
        Console.WriteLine(cache.Get(1)); // returns -1 (not found)
        Console.WriteLine(cache.Get(3)); // returns 3
        Console.WriteLine(cache.Get(4)); // returns 4
    }
}
