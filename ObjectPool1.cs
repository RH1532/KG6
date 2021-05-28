using System;
using System.Collections.Concurrent;

namespace KG6
{
    public class ObjectPool<T>
    {
        private ConcurrentBag<T> _objects;
        private Func<T> _objectGenerator;

        public ObjectPool(Func<T> objectGenerator)
        {
            _objects = new ConcurrentBag<T>();
            _objectGenerator = objectGenerator ?? throw new ArgumentNullException(nameof(objectGenerator));
        }

        public T GetObject() => _objects.TryTake(out T item) ? item : _objectGenerator();

        public void PutObject(T item) => _objects.Add(item);
    }
}