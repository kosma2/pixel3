using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace pixelSort
{
    public class Program
    {
        public static int reduceValue(int val)
        {
            if(val <= 63)
            {return 0;}
            if(val > 63 || val < 127)
            {return 127;}
            if(val >= 127 || val < 190)
            {return 127;}
            if(val >= 190 || val <= 255)
            {return 255;}
            else{
                return 0;
            }
        }
        public static int reduceValPro(int val)
        {
            var interval = 255 / 4;
            if(val <= interval)
            {return 0;}
            if(val > interval && val < interval*2)
            {return interval;}
            if(val >= interval*2 && val < interval*3)
            {return interval*2;}
            if(val >= interval*3 && val <= 255)
            {return 255;}
            else{
                return 0;
            }
        }
        public static Color reduceColor(Color col)
        {
            Color newCol =  Color.FromArgb(reduceValPro(col.R),reduceValPro(col.G),reduceValPro(col.B));
            return newCol;
        }

        public class SortColorsByRed : IComparer<Color>
        {
            public int Compare(Color x, Color y)
            {
                if (x == null || y == null)
                {
                    return 0;
                }
                return x.R.CompareTo(y.R);
            }
        }
        public static void Main()
        {
            int spacing = 1;

            try
            {
                // Retrieve the image.
                using Bitmap image = new Bitmap("images/face.jpg", true);
                using Bitmap newImage = new Bitmap(image, 50, 50);
                using Bitmap piktImage = new Bitmap(50 * spacing, 50 * spacing); //PixelFormat.Format16bppGrayScale);
                List<Color> pixList = new List<Color>();
                List<Color> reducPixList = new List<Color>();
                int pixCount = 0;
                int x, y;

                // Loop through the images pixels to reset color.
                for (x = 0; x < newImage.Width; x++)
                {
                    for (y = 0; y < newImage.Height; y++)
                    {
                        Color pixelColor = newImage.GetPixel(x, y);
                        pixList.Add(pixelColor);
                    }
                }
                // reduce colors
                for(int i=0;i<pixList.Count;i++)
                {
                    Color col = reduceColor(pixList[i]);
                    reducPixList.Add(col);
                }
                //SortColorsByRed sortByRed = new SortColorsByRed();
                //pixList.Sort(sortByRed);
                for (x = 0; x < newImage.Width; x++)
                {
                    for (y = 0; y < newImage.Height; y++)
                    {
                        piktImage.SetPixel(x * spacing, y * spacing, reducPixList[pixCount]);
                        pixCount++;
                    }

                }
                newImage.Save("images/face1.jpg");
                piktImage.Save("images/faceReduc.jpg");
            }
            catch (Exception e) { System.Console.WriteLine(e.Message); }
        }
    }
}