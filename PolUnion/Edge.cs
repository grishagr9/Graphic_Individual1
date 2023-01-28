using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PolUnion
{
    
    public class Edge
    {
        public bool Red { get; set; }
        public Point Begin { get; set; }
        public Point End { get; set; }
        public Point Mid => new Point((Begin.X + End.X) / 2, (Begin.Y + End.Y) / 2);
        public Point Vector => new Point(End.X - Begin.X, End.Y - Begin.Y);
        public Point Normal
        {
            get
            {
                var vector = Vector;
                var normal = new Point(-vector.Y, vector.X);
                return normal;
            }
        }

        public Edge(Point begin, Point end)
        {
            Begin = begin;
            End = end;
        }

        public void Rotate()
        {
            var mid = Mid;
            var normal = Normal;
            Begin = new Point(mid.X + normal.X / 2, mid.Y + normal.Y / 2);
            End = new Point(mid.X - normal.X / 2, mid.Y - normal.Y / 2);
        }

        public static bool AreIntersect(Point a, Point b, Point c, Point d)
        {
            try
            {
                var _intersectionPoint = Intersect(a, b, c, d);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Point Intersect(Edge other)
        {
            var a = Begin;
            var b = End;

            var c = other.Begin;
            var d = other.End;

            return Intersect(a, b, c, d);
        }

        public static Point Intersect(Point a, Point b, Point c, Point d)
        {
            var n = new Point(d.Y - c.Y, c.X - d.X);

            int denominator = ScalarProduct(n, new Edge(b, a).Vector);
            if (denominator == 0)
            {
                throw new EdgesDontIntersectException();
            }
            int numerator = -ScalarProduct(n, new Edge(a, c).Vector);

            double t = (double)numerator / denominator;

            var p = ParametrizePoint(a, b, t);

            if (!IsPointOnEdge(new Edge(a, b), p) || !IsPointOnEdge(new Edge(c, d), p))
            {
                throw new EdgesDontIntersectException();
            }

            return p;
        }

        private static bool IsPointOnEdge(Edge edge, Point p)
        {
            return Math.Min(edge.Begin.X, edge.End.X) <= p.X && p.X <= Math.Max(
                edge.Begin.X, edge.End.X) && Math.Min(edge.Begin.Y, edge.End.Y) <= p.Y && p.Y <= Math.Max(edge.Begin.Y, edge.End.Y);
        }

        private static Point ParametrizePoint(Point begin, Point end, double t)
        {
            return new Point(begin.X + (int)(t * (end.X - begin.X)), begin.Y + (int)(t * (end.Y - begin.Y)));
        }

        private static int ScalarProduct(Point vec1, Point vec2)
        {
            return vec1.X * vec2.X + vec1.Y * vec2.Y;
        }
    }
}
