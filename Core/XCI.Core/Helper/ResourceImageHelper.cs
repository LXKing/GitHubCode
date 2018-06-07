using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace XCI.Helper
{
    /// <summary>
    /// ����ͼƬ��Դ����������
    /// </summary>
    public class ResourceImageHelper
    {
        /// <summary>
        /// �������ƻ�ȡ�����Դ
        /// </summary>
        /// <param name="name">��Դ����</param>
        /// <param name="asm">���ڳ���</param>
        /// <exception cref="ArgumentException"></exception>
        public static Cursor CreateCursorFromResources(string name, System.Reflection.Assembly asm)
        {
            System.IO.Stream stream = asm.GetManifestResourceStream(name);
            if (stream == null)
            {
                throw new ArgumentException("ָ������Դ���Ʋ�����");
            }
            return new Cursor(stream);
        }


        /// <summary>
        /// �ָ�ָ����ͼƬ��Դ��ImageList
        /// </summary>
        /// <param name="image">ͼƬ�ļ�</param>
        /// <param name="size">����ͼƬ��С</param>
        public static ImageList CreateImageListFromResources(Image image, Size size)
        {
            return CreateImageListFromResources(image, size, Color.Empty);
        }


        /// <summary>
        /// �ָ�ָ����ͼƬ��Դ��ImageList
        /// </summary>
        /// <param name="image">ͼƬ�ļ�</param>
        /// <param name="size">����ͼƬ��С</param>
        /// <param name="transparent">͸��ɫ</param>
        public static ImageList CreateImageListFromResources(Image image, Size size, Color transparent)
        {
            return CreateImageListFromResources(image, size, transparent, ColorDepth.Depth8Bit);
        }


        /// <summary>
        /// �ָ�ָ����ͼƬ��Դ��ImageList
        /// </summary>
        /// <param name="image">ͼƬ�ļ�</param>
        /// <param name="size">����ͼƬ��С</param>
        /// <param name="transparent">͸��ɫ</param>
        /// <param name="depth">��ɫ���</param>
        public static ImageList CreateImageListFromResources(Image image, Size size, Color transparent, ColorDepth depth)
        {
            if (transparent == Color.Empty) transparent = Color.Empty;
            ImageList images = new ImageList();
            images.ColorDepth = depth;
            images.ImageSize = size.IsEmpty ? new Size(16, 16) : size;
            var bitmap = image as Bitmap;
            if (bitmap != null)
            {
                (bitmap).MakeTransparent(transparent);
            }
            images.Images.AddStrip(image);
            return images;
        }


        /// <summary>
        /// �ָ�ָ����ͼƬ��Դ��ImageList
        /// </summary>
        /// <param name="name">��Դ����</param>
        /// <param name="asm">���ڳ���</param>
        /// <param name="size">����ͼƬ��С</param>
        public static ImageList CreateImageListFromResources(string name, System.Reflection.Assembly asm, Size size)
        {
            return CreateImageListFromResources(name, asm, size, Color.Magenta);
        }


        /// <summary>
        /// �ָ�ָ����ͼƬ��Դ��ImageList
        /// </summary>
        /// <param name="name">��Դ����</param>
        /// <param name="asm">���ڳ���</param>
        /// <param name="size">����ͼƬ��С</param>
        /// <param name="transparent">͸��ɫ</param>
        public static ImageList CreateImageListFromResources(string name, System.Reflection.Assembly asm, Size size, Color transparent)
        {
            return CreateImageListFromResources(name, asm, size, transparent, ColorDepth.Depth8Bit);
        }


        /// <summary>
        /// �ָ�ָ����ͼƬ��Դ��ImageList
        /// </summary>
        /// <param name="name">��Դ����</param>
        /// <param name="asm">���ڳ���</param>
        /// <param name="size">����ͼƬ��С</param>
        /// <param name="transparent">͸��ɫ</param>
        /// <param name="depth">��ɫ���</param>
        public static ImageList CreateImageListFromResources(string name, System.Reflection.Assembly asm, Size size, Color transparent, ColorDepth depth)
        {
            if (transparent == Color.Empty) transparent = Color.Magenta;
            ImageList images = new ImageList();
            images.ColorDepth = depth;
            images.ImageSize = size.IsEmpty ? new Size(16, 16) : size;
            FillImageListFromResources(images, name, asm, transparent);
            return images;
        }
        

        /// <summary>
        /// ʹ��ָ����ͼƬ��Դ���ImageList
        /// </summary>
        /// <param name="images">ImageList����</param>
        /// <param name="name">��Դ����</param>
        /// <param name="type">��Դ����</param>
        public static void FillImageListFromResources(ImageList images, string name, Type type)
        {
            FillImageListFromResources(images, GetResourceName(type, name), type.Assembly);
        }


        /// <summary>
        /// ʹ��ָ����ͼƬ��Դ���ImageList
        /// </summary>
        /// <param name="images">ImageList����</param>
        /// <param name="name">��Դ����</param>
        /// <param name="asm">���ڳ���</param>
        public static void FillImageListFromResources(ImageList images, string name, System.Reflection.Assembly asm)
        {
            FillImageListFromResources(images, name, asm, images.TransparentColor);
        }



        /// <summary>
        /// ʹ��ָ����ͼƬ��Դ���ImageList
        /// </summary>
        /// <param name="images">ImageList����</param>
        /// <param name="name">��Դ����</param>
        /// <param name="asm">���ڳ���</param>
        /// <param name="transparent">͸��ɫ</param>
        public static void FillImageListFromResources(ImageList images, string name, System.Reflection.Assembly asm, Color transparent)
        {
            Bitmap image = CreateBitmapFromResources(name, asm);
            image.MakeTransparent(transparent);
            images.Images.AddStrip(image);
        }

        
        /// <summary>
        /// ��ȡ��Դȫ��
        /// </summary>
        /// <param name="baseType">������</param>
        /// <param name="name">��Դ�ļ���</param>
        static string GetResourceName(Type baseType, string name)
        {
            return string.Format("{0}.{1}", baseType.Namespace, name);
        }
        
        
        /// <summary>
        /// ����Bitmap
        /// </summary>
        /// <param name="name">��Դ����</param>
        /// <param name="type">��Դ����</param>
        public static Bitmap CreateBitmapFromResources(string name, Type type)
        {
            return CreateBitmapFromResources(GetResourceName(type, name), type.Assembly);
        }
        

        /// <summary>
        /// ����Bitmap
        /// </summary>
        /// <param name="name">��Դ����</param>
        /// <param name="asm">��������</param>
        public static Bitmap CreateBitmapFromResources(string name, System.Reflection.Assembly asm)
        {
            return (Bitmap)CreateImageFromResources(name, asm);
        }


        /// <summary>
        /// ����Image
        /// </summary>
        /// <param name="name">��Դ����</param>
        /// <param name="type">��Դ����</param>
        /// <exception cref="ArgumentException">ָ������Դ���Ʋ�����</exception>
        public static Image CreateImageFromResources(string name, Type type)
        {
            return CreateImageFromResources(GetResourceName(type, name), type.Assembly);
        }


        /// <summary>
        ///  ����Image
        /// </summary>
        /// <param name="name">��Դ����</param>
        /// <param name="asm">��������</param>
        /// <exception cref="ArgumentException">ָ������Դ���Ʋ�����</exception>
        public static Image CreateImageFromResources(string name, System.Reflection.Assembly asm)
        {
            System.IO.Stream stream = asm.GetManifestResourceStream(name);
            if (stream == null)
            {
                return null;
            }
            Image image = Image.FromStream(stream);
            return image;
        }


        /// <summary>
        /// ����͸��Image
        /// </summary>
        /// <param name="rbgSource"></param>
        /// <param name="alphaSource"></param>
        /// <param name="asm">��������</param>
        public static Image CreateTransparentImageFromResources(string rbgSource, string alphaSource, System.Reflection.Assembly asm)
        {
            Stream stream = asm.GetManifestResourceStream(rbgSource);
            if (stream == null)
            {
                throw new ArgumentException("ָ������Դ���Ʋ�����", rbgSource);
            }
            Bitmap rgb = (Bitmap)Image.FromStream(stream);
            int width = rgb.Width;
            int height = rgb.Height;
            BitmapData drgb = rgb.LockBits(new Rectangle(Point.Empty, rgb.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            byte[] data = new byte[drgb.Stride * rgb.Height];
            Marshal.Copy(drgb.Scan0, data, 0, data.Length);
            rgb.UnlockBits(drgb);
            rgb.Dispose();

            stream = asm.GetManifestResourceStream(alphaSource);
            if (stream == null)
            {
                throw new ArgumentException("ָ������Դ���Ʋ�����", alphaSource);
            }
            Bitmap a = (Bitmap)Image.FromStream(stream);
            if (a.Width != width || a.Height != height)
                throw new ArgumentException();
            BitmapData da = a.LockBits(new Rectangle(Point.Empty, a.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            byte[] adata = new byte[da.Stride * a.Height];
            Marshal.Copy(da.Scan0, adata, 0, adata.Length);
            a.UnlockBits(da);
            a.Dispose();
            for (int i = 0; i < data.Length; i += 4)
                data[i + 3] = adata[i];
            Bitmap image = new Bitmap(width, height);
            BitmapData dimg = image.LockBits(new Rectangle(Point.Empty, image.Size), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(data, 0, dimg.Scan0, data.Length);
            image.UnlockBits(dimg);
            return image;
        }
        

        /// <summary>
        /// �������ƻ�ȡIcon��Դ
        /// </summary>
        /// <param name="name">��Դ����</param>
        /// <param name="type">��Դ����</param>
        /// <exception cref="ArgumentException">ָ������Դ���Ʋ�����</exception>
        public static Icon CreateIconFromResources(string name, Type type)
        {
            return CreateIconFromResources(GetResourceName(type, name), type.Assembly);
        }


        /// <summary>
        /// �������ƻ�ȡIcon��Դ
        /// </summary>
        /// <param name="name">��Դ����</param>
        /// <param name="asm">��������</param>
        /// <exception cref="ArgumentException">ָ������Դ���Ʋ�����</exception>
        public static Icon CreateIconFromResources(string name, System.Reflection.Assembly asm)
        {
            System.IO.Stream stream = asm.GetManifestResourceStream(name);
            if (stream == null)
            {
                throw new ArgumentException("ָ������Դ���Ʋ�����");
            }
            Icon icon = new Icon(stream);
            return icon;
        }
    }
}