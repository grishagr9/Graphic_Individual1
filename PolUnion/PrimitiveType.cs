using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolUnion
{
    public enum PrimitiveType
    {
        Point, Edge, Polygon
    }

    public static class PrimitiveTypeExtensionMethods
    {
        public static string GetPrimitiveName(this PrimitiveType primitiveType)
        {
            string primitiveName = null;
            switch (primitiveType)
            {
                case PrimitiveType.Point:
                    primitiveName = "Точка";
                    break;
                case PrimitiveType.Edge:
                    primitiveName = "Ребро";
                    break;
                case PrimitiveType.Polygon:
                    primitiveName = "Полигон";
                    break;
            }
            return primitiveName;
        }
    }
}
