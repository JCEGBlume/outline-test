using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Edge
{
	public Vector3 a;
	public Vector3 b;

	public Edge(Vector3 a, Vector3 b)
	{
		this.a = a;
		this.b = b;
	}

	public bool SameAs(Edge other)
	{
		return (a == other.a &&  b == other.b) || (a == other.b && b == other.a);
	}
}
