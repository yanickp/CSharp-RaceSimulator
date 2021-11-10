using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace WpfApp
{
    class imageCache
    {
        private static readonly Dictionary<string, Bitmap> _images = new Dictionary<string, Bitmap>();

        public static Bitmap Get(string key)
        {
            if (!_images.ContainsKey(key))
                _images.Add(key, new Bitmap(key));

            return (Bitmap) _images[key].Clone();
        }

        public static Bitmap GetEmptyBitmap(int x, int y)
        {
            if (!_images.ContainsKey("empty"))
                _images.Add("empty", new Bitmap(x, y));

            return (Bitmap) _images["empty"].Clone();
        }

        public static void Clear()
        {
            _images.Clear();
        }

        public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }
    }
}