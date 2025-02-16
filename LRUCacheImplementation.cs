using System;
using System.Collections.Generic;

public class LRUCache
{
    private readonly int capacity;
    private readonly Dictionary<int, LinkedListNode<(int key, int value)>> cache;
    private readonly LinkedList<(int key, int value)> order;

    public LRUCache(int capacity)
    {
        this.capacity = capacity;
        cache = new Dictionary<int, LinkedListNode<(int key, int value)>>();
        order = new LinkedList<(int key, int value)>();
    }

    public int Get(int key)
    {
        if (!cache.ContainsKey(key))
        {
            return -1;
        }

        var node = cache[key];
        order.Remove(node);
        order.AddFirst(node);
        return node.Value.value;
    }

    public void Put(int key, int value)
    {
        if (cache.ContainsKey(key))
        {
            var existingNode = cache[key];
            order.Remove(existingNode);
            cache.Remove(key);
        }
        else if (cache.Count >= capacity)
        {
            var lastNode = order.Last;
            cache.Remove(lastNode.Value.key);
            order.RemoveLast();
        }

        var newNode = new LinkedListNode<(int key, int value)>((key, value));
        order.AddFirst(newNode);
        cache[key] = newNode;
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
