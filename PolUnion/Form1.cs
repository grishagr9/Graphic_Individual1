
 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolUnion
{
    using FastBitmap;
    using PolUnion;
    public partial class Form1 : Form
    {
        
        private PrimitiveType[] primitiveTypes;

        private List<Point> points;
        private List<Edge> edges;
        private List<Polygon> polygons;
        private Polygon P;
        private List<Point> currentPoints;
        private Edge currentlySelectedEdge;
        private static readonly Color DrawingColor = Color.Blue;
        private static readonly Color DrawingColorRed = Color.Red;
        public Form1()
        {
            InitializeComponent();
            SetPrimitiveTypes();
            ResetDrawingSurface();

            doneButton.Enabled = false;

            points = new List<Point>();
            edges = new List<Edge>();
            polygons = new List<Polygon>();

            currentPoints = new List<Point>();
        }

        private void SetPrimitiveTypes()
        {
            primitiveTypes = Enum.GetValues(typeof(PrimitiveType)).Cast<PrimitiveType>().ToArray();
            var primitiveTypeNames = primitiveTypes.Select(pt => pt.GetPrimitiveName()).ToArray();
        }

        private void ResetDrawingSurface()
        {
           
            var size = scenePictureBox.Size;
            var scene = new Bitmap(size.Width, size.Height);
            scenePictureBox.Image = scene;
            
            P = null;
            currentlySelectedEdge = null;
            if (points!=null)
                points.Clear();
            if (edges != null)
                edges.Clear();
            if (polygons != null)
                polygons.Clear();

        }

        private void scenePictureBox_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Cross;
        }

        private void scenePictureBox_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void clearSceneButton_Click(object sender, EventArgs e)
        {
            ResetDrawingSurface();
        }

        private void scenePictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            var p = new Point(e.X, e.Y);
            DrawPoint(p.X, p.Y, DrawingColor);   
            currentPoints.Add(p);
            doneButton.Enabled = currentPoints.Count > 3;
        }

        private void DrawPoint(int x, int y, Color color)
        {
            var drawingSurface = scenePictureBox.Image as Bitmap;
            using (var fastDrawingSurface = new FastBitmap(drawingSurface))
            {
                fastDrawingSurface[x, y] = color;
            }
            scenePictureBox.Image = drawingSurface;
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            var polygonEdges = new List<Edge>();
            var drawingSurface = scenePictureBox.Image as Bitmap;
            using (var fastDrawingSurface = new FastBitmap(drawingSurface))
            {
                for (int i = 0; i < currentPoints.Count; i++)
                {
                    var edge = new Edge(currentPoints[i], currentPoints[(i + 1) % currentPoints.Count]);
                    polygonEdges.Add(edge);
                    fastDrawingSurface.DrawBresenhamLine(edge.Begin, edge.End, DrawingColor);
                }
            }
            scenePictureBox.Image = drawingSurface;
            polygons.Add(new Polygon(polygonEdges));

            currentPoints.Clear();


            doneButton.Enabled = false;
        }


        private List<Point> ForEdges(Polygon p1, Polygon p2)
        {
            var points = new List<Point>(); points.Add(p1.Edges[0].Begin); //массив точек нового полигона
            var endPoint = p1.Edges[0].Begin; //точка останова
            var edges = new List<List<Edge>>(); //два полигона из ребер
                edges.Add(p1.Edges);
                edges.Add(p2.Edges);
            int ind = 0; //индекс текущего полигона
            int ind1 = 0; //индекс ребра первого полигона
            int ind2 = 0; //индекс ребра второго полигона
            var inds = new List<int>(); inds.Add(ind1); inds.Add(ind2); 
            var curEdge = edges[ind][inds[ind]]; //текущее ребро
            int cur = 1; //индекс проверяемого полигона
            int mod = p1.Edges.Count;
            int count = 0;
            while (true) {
                int flag = cur;
               
                for (int i = 0; i<edges[cur].Count; i++)
                {
                    if (Edge.AreIntersect(curEdge.Begin,curEdge.End,edges[cur][i].Begin, edges[cur][i].End)) //если текущее ребро пересекается с ребром i
                    {
                        count++;
                        Point intersect = curEdge.Intersect(edges[cur][i]);
                        points.Add(curEdge.Begin);
                        points.Add(intersect);
                        points.Add(edges[cur][i].End);
                        Console.WriteLine(curEdge.Begin.ToString() + " " + curEdge.End.ToString()
                            + " " + edges[cur][i].Begin.ToString() + " " + edges[cur][i].End.ToString() + " -> " + intersect.X.ToString() + " " + intersect.Y.ToString());
                        if (edges[cur][i].End == endPoint)
                            flag = -1;
                        ind = ind == 1 ? 0 : 1;
                        cur = cur == 1 ? 0 : 1;
                        inds[ind] = i;
                        break;
                    }
                }
                var t = inds[ind];
                if (curEdge.End == endPoint || flag == -1 || inds[ind]>=edges[ind].Count)
                    break;
                if (cur == flag)
                {
                    points.Add(curEdge.End);
                    // t = ++inds[ind];
                   
                }
                try
                {
                    curEdge = edges[ind][(++inds[ind])%mod];
                }
                catch (Exception)
                {
                    
                }
            }
            if (count == 0)
                points = null;
            return points;
        }

        private List<Point> TwoPoli(Polygon p1, Polygon p2)
        {
            List<Point> points = new List<Point>();
            foreach (Edge p in p1.Edges)
            {
                points.Add(p.Begin);
            }
            foreach (Edge p in p2.Edges)
            {
                points.Add(p.Begin);
            }
            return points;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentPoints = ForEdges(polygons[0], polygons[1]);
            if (currentPoints == null) {
                if (buttonPrinadlegit_Click(polygons[0], polygons[1].Edges[0].Begin))
                    currentPoints = PointPoli(polygons[0]);
                else
                    DiffPol(polygons[0], polygons[1]);

            }
                
            var polygonEdges = new List<Edge>();
            var drawingSurface = scenePictureBox.Image as Bitmap;
            using (var fastDrawingSurface = new FastBitmap(drawingSurface))
            {
                for (int i = 0; i < currentPoints.Count; i++)
                {
                    var edge = new Edge(currentPoints[i], currentPoints[(i + 1) % currentPoints.Count]);
                    polygonEdges.Add(edge);
                    fastDrawingSurface.DrawBresenhamLine(edge.Begin, edge.End, DrawingColorRed);
                }
            }
            scenePictureBox.Image = drawingSurface;
            polygons.Add(new Polygon(polygonEdges));

            currentPoints.Clear();

            doneButton.Enabled = false;
        }

        private List<Point> PointPoli(Polygon polygon)
        {
            List<Point> points = new List<Point>();
            foreach (Edge p in polygon.Edges)
            {
                points.Add(p.Begin);
            }
            return points;
        }

        private bool buttonPrinadlegit_Click(Polygon p12, Point ppp)
        {
            int number;
            var point = ppp;
            var point0 = new Point(0, point.Y);
            int intersections = 0;
            P = p12;
            

            List<Point> primitiv = ToPoint(P);
            Point p1 = primitiv.First();
            int num = 0;

            foreach (var p2 in primitiv)
            {
                if (p1 == p2)
                {
                    continue;
                }

                if (point.X <= p1.X && point.Y == p1.Y && num == 0)
                {
                    num++;
                    continue;
                }
                else
                {
                    num = 0;
                }

                if (Edge.AreIntersect(point, point0, p1, p2))
                {
                    intersections++;
                }

                p1 = p2;
            }

            if (!(point.X <= p1.X && point.Y == p1.Y && num == 0))
            {

                if (Edge.AreIntersect(point, point0, p1, primitiv.First()))
                {
                    intersections++;
                }
            }

            return intersections % 2 == 0 ? false : true;
        }
        private List<Point> ToPoint(Polygon polygon)
        {
            List<Point> result = new List<Point>();
            result.Add(polygon.Edges[0].Begin);
            foreach (Edge e in polygon.Edges)
            {
                result.Add(e.End);
            }
            return result;
        }

        private void DiffPol(Polygon p1, Polygon p2)
        {
            currentPoints = ToPoint(p1);
           var polygonEdges = new List<Edge>();
            var drawingSurface = scenePictureBox.Image as Bitmap;
            using (var fastDrawingSurface = new FastBitmap(drawingSurface))
            {
                for (int i = 0; i < currentPoints.Count; i++)
                {
                    var edge = new Edge(currentPoints[i], currentPoints[(i + 1) % currentPoints.Count]);
                    polygonEdges.Add(edge);
                    fastDrawingSurface.DrawBresenhamLine(edge.Begin, edge.End, DrawingColorRed);
                }
            }
            scenePictureBox.Image = drawingSurface;
            polygons.Add(new Polygon(polygonEdges));

            currentPoints.Clear();

            doneButton.Enabled = false;

            currentPoints = ToPoint(p2);
            polygonEdges = new List<Edge>();
            drawingSurface = scenePictureBox.Image as Bitmap;
            using (var fastDrawingSurface = new FastBitmap(drawingSurface))
            {
                for (int i = 0; i < currentPoints.Count; i++)
                {
                    var edge = new Edge(currentPoints[i], currentPoints[(i + 1) % currentPoints.Count]);
                    polygonEdges.Add(edge);
                    fastDrawingSurface.DrawBresenhamLine(edge.Begin, edge.End, DrawingColorRed);
                }
            }
            scenePictureBox.Image = drawingSurface;
            polygons.Add(new Polygon(polygonEdges));

            currentPoints.Clear();

            doneButton.Enabled = false;
        }
    }
}