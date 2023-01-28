using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PolUnion
{
    public class Polygon
    {
        public List<Edge> Edges { get; private set; }

        public Polygon(List<Edge> edges)
        {
            Edges = edges;
        }

        public void ToRed() {
            foreach (var item in Edges)
                item.Red = true;
        }
        public void ToUsual()
        {
            foreach (var item in Edges)
                item.Red = false;
        }
       
    }
}
