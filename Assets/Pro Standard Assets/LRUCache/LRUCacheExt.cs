using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;


    public class LRUCacheExt<K, V>
    {
        public delegate void BeforRemoveDelegate(V val);
        private BeforRemoveDelegate OnBeforRemove = null;

        public LRUCacheExt(int capacity, BeforRemoveDelegate function)
        {
            this.capacity = capacity;
            this.OnBeforRemove = function;
        }

        public V Get(K key)
        {
            HashSet<LinkedListNode<LRUCacheItemExt<K, V>>> nodes;
            if (cacheMap.TryGetValue(key, out nodes))
            {
                //System.Console.WriteLine("Cache HIT " + key);
                var iter = nodes.GetEnumerator();
                iter.MoveNext();
                LinkedListNode<LRUCacheItemExt<K, V>> node = iter.Current;
                V value = node.Value.value;
                nodes.Remove(node);

                lruList.Remove(node);
                //lruList.AddLast(node);
                return value;
            }
            //System.Console.WriteLine("Cache MISS " + key);
            return default(V);
        }

        public void Add(K key, V val)
        {
            if (lruList.Count >= capacity)
            {
                removeFirst();
            }
            LRUCacheItemExt<K, V> cacheItem = new LRUCacheItemExt<K, V>(key, val);
            LinkedListNode<LRUCacheItemExt<K, V>> node = new LinkedListNode<LRUCacheItemExt<K, V>>(cacheItem);
            lruList.AddLast(node);
            if (!cacheMap.ContainsKey(key))
                cacheMap.Add(key, new HashSet<LinkedListNode<LRUCacheItemExt<K, V>>>());
            cacheMap[key].Add(node);
        }

        public bool ContainsKey(K key)
        {
            if (!cacheMap.ContainsKey(key))
                return false;
            else
            {
                return !(cacheMap[key].Count == 0);
            }
        }

        public void Clear()
        {
            while (cacheMap.Count > 0)
            {
                removeFirst();
            }
        }

        protected void removeFirst()
        {
            // Remove from LRUPriority
            LinkedListNode<LRUCacheItemExt<K, V>> node = lruList.First;
            if (this.OnBeforRemove != null) this.OnBeforRemove(node.Value.value);
            lruList.RemoveFirst();
            // Remove from cache
            //cacheMap.Remove(node.Value.key);
            cacheMap[node.Value.key].Remove(node);
        }

        int capacity;
        Dictionary<K, HashSet<LinkedListNode<LRUCacheItemExt<K, V>>>> cacheMap =
            new Dictionary<K, HashSet<LinkedListNode<LRUCacheItemExt<K, V>>>>();
        LinkedList<LRUCacheItemExt<K, V>> lruList = new LinkedList<LRUCacheItemExt<K, V>>();
    }


    internal class LRUCacheItemExt<K, V>
    {
        public LRUCacheItemExt(K k, V v)
        {
            key = k;
            value = v;
        }
        public K key;
        public V value;
    }
