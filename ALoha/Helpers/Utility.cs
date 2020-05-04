using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Aloha.Helpers {
    public static class Utility {
        public static ImageSource ToImageSource(this Bitmap bitmap) {
            if (bitmap == null)
                throw new ArgumentNullException("Bitmap is null");

            IntPtr hBitmap = bitmap.GetHbitmap();
            IntPtr zero = IntPtr.Zero;
            Int32Rect empty = Int32Rect.Empty;
            BitmapSizeOptions options = BitmapSizeOptions.FromEmptyOptions();

            return Imaging.CreateBitmapSourceFromHBitmap(hBitmap, zero, empty, options);
        }
    }
}
