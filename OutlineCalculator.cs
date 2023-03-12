using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO: generates far too few points! 1 point per triangle?

static partial class OutlineCalculator
{
	public static List<Shape> GenerateOutlines(Vector3[] vertices, int[] indices)
	{
		var shapes = new List<Shape>();

		// TODO: algorithm
		// find coplanar triangles, group them
		// remove any duplicate edges (both)
		// line up the vertices by daisy-chaining them
		// split groups when no connections are found
		// every time you make a connection, remove from the list to check against

		// potential problem: two coplanar shapes which are only connected in one point

		var triangles = new List<Triangle>();
		for (int i = 0; i < indices.Length; i += 3)
		{
			//GD.Print($"index: {i}, vertices count: {vertices.Length}");

			var i1 = indices[i];
			var i2 = indices[i + 1];
			var i3 = indices[i + 2];

			var v1 = vertices[i1];
			var v2 = vertices[i2];
			var v3 = vertices[i3];

			var t = new Triangle(v1, v2, v3, i1, i2, i3);

			triangles.Add(t);
		}
		GD.Print($"Generated {triangles.Count} triangles");
		/*
		var triangleGroups = new List<List<Triangle>>();

		var uncheckedTriangles = new List<Triangle>();
		uncheckedTriangles.AddRange(triangles);

		int checkLimit = 10000;

		while (uncheckedTriangles.Count > 0 && checkLimit > 0)
		{
			var triangle =

			checkLimit--;
		}
		*/

		// TODO: keep track as a dict? could use the first added triangle as key. if it is coplanar with one, it would be with all
		// can't just use the normal, also need a point

		var coplanarTriangles = new Dictionary<Triangle, List<Triangle>>();

		for (int t = 0; t < triangles.Count; t++)
		{
			var tri = triangles[t];

			foreach (var key in coplanarTriangles.Keys)
			{
				if (tri.IsCoplanarWith(key))
				{
					coplanarTriangles[key].Add(tri);
					tri = null;
					break;
				}
			}

			if (tri == null) { continue; }

			coplanarTriangles[tri] = new List<Triangle> { tri };
		}

		// TODO: the results seem off...
		GD.Print($"Total coplanar triangle groups: {coplanarTriangles.Keys.Count}");
		var pointCount = 0;

		foreach (var key in coplanarTriangles.Keys)
		{
			var tris = coplanarTriangles[key];
			GD.Print($"Group of {tris.Count}");

			// TODO: grab all edges, remove duplicate ones
			var edges = new List<Edge>();
			foreach (var tri in tris)
			{
				edges.Add(new Edge(tri.v1, tri.v2));
				edges.Add(new Edge(tri.v2, tri.v3));
				edges.Add(new Edge(tri.v3, tri.v1));
			}

			var uniqueEdges = new List<Edge>();
			uniqueEdges.AddRange(edges);

			for (int e = 0; e < edges.Count - 1; e++)
			{
				for (int f = e + 1; f < edges.Count; f++)
				{
					if (edges[e].SameAs(edges[f]))
					{
						uniqueEdges.Remove(edges[e]);
					}
				}
			}
			GD.Print($"Unique edges left: {uniqueEdges.Count}");

			var points = new List<Vector3>();

			for (int u = 0; u < uniqueEdges.Count; u++)
			{
				points.Add(uniqueEdges[u].a);
				points.Add(uniqueEdges[u].b);
			}
			/*
			foreach (var t in tris)
			{
				points.Add(t.v1);
				points.Add(t.v2);
				points.Add(t.v3);
			}
			*/
			var shape = new Shape(points);
			shapes.Add(shape);

			pointCount += shape.points.Count;
		}

		GD.Print($"Points left: {pointCount}");
		// TODO: create shapes from the grouped triangles

		return shapes;
	}
}

