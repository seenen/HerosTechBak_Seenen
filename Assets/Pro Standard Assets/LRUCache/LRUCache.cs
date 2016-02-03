using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;


    public class LRUCache<K, V>
    {
        public delegate void BeforRemoveDelegate(V val);
        private BeforRemoveDelegate OnBeforRemove = null;

        public LRUCache(int capacity, BeforRemoveDelegate function)
        {
            this.capacity = capacity;
            this.OnBeforRemove = function;
        }

        public V Get(K key)
        {
            LinkedListNode<LRUCacheItem<K, V>> node;
            if (cacheMap.TryGetValue(key, out node))
            {
                //System.Console.WriteLine("Cache HIT " + key);
                V value = node.Value.value;

                lruList.Remove(node);
                lruList.AddLast(node);
                return value;
            }
            //System.Console.WriteLine("Cache MISS " + key);
            return default(V);
        }

        public void Add(K key, V val)
        {
            if (cacheMap.Count >= capacity)
            {
                removeFirst();
            }
            LRUCacheItem<K, V> cacheItem = new LRUCacheItem<K, V>(key, val);
            LinkedListNode<LRUCacheItem<K, V>> node = new LinkedListNode<LRUCacheItem<K, V>>(cacheItem);
            lruList.AddLast(node);
            cacheMap.Add(key, node);
        }

        public bool ContainsKey(K key)
        {
            return cacheMap.ContainsKey(key);
        }

        public void Clear()
        {
            while (cacheMap.Count > 0)
            {
                removeFirst();
            }
        }

        public void Update(K key, V val)
        {
            cacheMap[key].Value.value = val;
            //LinkedListNode<LRUCacheItem<K, V>> node;
            //if (cacheMap.TryGetValue(key, out node))
            //{
            //    //System.Console.WriteLine("Cache HIT " + key);
            //    node.Value.value = val;
            //}
        }

        public V GetFirst()
        {
            if (cacheMap.Count > 0)
            {
                LinkedListNode<LRUCacheItem<K, V>> node = lruList.First;

                if (node != null)
                {
                    V ret = node.Value.value;

                    lruList.Remove(node);

                    cacheMap.Remove(node.Value.key);

                    return ret;

                }
            }

            return default(V);
        }

        protected void removeFirst()
        {
            // Remove from LRUPriority
            LinkedListNode<LRUCacheItem<K, V>> node = lruList.First;
            if (this.OnBeforRemove != null) this.OnBeforRemove(node.Value.value);
            lruList.RemoveFirst();
            // Remove from cache
            cacheMap.Remove(node.Value.key);
        }

        int capacity;
        Dictionary<K, LinkedListNode<LRUCacheItem<K, V>>> cacheMap = new Dictionary<K, LinkedListNode<LRUCacheItem<K, V>>>();
        LinkedList<LRUCacheItem<K, V>> lruList = new LinkedList<LRUCacheItem<K, V>>();
    }


    internal class LRUCacheItem<K, V>
    {
        public LRUCacheItem(K k, V v)
        {
            key = k;
            value = v;
        }
        public K key;
        public V value;
    }
