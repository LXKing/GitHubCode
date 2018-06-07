using System.Collections.Generic;
using System.IO;
using SharpZipLib;
using XCI.Core;

namespace XCI.Helper
{
    public static class ZipHelper
    {
        /// <summary>
        /// 压缩单个文件
        /// </summary>
        /// <param name="zipPath">压缩文件路径</param>
        /// <param name="filePath">待压缩的文件路径</param>
        public static void ZipFile(string zipPath, string filePath)
        {
            using (FileStream fileStreamIn = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (FileStream fileStreamOut = new FileStream(zipPath, FileMode.Create, FileAccess.Write))
                {
                    using (ZipOutputStream zipOutStream = new ZipOutputStream(fileStreamOut))
                    {
                        byte[] buffer = new byte[4096];
                        ZipEntry entry = new ZipEntry(Path.GetFileName(filePath));
                        zipOutStream.PutNextEntry(entry);
                        int size;
                        do
                        {
                            size = fileStreamIn.Read(buffer, 0, buffer.Length);
                            zipOutStream.Write(buffer, 0, size);
                        } while (size > 0);
                        zipOutStream.Flush();
                    }
                }
            }
        }

        /// <summary>
        /// 解压单个文件
        /// </summary>
        /// <param name="zipPath">压缩文件路径</param>
        /// <param name="outDir">解压目录</param>
        public static void ExtractFile(string zipPath, string outDir)
        {
            using (FileStream fileStreamIn = new FileStream(zipPath, FileMode.Open, FileAccess.Read))
            {
                using (ZipInputStream zipInStream = new ZipInputStream(fileStreamIn))
                {
                    ZipEntry entry = zipInStream.GetNextEntry();
                    using (FileStream fileStreamOut = new FileStream(Path.Combine(outDir, entry.Name), FileMode.Create, FileAccess.Write))
                    {
                        int size;
                        byte[] buffer = new byte[4096];
                        do
                        {
                            size = zipInStream.Read(buffer, 0, buffer.Length);
                            fileStreamOut.Write(buffer, 0, size);
                        } while (size > 0);
                        fileStreamOut.Flush();
                    }
                }
            }
        }

        /// <summary>
        /// 压缩多个文件
        /// </summary>
        /// <param name="zipPath">压缩文件路径</param>
        /// <param name="filePaths">待压缩的文件路径</param>
        public static void ZipFiles(string zipPath, params string[] filePaths)
        {
            ZipStorer zip = ZipStorer.Create(zipPath, string.Empty);
            zip.EncodeUTF8 = true;
            foreach (var path in filePaths)
            {
                zip.AddFile(ZipStorer.Compression.Deflate, path, Path.GetFileName(path), string.Empty);
            }
            zip.Close();
        }

        /// <summary>
        /// 解压多个文件
        /// </summary>
        /// <param name="zipPath">压缩文件路径</param>
        /// <param name="outDir">解压目录</param>
        public static void ExtractFiles(string zipPath, string outDir)
        {
            ZipStorer zip = ZipStorer.Open(zipPath, FileAccess.Read);
            zip.EncodeUTF8 = true;
            List<ZipStorer.ZipFileEntry> dir = zip.ReadCentralDir();
            foreach (ZipStorer.ZipFileEntry entry in dir)
            {
                string path = Path.Combine(outDir, Path.GetFileName(entry.FilenameInZip));
                zip.ExtractFile(entry, path);
            }
            zip.Close();
        }

        /// <summary>
        /// 压缩目录
        /// </summary>
        /// <param name="zipPath">压缩文件路径</param>
        /// <param name="folderPath">压缩目录</param>
        public static void ZipFolder(string zipPath, string folderPath)
        {
            FastZipEvents args = new FastZipEvents();
            //args.Progress = new ICSharpCode.SharpZipLib.Core.ProgressHandler((o, e) =>
            //{
            //    Debug.WriteLine(DateTime.Now + "进度=" + e.PercentComplete);
            //});
            //args.ProcessFile = new ProcessFileHandler((o, e) =>
            //{
            //    Debug.WriteLine(DateTime.Now + "名称=" + e.Name);
            //});
            FastZip fastZip = new FastZip(args);
            //fastZip.CreateEmptyDirectories = true;
            fastZip.RestoreAttributesOnExtract = true;
            fastZip.RestoreDateTimeOnExtract = true;
            fastZip.CreateZip(zipPath, folderPath, true, "");
        }

        /// <summary>
        /// 解压目录
        /// </summary>
        /// <param name="zipPath">压缩文件路径</param>
        /// <param name="folderPath">解压目录</param>
        public static void ExtractFolder(string zipPath, string folderPath)
        {
            FastZipEvents args = new FastZipEvents();
            //args.Progress = new ICSharpCode.SharpZipLib.Core.ProgressHandler((o, e) =>
            //{
            //    Debug.WriteLine(DateTime.Now + "进度=" + e.PercentComplete);
            //});
            //args.ProcessFile = new ProcessFileHandler((o, e) =>
            //{
            //    Debug.WriteLine(DateTime.Now + "名称=" + e.Name);
            //});
            FastZip fastZip = new FastZip(args);
            fastZip.CreateEmptyDirectories = true;
            fastZip.RestoreAttributesOnExtract = true;
            fastZip.RestoreDateTimeOnExtract = true;
            fastZip.ExtractZip(zipPath, folderPath, "");
        }
    }
}