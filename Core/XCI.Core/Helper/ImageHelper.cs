using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace XCI.Helper
{
    /// <summary>
    /// 图片管理
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// 获取图片得缩略图
        /// </summary>
        /// <param name="originalImage">原始图片(Image)</param>
        /// <param name="targetWidth">缩略图宽度</param>
        /// <param name="targetHeight">缩略图高度</param>
        /// <returns>返回产生的缩略图(Image)</returns>
        public static Image CreateImageThumbnail(Image originalImage, int targetWidth = 100, int targetHeight = 100)
        {
            Bitmap finalImage = null;
            Graphics graphic = null;
            int width = originalImage.Width;
            int height = originalImage.Height;
            int newWidth, newHeight;

            float targetRatio = targetWidth / (float)targetHeight;
            float imageRatio = width / (float)height;

            if (targetRatio > imageRatio)
            {
                newHeight = targetHeight;
                newWidth = (int)Math.Floor(imageRatio * targetHeight);
            }
            else
            {
                newHeight = (int)Math.Floor(targetWidth / imageRatio);
                newWidth = targetWidth;
            }

            newWidth = newWidth > targetWidth ? targetWidth : newWidth;
            newHeight = newHeight > targetHeight ? targetHeight : newHeight;

            //Image thumbnailImage = originalImage.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);
            
            finalImage = new Bitmap(targetWidth, targetHeight);
            graphic = Graphics.FromImage(finalImage);
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            graphic.Clear(Color.Transparent);//清空画布并以透明背景色填充    
            //graphic.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, targetWidth, targetHeight));
            int pasteX = (targetWidth - newWidth) / 2;
            int pasteY = (targetHeight - newHeight) / 2;
            graphic.DrawImage(originalImage, pasteX, pasteY, newWidth, newHeight);

            graphic.Dispose();

            return finalImage;
        }


        #region 图片水印

        /// <summary>
        /// 加图片水印
        /// </summary>
        /// <param name="originalImage">图像</param>
        /// <param name="watermarkImage">水印文件</param>
        /// <param name="savePath">目标文件保存路径</param>
        /// <param name="watermarkPosition">图片水印位置 0=不使用 1=左上 2=中上 3=右上 4=左中  9=右下</param>
        /// <param name="quality">附加图像质量，1-100</param>
        /// <param name="watermarkTransparency">水印的透明度 1--10 10为不透明</param>
        public static void ImageWatermark(Image originalImage, Image watermarkImage, string savePath, 
            int watermarkPosition = 9, int quality = 50, int watermarkTransparency = 10)
        {
            if (originalImage == null) throw new ArgumentNullException("originalImage");
            if (watermarkImage == null) throw new ArgumentNullException("watermarkImage");
            if (savePath == null) throw new ArgumentNullException("savePath");
            Graphics g = Graphics.FromImage(originalImage);
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Image watermark = new Bitmap(watermarkImage);

            if (watermark.Height >= originalImage.Height || watermark.Width >= originalImage.Width)
            {
                throw new Exception("水印图片的高度或者宽度不能超过原始图片的宽度或者高度");
            }

            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMap colorMap = new ColorMap();

            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = { colorMap };

            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            float transparency = 0.5F;
            if (watermarkTransparency >= 1 && watermarkTransparency <= 10)
            {
                transparency = (watermarkTransparency / 10.0F);
            }

            float[][] colorMatrixElements = {
                                                new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  0.0f,  0.0f,  transparency, 0.0f},
                                                new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                                            };

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            int xpos = 0;
            int ypos = 0;

            switch (watermarkPosition)
            {
                case 1:
                    xpos = (int)(originalImage.Width * (float).01);
                    ypos = (int)(originalImage.Height * (float).01);
                    break;
                case 2:
                    xpos = (int)((originalImage.Width * (float).50) - ((float)watermark.Width / 2));
                    ypos = (int)(originalImage.Height * (float).01);
                    break;
                case 3:
                    xpos = (int)((originalImage.Width * (float).99) - (watermark.Width));
                    ypos = (int)(originalImage.Height * (float).01);
                    break;
                case 4:
                    xpos = (int)(originalImage.Width * (float).01);
                    ypos = (int)((originalImage.Height * (float).50) - ((float)watermark.Height / 2));
                    break;
                case 5:
                    xpos = (int)((originalImage.Width * (float).50) - ((float)watermark.Width / 2));
                    ypos = (int)((originalImage.Height * (float).50) - ((float)watermark.Height / 2));
                    break;
                case 6:
                    xpos = (int)((originalImage.Width * (float).99) - (watermark.Width));
                    ypos = (int)((originalImage.Height * (float).50) - ((float)watermark.Height / 2));
                    break;
                case 7:
                    xpos = (int)(originalImage.Width * (float).01);
                    ypos = (int)((originalImage.Height * (float).99) - watermark.Height);
                    break;
                case 8:
                    xpos = (int)((originalImage.Width * (float).50) - ((float)watermark.Width / 2));
                    ypos = (int)((originalImage.Height * (float).99) - watermark.Height);
                    break;
                case 9:
                    xpos = (int)((originalImage.Width * (float).99) - (watermark.Width));
                    ypos = (int)((originalImage.Height * (float).99) - watermark.Height);
                    break;
            }

            g.DrawImage(watermark, new Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);
            //g.DrawImage(watermark, new System.Drawing.Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, System.Drawing.GraphicsUnit.Pixel);

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ici = null;
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType.IndexOf("jpeg") > -1)
                {
                    ici = codec;
                }
            }
            EncoderParameters encoderParams = new EncoderParameters();
            long[] qualityParam = new long[1];
            if (quality < 0 || quality > 100)
            {
                quality = 100;
            }
            qualityParam[0] = quality;

            EncoderParameter encoderParam = new EncoderParameter(Encoder.Quality, qualityParam);
            encoderParams.Param[0] = encoderParam;

            if (ici != null)
            {
                originalImage.Save(savePath, ici, encoderParams);
            }
            else
            {
                originalImage.Save(savePath);
            }

            g.Dispose();
            originalImage.Dispose();
            watermark.Dispose();
            imageAttributes.Dispose();
        }


        /// <summary>
        /// 增加文字水印
        /// </summary>
        /// <param name="originalImage">原始图片</param>
        /// <param name="watermarkText">水印文字</param>
        /// <param name="savePath">目标文件保存路径</param>
        /// <param name="watermarkPosition">图片水印位置 0=不使用 1=左上 2=中上 3=右上 4=左中  9=右下</param>
        /// <param name="quality">附加图像质量质量，1-100</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        public static void GetFontWatermark(Image originalImage, string watermarkText, string savePath,
            int watermarkPosition = 9, int quality = 50, string fontName = "宋体", int fontSize = 16)
        {
            //System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
            //    .FromFile(filename);
            Graphics g = Graphics.FromImage(originalImage);
            Font drawFont = new Font(fontName, fontSize, FontStyle.Regular, GraphicsUnit.Pixel);
            SizeF crSize = g.MeasureString(watermarkText, drawFont);

            float xpos = 0;
            float ypos = 0;

            switch (watermarkPosition)
            {
                case 1:
                    xpos = originalImage.Width * (float).01;
                    ypos = originalImage.Height * (float).01;
                    break;
                case 2:
                    xpos = (originalImage.Width * (float).50) - (crSize.Width / 2);
                    ypos = originalImage.Height * (float).01;
                    break;
                case 3:
                    xpos = (originalImage.Width * (float).99) - crSize.Width;
                    ypos = originalImage.Height * (float).01;
                    break;
                case 4:
                    xpos = originalImage.Width * (float).01;
                    ypos = (originalImage.Height * (float).50) - (crSize.Height / 2);
                    break;
                case 5:
                    xpos = (originalImage.Width * (float).50) - (crSize.Width / 2);
                    ypos = (originalImage.Height * (float).50) - (crSize.Height / 2);
                    break;
                case 6:
                    xpos = (originalImage.Width * (float).99) - crSize.Width;
                    ypos = (originalImage.Height * (float).50) - (crSize.Height / 2);
                    break;
                case 7:
                    xpos = originalImage.Width * (float).01;
                    ypos = (originalImage.Height * (float).99) - crSize.Height;
                    break;
                case 8:
                    xpos = (originalImage.Width * (float).50) - (crSize.Width / 2);
                    ypos = (originalImage.Height * (float).99) - crSize.Height;
                    break;
                case 9:
                    xpos = (originalImage.Width * (float).99) - crSize.Width;
                    ypos = (originalImage.Height * (float).99) - crSize.Height;
                    break;
            }

            //            System.Drawing.StringFormat StrFormat = new System.Drawing.StringFormat();
            //            StrFormat.Alignment = System.Drawing.StringAlignment.Center;
            //
            //            g.DrawString(watermarkText, drawFont, new System.Drawing.SolidBrush(System.Drawing.Color.White), xpos + 1, ypos + 1, StrFormat);
            //            g.DrawString(watermarkText, drawFont, new System.Drawing.SolidBrush(System.Drawing.Color.Black), xpos, ypos, StrFormat);
            g.DrawString(watermarkText, drawFont, new SolidBrush(Color.White), xpos + 1, ypos + 1);
            g.DrawString(watermarkText, drawFont, new SolidBrush(Color.Black), xpos, ypos);

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ici = null;
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType.IndexOf("jpeg") > -1)
                {
                    ici = codec;
                }
            }
            EncoderParameters encoderParams = new EncoderParameters();
            long[] qualityParam = new long[1];
            if (quality < 0 || quality > 100)
            {
                quality = 100;
            }
            qualityParam[0] = quality;

            EncoderParameter encoderParam = new EncoderParameter(Encoder.Quality, qualityParam);
            encoderParams.Param[0] = encoderParam;

            if (ici != null)
            {
                originalImage.Save(savePath, ici, encoderParams);
            }
            else
            {
                originalImage.Save(savePath);
            }
            originalImage.Save(savePath);
            g.Dispose();
            //bmp.Dispose();
            originalImage.Dispose();
        }


        #endregion


        #region 图片序列化

        /// <summary>
        /// 把图片进行序列化(二进制方式)
        /// </summary>
        /// <param name="originalImageFullPath">原始图片全名(完整得物理路径E:\xx.jpg)</param>
        /// <param name="targetImageFullPath">要保存的文件路径(物理路径)</param>
        /// <param name="autoDeleteOriginal">是否自动删除原文件</param>
        /// <remarks>相当于把图片加密</remarks>
        public static void SerializeImage(string originalImageFullPath, string targetImageFullPath, bool autoDeleteOriginal = false)
        {
                if (File.Exists(targetImageFullPath))
                {
                    File.Delete(targetImageFullPath);
                }
                Stream targetImageStream = new FileStream(targetImageFullPath, FileMode.Create, FileAccess.Write, FileShare.None);
                Image originalImage = Image.FromFile(originalImageFullPath);
                StreamHelper.Serialize(targetImageStream, originalImage);
                originalImage.Dispose();
                targetImageStream.Close();
                targetImageStream.Dispose();
                if (autoDeleteOriginal)
                {
                    if (File.Exists(originalImageFullPath))
                    {
                        File.Delete(originalImageFullPath);
                    }
                }
        }
        
        
        /// <summary>
        /// 把图片进行反序列化(二进制序列化)
        /// </summary>
        /// <param name="imageFullPath">(序列化的)图片全名(完整得物理路径E:\xx.jpg)</param>
        /// <remarks>相当于把图片解密</remarks>
        /// <returns>返回反序列化后的图片(Image)</returns>
        public static Image DeserializeImage(string imageFullPath)
        {
            Stream stream = new FileStream(imageFullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            Image img = (Image)StreamHelper.Deserialize(stream);
            stream.Close();
            return img;

        }

        #endregion


        #region 图片特效

        /// <summary>
        /// 以反色方式显示图像    
        /// </summary>
        /// <param name="sImage">源图像</param>
        /// <returns>反色处理后的图像</returns>
        public static Bitmap FanSe(Image sImage)
        {
            int height = sImage.Height;
            int width = sImage.Width;
            Bitmap bitmap = new Bitmap(width, height);
            Bitmap myBitmap = (Bitmap)sImage;
            for (int x = 1; x < width; x++)
            {
                for (int y = 1; y < height; y++)
                {
                    Color pixel = myBitmap.GetPixel(x, y);
                    int r = 255 - pixel.R;
                    int g = 255 - pixel.G;
                    int b = 255 - pixel.B;
                    bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            return bitmap;
        }


        /// <summary> 
        ///以浮雕方式显示图像        
        /// </summary>
        /// <param name="sImage">源图像</param>
        /// <returns>浮雕效果处理后的图像</returns>
        public static Bitmap FuDiao(Image sImage)
        {
            int height = sImage.Height;
            int width = sImage.Width;
            Bitmap bitmap = new Bitmap(width, height);
            Bitmap myBitmap = (Bitmap)sImage;
            for (int x = 0; x < width - 1; x++)
            {
                for (int y = 0; y < height - 1; y++)
                {
                    int r = 0, g = 0, b = 0;
                    Color pixel1 = myBitmap.GetPixel(x, y);
                    Color pixel2 = myBitmap.GetPixel(x + 1, y + 1);
                    r = pixel1.R - pixel2.R + 128;
                    g = pixel1.G - pixel2.G + 128;
                    b = pixel1.B - pixel2.B + 128;
                    if (r > 255)
                        r = 255;
                    if (r < 0)
                        r = 0;
                    if (g > 255)
                        g = 255;
                    if (g < 0)
                        g = 0;
                    if (b > 255)
                        b = 255;
                    if (b < 0)
                        b = 0;
                    bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            return bitmap;
        }


        /// <summary>
        /// 以黑白方式显示图像
        /// </summary>
        /// <param name="sImage">源图像</param>
        /// <param name="iType">黑白处理的方法参数,0-平均值法;1-最大值法;2-加权平均值法</param>
        /// <returns>黑白效果处理后的图像</returns>
        public static Bitmap HeiBai(Image sImage, int iType=0)
        {
            int height = sImage.Height;
            int width = sImage.Width;
            Bitmap bitmap = new Bitmap(width, height);
            Bitmap myBitmap = (Bitmap)sImage;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixel = myBitmap.GetPixel(x, y);
                    int result = 0;
                    int r = pixel.R;
                    int g = pixel.G;
                    int b = pixel.B;

                    switch (iType)
                    {
                        case 0://平均值法
                            result = ((r + g + b) / 3);
                            break;
                        case 1://最大值法
                            result = r > g ? r : g;
                            result = result > b ? result : b;
                            break;
                        case 2://加权平均值法
                            result = ((int)(0.7 * r) + (int)(0.2 * g) + (int)(0.1 * b));
                            break;
                    }
                    bitmap.SetPixel(x, y, Color.FromArgb(result, result, result));
                }
            }
            return bitmap;
        }


        /// <summary>
        /// 以柔化方式显示图像
        /// 高斯模板法
        /// </summary>
        /// <param name="sImage">源图像</param>
        /// <returns>柔化处理后的图像</returns>
        public static Bitmap RouHua(Image sImage)
        {
            int height = sImage.Height;
            int width = sImage.Width;
            Bitmap bitmap = new Bitmap(width, height);
            Bitmap myBitmap = (Bitmap)sImage;
            //高斯模板
            int[] gauss = { 1, 2, 1, 2, 4, 2, 1, 2, 1 };
            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    int r = 0, g = 0, b = 0;
                    int index = 0;
                    //int a=0;
                    for (int col = -1; col <= 1; col++)
                        for (int row = -1; row <= 1; row++)
                        {
                            Color pixel = myBitmap.GetPixel(x + row, y + col);
                            r += pixel.R * gauss[index];
                            g += pixel.G * gauss[index];
                            b += pixel.B * gauss[index];
                            index++;
                        }
                    r /= 16;
                    g /= 16;
                    b /= 16;
                    //处理颜色值溢出
                    r = r > 255 ? 255 : r;
                    r = r < 0 ? 0 : r;
                    g = g > 255 ? 255 : g;
                    g = g < 0 ? 0 : g;
                    b = b > 255 ? 255 : b;
                    b = b < 0 ? 0 : b;
                    bitmap.SetPixel(x - 1, y - 1, Color.FromArgb(r, g, b));
                }
            }
            return bitmap;
        }


        /// <summary>
        /// 以锐化方式显示图像
        /// 拉普拉斯模板法     
        /// </summary>
        /// <param name="sImage">源图像</param>
        /// <returns>锐化处理后的图像</returns>
        public static Bitmap RuiHua(Image sImage)
        {
            int height = sImage.Height;
            int width = sImage.Width;
            Bitmap bitmap = new Bitmap(width, height);
            Bitmap myBitmap = (Bitmap)sImage;
            //拉普拉斯模板
            int[] laplacian = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    int r = 0, g = 0, b = 0;
                    int index = 0;
                    //int a = 0;
                    for (int col = -1; col <= 1; col++)
                        for (int row = -1; row <= 1; row++)
                        {
                            Color pixel = myBitmap.GetPixel(x + row, y + col);
                            r += pixel.R * laplacian[index];
                            g += pixel.G * laplacian[index];
                            b += pixel.B * laplacian[index];
                            index++;
                        }
                    //处理颜色值溢出
                    r = r > 255 ? 255 : r;
                    r = r < 0 ? 0 : r;
                    g = g > 255 ? 255 : g;
                    g = g < 0 ? 0 : g;
                    b = b > 255 ? 255 : b;
                    b = b < 0 ? 0 : b;
                    bitmap.SetPixel(x - 1, y - 1, Color.FromArgb(r, g, b));
                }
            }
            return bitmap;
        }


        /// <summary>
        /// 以雾化方式显示图像
        /// </summary>
        /// <param name="sImage">源图像</param>
        /// <returns>雾化处理后的图像</returns>
        public static Bitmap WuHua(Image sImage)
        {
            int height = sImage.Height;
            int width = sImage.Width;
            Bitmap bitmap = new Bitmap(width, height);
            Bitmap myBitmap = (Bitmap)sImage;
            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    Random myRandom = new Random();
                    int k = myRandom.Next(123456);
                    //像素块大小
                    int dx = x + k % 19;
                    int dy = y + k % 19;
                    if (dx >= width)
                        dx = width - 1;
                    if (dy >= height)
                        dy = height - 1;
                    Color pixel = myBitmap.GetPixel(dx, dy);
                    bitmap.SetPixel(x, y, pixel);
                }
            }
            return bitmap;
        }


        #endregion


        #region 创建图片
        
        /// <summary>
        /// 根据源图片创建新图片
        /// </summary>
        /// <param name="OrgImage">源图片</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="format">格式</param>
        /// <param name="scale">缩放方式</param>
        /// <param name="fillColor">填充颜色</param>
        public static Image CreateNewImage(Image OrgImage, int width, int height, PixelFormat format, ImageScaleMethod scale, Color fillColor)
        {
            switch (scale)
            {
                case ImageScaleMethod.Scale:
                    return CreateNewImageScale(OrgImage, width, height, format);

                case ImageScaleMethod.Fill:
                    return CreateNewImageFill(OrgImage, width, height, format, fillColor);

                case ImageScaleMethod.Cut:
                    return CreateNewImageCut(OrgImage, width, height, format);
            }
            return null;
        }


        /// <summary>
        /// 根据源图片裁切为新图片
        /// </summary>
        /// <param name="OrgImage">源图片</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="format">格式</param>
        public static Image CreateNewImageCut(Image OrgImage, int width, int height, PixelFormat format)
        {
            float num = width / ((float)OrgImage.Width);
            if ((OrgImage.Height * num) < height)
            {
                num = height / (float)OrgImage.Height;
            }
            float num4 = OrgImage.Width * num;
            float num5 = OrgImage.Height * num;
            float x = (width - num4) / 2f;
            float y = (height - num5) / 2f;
            Bitmap image = new Bitmap(width, height, format);
            Graphics graphics = Graphics.FromImage(image);
            graphics.CompositingMode = CompositingMode.SourceOver;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.DrawImage(OrgImage, x, y, num4, num5);
            graphics.Save();
            graphics.Dispose();
            return image;
        }


        /// <summary>
        /// 根据源图片填充为新图片
        /// </summary>
        /// <param name="OrgImage">源图片</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="format">格式</param>
        /// <param name="fillColor">填充色</param>
        public static Image CreateNewImageFill(Image OrgImage, int width, int height, PixelFormat format, Color fillColor)
        {
            float num = width / ((float)OrgImage.Width);
            if ((OrgImage.Height * num) > height)
            {
                num = height / ((float)OrgImage.Height);
            }
            float num4 = OrgImage.Width * num;
            float num5 = OrgImage.Height * num;
            float x = (width - num4) / 2f;
            float y = (height - num5) / 2f;
            Bitmap image = new Bitmap(width, height, format);
            Graphics graphics = Graphics.FromImage(image);
            graphics.CompositingMode = CompositingMode.SourceOver;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.Clear(fillColor);
            graphics.DrawImage(OrgImage, x, y, num4, num5);
            graphics.Save();
            graphics.Dispose();
            return image;
        }


        /// <summary>
        /// 根据源图片比例缩放为新图片
        /// </summary>
        /// <param name="OrgImage">源图片</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="format">格式</param>
        public static Image CreateNewImageScale(Image OrgImage, int width, int height, PixelFormat format)
        {
            Bitmap image = new Bitmap(width, height, format);
            Graphics graphics = Graphics.FromImage(image);
            graphics.CompositingMode = CompositingMode.SourceOver;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.DrawImage(OrgImage, 0, 0, width, height);
            graphics.Save();
            graphics.Dispose();
            return image;
        }


        #endregion

       
        /// <summary>
        /// 从磁盘加载图片到内存 自动释放文件
        /// </summary>
        /// <param name="path">图片路径</param>
        public static Image LoadImage(string path)
        {
            if (File.Exists(path))
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(path);
                System.Drawing.Image bmp = new System.Drawing.Bitmap(img);
                img.Dispose();
                return bmp;
            }
            return null;
        }


        /// <summary>
        /// 把图片保存为Jpeg格式
        /// </summary>
        /// <param name="image">源图片</param>
        /// <param name="path">保存路径</param>
        /// <param name="quality">品质</param>
        public static void SaveJpegQualityCodecsInfo(Image image, string path, int quality)
        {
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, quality);
            foreach (ImageCodecInfo info in ImageCodecInfo.GetImageEncoders())
            {
                if (info.MimeType == "image/jpeg")
                {
                    image.Save(path, info, encoderParams);
                    return;
                }
            }
        }


        /// <summary>
        /// 分割图片
        /// </summary>
        /// <param name="path">源图片路径</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public static Image[] SplitImage(string path, int width, int height)
        {
            return SplitImage(LoadImage(path), width, height);
        }


        /// <summary>
        /// 分割图片
        /// </summary>
        /// <param name="img">源图片</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public static Image[] SplitImage(Image img, int width, int height)
        {
            if (img == null)
            {
                throw new ArgumentNullException("img","源图片能为空");
            }
            if (img.Width < width)
            {
                throw new ArgumentOutOfRangeException("width","指定的宽度大于源图片宽度");
            }
            if (img.Height < height)
            {
                throw new ArgumentOutOfRangeException("height", "指定的高度大于源图片高度");
            }
            List<Image> images = new List<Image>();
            int num1 = 0;
            int num2 = 0;
            while (num2 < img.Height)
            {
                if ((height + num2) > img.Height && (num1 + width) <= img.Width)
                {
                    Image bitmap = new Bitmap(width, height);
                    Graphics graphic = Graphics.FromImage(bitmap);
                    graphic.DrawImage(img, 0, 0, new RectangleF(num1, num2, width, height), GraphicsUnit.Pixel);
                    graphic.Flush();
                    graphic.Dispose();
                    images.Add(bitmap);
                    num1 = num1 + width;
                }
                else
                {
                    num2 = num2 + height;
                }
            }
            return images.ToArray();
        }


        #region 图片 字节数组 转换

        /// <summary>
        /// 把图片转为字节数组
        /// </summary>
        /// <param name="img">图片</param>
        public static byte[] ToArray(Image img)
        {
            if (img == null)
            {
                return new byte[0];
            }
            lock (img)
            {
                return ToArray(img, img.RawFormat);
            }
        }


        /// <summary>
        /// 把图片转为字节数组
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="format">格式</param>
        public static byte[] ToArray(Image img, ImageFormat format)
        {
            return ToArrayCore(img, format);
        }


        /// <summary>
        /// 把图片转为字节数组(实现)
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="format">格式</param>
        private static byte[] ToArrayCore(Image img, ImageFormat format)
        {
            MemoryStream stream = new MemoryStream();
            try
            {
                SaveImage(img, stream, format);
                return stream.ToArray();
            }
            catch
            {
                return new byte[] { };
            }
            finally
            {
                stream.Close();
                ((IDisposable)stream).Dispose();
            }
        }


        /// <summary>
        /// 把图像保存到指定的流中
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="stream">流</param>
        /// <param name="format">格式</param>
        public static void SaveImage(Image img, Stream stream, ImageFormat format)
        {
            ImageCodecInfo info = FindEncoder(format);
            if (info == null)
                info = FindEncoder(ImageFormat.Png);
            lock (img)
            {
                img.Save(stream, info, null);
            }
        }


        /// <summary>
        /// 把字节数组转为图片
        /// </summary>
        /// <param name="buffer">字节数字</param>
        public static Image FromArray(byte[] buffer)
        {
            if (buffer == null)
                return null;
            Image img = null;

            if (buffer.Length > 78)
            {
                if (buffer[0] == 0x15 && buffer[1] == 0x1c)
                    img = FromArrayCore(buffer, 78);
            }
            if (img == null)
            {
                img = FromArrayCore(buffer, 0);
            }
            return img;
        }


        /// <summary>
        /// 把字节数组转为图片
        /// </summary>
        /// <param name="buffer">字节数字</param>
        /// <param name="offset">开始位置</param>
        private static Image FromArrayCore(byte[] buffer, int offset)
        {
            if (buffer == null)
            {
                return null;
            }
            try
            {
                MemoryStream stream = new MemoryStream(buffer, offset, buffer.Length - offset);
                return Image.FromStream(stream);
            }
            catch { return null; }
        }

        
 

        /// <summary>
        /// 返回编码解码器
        /// </summary>
        /// <param name="format">图片格式</param>
        static ImageCodecInfo FindEncoder(ImageFormat format)
        {
            ImageCodecInfo[] infos = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo t in infos)
            {
                if (t.FormatID.Equals(format.Guid))
                {
                    return t;
                }
            }
            return null;
        }

         
        #endregion

    }


    /// <summary>
    /// 图片缩放方式
    /// </summary>
    public enum ImageScaleMethod
    {
        /// <summary>
        /// 等比例缩放
        /// </summary>
        Scale,


        /// <summary>
        /// 背景色填充
        /// </summary>
        Fill,


        /// <summary>
        /// 裁切
        /// </summary>
        Cut
    }

}
