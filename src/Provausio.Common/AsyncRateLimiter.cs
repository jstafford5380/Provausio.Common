using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Provausio.Common
{
    public class AsyncRateLimiter : IRateLimiter
    {
        private readonly int _maxRequests;
        private readonly TimeSpan _interval;
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        private readonly Dictionary<string, Queue<DateTime>> _internalStorage =
            new Dictionary<string, Queue<DateTime>>();

        public AsyncRateLimiter(int maxRequests, TimeSpan interval)
        {
            _maxRequests = maxRequests;
            _interval = interval;
        }

        public async Task Wait(string key)
        {
            await Task.Run(async () =>
            {
                while (!CanProcess(key))
                    await Task.Delay(1);

            }).ConfigureAwait(false);
        }

        private bool CanProcess(string key)
        {
            _lock.EnterUpgradeableReadLock();
            try
            {
                if (!_internalStorage.ContainsKey(key))
                {
                    _lock.EnterWriteLock();
                    try
                    {
                        _internalStorage.Add(key, new Queue<DateTime>());
                        _internalStorage[key].Enqueue(DateTime.Now);
                        return true;
                    }
                    finally
                    {
                        _lock.ExitWriteLock();
                    }
                }
                else
                {
                    Purge(key);

                    _lock.EnterWriteLock();
                    try
                    {
                        if (_internalStorage[key].Count == _maxRequests)
                            return false;

                        _internalStorage[key].Enqueue(DateTime.Now);
                        return _internalStorage[key].Count <= _maxRequests;
                    }
                    finally
                    {
                        _lock.ExitWriteLock();
                    }
                }
            }
            finally
            {
                _lock.ExitUpgradeableReadLock();
            }
        }

        private void Purge(string key)
        {
            _lock.EnterWriteLock();
            try
            {
                // dequeue any entry that has elapsed
                while (_internalStorage[key].Count > 0 && (DateTime.Now - _internalStorage[key].Peek() > _interval))
                {
                    _internalStorage[key].Dequeue();
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}
