using System;
using System.Collections.Generic;

namespace Assets.Common.Scripts.Data
{
    class ValueDict<T0, T1> : IValueDict<T0, T1>
    {
        private Dictionary<T0, T1> _dictionary;

        public void Add(T0 id, T1 item)
        {
            if (Contains(id)) throw new Exception("ValueDict<"+typeof(T0)+","+ typeof(T1)+"> already contains such key");
        }

        public void Remove(T0 id)
        {
            if (!Contains(id)) throw new Exception("ValueDict<" + typeof(T0) + "," + typeof(T1) + "> does not contain such key");
        }

        public bool Contains(T0 id)
        {
            return _dictionary.ContainsKey(id);
        }

        public T1 Get(T0 id)
        {
            if (!Contains(id)) throw new Exception("ValueDict<" + typeof(T0) + "," + typeof(T1) + "> does not contain such key");
            return _dictionary[id];
        }
    }
}
