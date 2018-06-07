using System.Diagnostics;
using System.Threading;
using System;

namespace XCI.Helper
{
    /// <summary>
    /// 读写锁操作帮助类
    /// </summary>
    public class ReaderWriterLockHelper
    {
        #region 字段

        private ReaderWriterLock _readwriteLock = new ReaderWriterLock();
        private const int _lockMilliSecondsForRead = 1000;
        private const int _lockMilliSecondsForWrite = 1000;

        #endregion

        #region 同步方法

        /// <summary>
        /// 读操作
        /// </summary>
        /// <param name="executor">操作</param>
        public void ExecuteRead(Action executor)
        {
            AcquireReaderLock();
            try
            {
                executor();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("无法执行日志记录器集合读操作" + ex.Message);
            }
            finally
            {
                ReleaseReaderLock();
            }
        }


        /// <summary>
        /// 写操作
        /// </summary>
        /// <param name="executor">操作</param>
        public void ExecuteWrite(Action executor)
        {
            AcquireWriterLock();
            try
            {
                executor();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("无法执行日志记录器集合写操作" + ex.Message);
            }
            finally
            {
                ReleaseWriterLock();
            }
        }


        /// <summary>
        /// 获取读锁
        /// </summary>
        protected void AcquireReaderLock()
        {
            _readwriteLock.AcquireReaderLock(_lockMilliSecondsForRead);
        }


        /// <summary>
        /// 释放读锁
        /// </summary>
        protected void ReleaseReaderLock()
        {
            _readwriteLock.ReleaseReaderLock();
        }


        /// <summary>
        /// 获取写锁
        /// </summary>
        protected void AcquireWriterLock()
        {
            _readwriteLock.AcquireWriterLock(_lockMilliSecondsForWrite);
        }


        /// <summary>
        /// 释放写锁
        /// </summary>
        protected void ReleaseWriterLock()
        {
            _readwriteLock.ReleaseWriterLock();
        }

        #endregion

    }
}