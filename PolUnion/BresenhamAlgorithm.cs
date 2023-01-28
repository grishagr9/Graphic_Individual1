using System;
using System.Drawing;

namespace PolUnion
{
    using FastBitmap;

    public static class BresenhamAlgorithm
    {
        public static void DrawBresenhamLine(this Bitmap drawingSurface, Point p1, Point p2, Color color)
        {
            using (var fastDrawingSurface = new FastBitmap(drawingSurface))
                fastDrawingSurface.DrawBresenhamLine(p1, p2, color);
        }

        public static void DrawBresenhamLine(this FastBitmap fastDrawingSurface, Point p1, Point p2, Color color)
        {
            fastDrawingSurface.DrawBresenhamLine(p1.X, p1.Y, p2.X, p2.Y, color);
        }

        public static void DrawBresenhamLine(this FastBitmap fastDrawingSurface, int x1, int y1, int x2, int y2, Color color)
        {
            int diffX = x2 - x1;
            int diffY = y2 - y1;

            int growthDirectionX = diffX > 0 ? 1 : -1;
            int growthDirectionY = diffY > 0 ? 1 : -1;

            int deltaX = Math.Abs(diffX);
            int deltaY = Math.Abs(diffY);

            // Определяем gradient <= 1 или gradient > 1
            int axisGrowthDirection = deltaX > deltaY ? 1 : -1;

            int decision = 2 * axisGrowthDirection * (deltaY - deltaX);

            int refereeY = Math.Max(axisGrowthDirection, 0);
            int refereeX = Math.Min(axisGrowthDirection, 0);

            while (x1 != x2 || y1 != y2)
            {
                fastDrawingSurface.SetPixelSafely(x1, y1, color);
                x1 += growthDirectionX * refereeY;  // gradient <= 1
                y1 -= growthDirectionY * refereeX;  // gradient > 1

                if (decision < 0)
                {
                    decision += 2 * (deltaY * refereeY - deltaX * refereeX);
                }
                else
                {
                    y1 += growthDirectionY * refereeY;  // gradient <= 1
                    x1 -= growthDirectionX * refereeX;  // gradient > 1
                    decision += 2 * axisGrowthDirection * (deltaY - deltaX);
                }
            }
        }

        private static void SetPixelSafely(this FastBitmap fastDrawingSurface, int x, int y, Color color)
        {
            bool xIsInBounds = 0 <= x && x <= fastDrawingSurface.Width;
            bool yIsInBounds = 0 <= y && y <= fastDrawingSurface.Height;
            if (xIsInBounds && yIsInBounds)
            {
                fastDrawingSurface[x, y] = color;
            }
        }

    }
}
