﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using BitPoker.Models.Contracts;

namespace BitPoker.API.Repository
{
    public class InMemoryTableRepo : BitPoker.Repository.ITableRepository
    {
        private const String KEY = "TableContainer";

        public IEnumerable<Table> All()
        {
            if (MemoryCache.Default.Contains(KEY))
            {
                Models.TableContainer container = (Models.TableContainer)MemoryCache.Default["TableContainer"];
                return container.Tables;
            }
            else
            {
                return null;
            }
        }

        public Table Find(Guid id)
        {
            if (MemoryCache.Default.Contains(KEY))
            {
                Models.TableContainer container = (Models.TableContainer)MemoryCache.Default["TableContainer"];
                
                if (container != null)
                {
                    return container.Tables.FirstOrDefault(t => t.Id.ToString() == id.ToString());
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public void Add(Table item)
        {
            if (MemoryCache.Default.Contains("TableContainer"))
            {
                Models.TableContainer container = (Models.TableContainer)MemoryCache.Default["TableContainer"];
                container.Tables.Add(item);

                CacheItemPolicy policy = new CacheItemPolicy();
                policy.SlidingExpiration.Add(new TimeSpan(0, 30, 0));
                MemoryCache.Default.Add(KEY, container, policy);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~InMemoryTableRepo() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}