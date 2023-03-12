using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

public partial class OutlineDraw : Control
{
    private MeshInstance3D meshInstance;
    [Export] private NodePath meshPath;

    private List<Vector3> points = new();
    private List<List<Vector3>> triangleLines = new();
    public List<Shape> shapes = new();

    private Color[] colors = new Color[] {
        Colors.AliceBlue,
        Colors.AntiqueWhite,
        Colors.Aqua,
        Colors.Aquamarine,
        Colors.Azure,
        Colors.Beige,
        Colors.Bisque,
        Colors.Black,
        Colors.BlanchedAlmond,
        Colors.Blue,
        Colors.BlueViolet,
        Colors.Brown,
        Colors.Burlywood,
        Colors.CadetBlue,
        Colors.Chartreuse,
        Colors.Chocolate,
        Colors.Coral,
        Colors.CornflowerBlue,
        Colors.Cornsilk,
        Colors.Crimson,
        Colors.Cyan,
        Colors.DarkBlue,
        Colors.DarkCyan,
        Colors.DarkGoldenrod,
        Colors.DarkGray,
        Colors.DarkGreen,
        Colors.DarkKhaki,
        Colors.DarkMagenta,
        Colors.DarkOliveGreen,
        Colors.DarkOrange,
        Colors.DarkOrchid,
        Colors.DarkRed,
        Colors.DarkSalmon,
        Colors.DarkSeaGreen,
        Colors.DarkSlateBlue,
        Colors.DarkSlateGray,
        Colors.DarkTurquoise,
        Colors.DarkViolet,
        Colors.DeepPink,
        Colors.DeepSkyBlue,
        Colors.DimGray,
        Colors.DodgerBlue,
        Colors.Firebrick,
        Colors.FloralWhite,
        Colors.ForestGreen,
        Colors.Fuchsia,
        Colors.Gainsboro,
        Colors.GhostWhite,
        Colors.Gold,
        Colors.Goldenrod,
        Colors.Gray,
        Colors.Green,
        Colors.GreenYellow,
        Colors.Honeydew,
        Colors.HotPink,
        Colors.IndianRed,
        Colors.Indigo,
        Colors.Ivory,
        Colors.Khaki,
        Colors.Lavender,
        Colors.LavenderBlush,
        Colors.LawnGreen,
        Colors.LemonChiffon,
        Colors.LightBlue,
        Colors.LightCoral,
        Colors.LightCyan,
        Colors.LightGoldenrod,
        Colors.LightGray,
        Colors.LightGreen,
        Colors.LightPink,
        Colors.LightSalmon,
        Colors.LightSeaGreen,
        Colors.LightSkyBlue,
        Colors.LightSlateGray,
        Colors.LightSteelBlue,
        Colors.LightYellow,
        Colors.Lime,
        Colors.LimeGreen,
        Colors.Linen,
        Colors.Magenta,
        Colors.Maroon,
        Colors.MediumAquamarine,
        Colors.MediumBlue,
        Colors.MediumOrchid,
        Colors.MediumPurple,
        Colors.MediumSeaGreen,
        Colors.MediumSlateBlue,
        Colors.MediumSpringGreen,
        Colors.MediumTurquoise,
        Colors.MediumVioletRed,
        Colors.MidnightBlue,
        Colors.MintCream,
        Colors.MistyRose,
        Colors.Moccasin,
        Colors.NavajoWhite,
        Colors.NavyBlue,
        Colors.OldLace,
        Colors.Olive,
        Colors.OliveDrab,
        Colors.Orange,
        Colors.OrangeRed,
        Colors.Orchid,
        Colors.PaleGoldenrod,
        Colors.PaleGreen,
        Colors.PaleTurquoise,
        Colors.PaleVioletRed,
        Colors.PapayaWhip,
        Colors.PeachPuff,
        Colors.Peru,
        Colors.Pink,
        Colors.Plum,
        Colors.PowderBlue,
        Colors.Purple,
        Colors.RebeccaPurple,
        Colors.Red,
        Colors.RosyBrown,
        Colors.RoyalBlue,
        Colors.SaddleBrown,
        Colors.Salmon,
        Colors.SandyBrown,
        Colors.SeaGreen,
        Colors.Seashell,
        Colors.Sienna,
        Colors.Silver,
        Colors.SkyBlue,
        Colors.SlateBlue,
        Colors.SlateGray,
        Colors.Snow,
        Colors.SpringGreen,
        Colors.SteelBlue,
        Colors.Tan,
        Colors.Teal,
        Colors.Thistle,
        Colors.Tomato,
        Colors.Transparent,
        Colors.Turquoise,
        Colors.Violet,
        Colors.WebGray,
        Colors.WebGreen,
        Colors.WebMaroon,
        Colors.WebPurple,
        Colors.Wheat,
        Colors.White,
        Colors.WhiteSmoke,
        Colors.Yellow,
        Colors.YellowGreen,
        };

	public override void _Ready()
    {
        meshInstance = GetNode<MeshInstance3D>(meshPath);

        var mesh = (ArrayMesh)meshInstance.Mesh;

        var arrays = mesh.SurfaceGetArrays(2);

        var vertexArray = arrays[0].AsVector3Array();
        var indexArray = arrays[12].AsInt32Array();

        /*
        var transform = meshInstance.Transform;

        for (int v = 00; v < vertexArray.Length; v++)
        {
            var vertex = vertexArray[v];

            vertexArray[v] = transform * vertex;
        }
        */

        //GeneratePoints(vertexArray, indexArray);

        // TODO: use result
        shapes = OutlineCalculator.GenerateOutlines(vertexArray, indexArray);

        var rand = new Random();
        foreach (var shape in shapes) 
        {
            var r = rand.Next(0, colors.Length);
            shape.color = colors[r];
        }
        /*
        foreach (var shape in shapes)
        {
            triangleLines.Add(shape.points);
        }
        */

        return;

        // TODO: are the arrays always at the same indices? or depending on the format? the exporter?
        for (int a = 0; a < arrays.Count; a++)
        {
            var array = arrays[a];

            if (array.VariantType == Variant.Type.PackedVector3Array && array.AsVector3Array() is Vector3[] vectorArray)
            {
                GD.Print($"Vector3 array, count: {vectorArray.Length}");

            } 
            else if (array.VariantType == Variant.Type.PackedInt32Array && array.AsInt32Array() is int[] intArray)
            {
                GD.Print($"int array, count: {intArray.Length}");
                /*
                for (int i = 0; i < intArray.Length; i++)
                {
                    GD.Print($"#{i}: {intArray[i]}");
                }
                */
            } 
            GD.Print($"array #{a}: {array.VariantType}");

        }

        /*
        var camera = GetViewport().GetCamera3D();

        var mdt = new MeshDataTool();
        mdt.CreateFromSurface(mesh, 0);

        var transform = meshInstance.Transform;
        //Vector3 v = Vector3.Zero.

        // TODO: how to get indices?
        for (int f = 0; f < mdt.GetFaceCount(); f++)
        {
            for (int e = 0; e < 3; e++)
            {
                var edge = mdt.GetFaceEdge(f, e);
                var data = mdt.GetEdgeMeta(edge);
                GD.Print($"edge #{edge} data: {data}");
            }
        }

        for (int i = 0; i < mdt.GetVertexCount(); i += 3)
        {
            var v1 = mdt.GetVertex(i);
            var v2 = mdt.GetVertex(i + 1);
            var v3 = mdt.GetVertex(i + 2);

            // project points to 2d
            var p1 = camera.UnprojectPosition(v1);
            var p2 = camera.UnprojectPosition(v2);
            var p3 = camera.UnprojectPosition(v3);
            // line 1
            points.Add(p1);
            points.Add(p2);
            // line 2
            points.Add(p2);
            points.Add(p3);
            // closing line
            points.Add(p3);
            points.Add(p1);

            triangleLines.Add(new List<Vector2> { p1, p2, p2, p3, p3, p1 });
        }
        */

        QueueRedraw();
    }

    private void GeneratePoints(Vector3[] vertices, int[] indices)
    {
        for (int i = 0; i < indices.Length; i += 3)
        {
            //GD.Print($"index: {i}, vertices count: {vertices.Length}");

            var i1 = indices[i];
            var i2 = indices[i + 1];
            var i3 = indices[i + 2];

            var v1 = vertices[i1];
            var v2 = vertices[i2];
            var v3 = vertices[i3];

            // TODO draw the normal
            var normal = ((v2 - v1).Cross(v3 - v1)).Normalized();
            var midPoint = (v1 + v2 + v3) / 3;
            var n1 = midPoint;
            // TODO: normal was wrong direction
            var n2 = midPoint - normal;
            triangleLines.Add(new() { n1, n2 });

            // line 1
            points.Add(v1);
            points.Add(v2);
            // line 2
            points.Add(v2);
            points.Add(v3);
            // closing line
            points.Add(v3);
            points.Add(v1);

            triangleLines.Add(new() { v1, v2, v2, v3, v3, v1 });
        }

        /*
        for (int i = 0; i < indices.Length; i += 3)
        {
            GD.Print($"index: {i}, vertices count: {vertices.Length}");

            var i1 = indices[i];
            var i2 = indices[i + 1];
            var i3 = indices[i + 2];

            var v1 = vertices[i1];
            var v2 = vertices[i2];
            var v3 = vertices[i3];

            // TODO draw the normal
            var normal = ((v2 - v1).Cross(v3 - v1)).Normalized();
            var midPoint = (v1 + v2 + v3) / 3;
            var n1 = midPoint;
            var n2 = midPoint + normal;
            var np1 = camera.UnprojectPosition(n1);
            var np2 = camera.UnprojectPosition(n2);
            points.Add(np1);
            points.Add(np2);
            triangleLines.Add(new() { np1, np2 });

            // project points to 2d
            var p1 = camera.UnprojectPosition(v1);
            var p2 = camera.UnprojectPosition(v2);
            var p3 = camera.UnprojectPosition(v3);
            // line 1
            points.Add(p1);
            points.Add(p2);
            // line 2
            points.Add(p2);
            points.Add(p3);
            // closing line
            points.Add(p3);
            points.Add(p1);

            triangleLines.Add(new() { p1, p2, p2, p3, p3, p1 });
        }
        */
    }

    public override void _Process(double delta)
    {
        QueueRedraw();
    }

    public override void _Draw()
    {
        //GD.Print($"Drawing {points.Count} points");
        var random = new Random();

        var camera = GetViewport().GetCamera3D();
        var transform = meshInstance.GlobalTransform;

        foreach (var shape in shapes)
        {
            var points2d = new List<Vector2>();
            foreach (var point in shape.points)
            {
                var pGlobal = transform * point; 
                var p2d = camera.UnprojectPosition(pGlobal);
                points2d.Add(p2d);
            }

			//var r = random.Next(0, colors.Length);
            var color = shape.color; //Colors.White.Blend(Colors.Transparent); //colors[r];

			for (int p = 0; p < points2d.Count - 1; p++)
            {
                DrawLine(points2d[p], points2d[p + 1], color);
            }
		}

        return;

        for (int t = 0; t < triangleLines.Count; t++)
        {
            var r = random.Next(0, colors.Length);
            var color = Colors.White; //colors[r];

            var tPoints = triangleLines[t];
            for (int i = 0; i < tPoints.Count; i += 2)
            {
                if (i + 1 >= tPoints.Count)
                {
                    continue;
                }

                var l1 = tPoints[i];
                var l2 = tPoints[i + 1];

                var g1 = transform * l1;
                var g2 = transform * l2;

                var p1 = camera.UnprojectPosition(g1);
                var p2 = camera.UnprojectPosition(g2);
                DrawLine(p1, p2, color);
            }
        }

        return;

        /*
        for (int i = 0; i < points.Count; i += 2)
        {
            if (i + 1 >= points.Count)
            {
                continue;
            }

            GD.Print($"Drawing line from {points[i]} to {points[i+1]}");
            DrawLine(points[i], points[i + 1], Colors.White);
        }
        */
    }
}


