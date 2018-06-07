using System.Diagnostics;
using System.Threading;
using System;

namespace XCI.Helper
{
    /// <summary>
    /// ��д������������
    /// </summary>
    public class ReaderWriterLockHelper
    {
        #region �ֶ�

        private ReaderWriterLock _readwriteLock = new ReaderWriterLock();
        private const int _lockMilliSecondsForRead = 1000;
        private const int _lockMilliSecondsForWrite = 1000;

        #endregion

        #region ͬ������

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="executor">����</param>
        public void ExecuteRead(Action executor)
        {
            AcquireReaderLock();
            try
            {
                executor();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("�޷�ִ����־��¼�����϶�����" + ex.Message);
            }
            finally
            {
                ReleaseReaderLock();
            }
        }


        /// <summary>
        /// д����
        /// </summary>
        /// <param name="executor">����</param>
        public void ExecuteWrite(Action executor)
        {
            AcquireWriterLock();
            try
            {
                executor();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("�޷�ִ����־��¼������д����" + ex.Message);
            }
            finally
            {
                ReleaseWriterLock();
            }
        }


        /// <summary>
        /// ��ȡ����
        /// </summary>
        protected void AcquireReaderLock()
        {
            _readwriteLock.AcquireReaderLock(_lockMilliSecondsForRead);
        }


        /// <summary>
        /// �ͷŶ���
        /// </summary>
        protected void ReleaseReaderLock()
        {
            _readwriteLock.ReleaseReaderLock();
        }


        /// <summary>
        /// ��ȡд��
        /// </summary>
        protected void AcquireWriterLock()
        {
            _readwriteLock.AcquireWriterLock(_lockMilliSecondsForWrite);
        }


        /// <summary>
        /// �ͷ�д��
        /// </summary>
        protected void ReleaseWriterLock()
        {
            _readwriteLock.ReleaseWriterLock();
        }

        #endregion

    }
}