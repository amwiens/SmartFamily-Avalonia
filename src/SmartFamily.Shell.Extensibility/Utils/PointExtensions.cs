using Avalonia;

namespace SmartFamily.Shell.Extensibility.Utils
{
    public static class PointExtensions
    {
        public static double DistanceTo(this Point p, Point q)
        {
            var a = p.X - q.X;
            var b = p.Y - q.Y;
            var distance = Math.Sqrt((a * a) + (b * b));
            return distance;
        }
    }
}