using Godot;


public class Triangle
{
	public Vector3 v1; public Vector3 v2; public Vector3 v3;
	public int i1; public int i2; public int i3;
	public Vector3 normal;

	public Triangle(Vector3 v1, Vector3 v2, Vector3 v3, int i1, int i2, int i3)
	{
		this.v1 = v1;
		this.v2 = v2;
		this.v3 = v3;
		this.i1 = i1;
		this.i2 = i2;
		this.i3 = i3;

		normal = CalculateNormal(v1, v2, v3);
	}

	// TODO: simplify
	public bool IsCoplanarWith(Triangle other)
	{
		// TODO: what does this calculate?
		// edge 1?
		float a1 = v2.X - v1.X;
		float b1 = v2.Y - v1.Y;
		float c1 = v2.Z - v1.Z;
		// edge 2?
		float a2 = v3.X - v1.X;
		float b2 = v3.Y - v1.Y;
		float c2 = v3.Z - v1.Z;
		// cross product?
		float a = b1 * c2 - b2 * c1;
		float b = a2 * c1 - a1 * c2;
		float c = a1 * b2 - b1 * a2;
		// this should be the plane constant?
		// TODO: which operators / signs to use here? are they carried over from the cross product?
		float d = (-a * v1.X - b * v1.Y - c * v1.Z);

		var o = other.v1;
		var coplanarWithO = (a * o.X + b * o.Y + c * o.Z + d == 0);
		return coplanarWithO;
	}

	private Vector3 CalculateNormal(Vector3 v1, Vector3 v2, Vector3 v3)
	{
		return ((v2 - v1).Cross(v3 - v1)).Normalized();
	}

	/*
	public bool IsCoplanarWith(Triangle other)
	{
		// TODO: is this the way of calculating the plane constant???
		var a = other.v1.Dot(other.normal);
		var b = v1.Dot(normal);

		return (a == b);

		// TODO: is this actually checking for coplanar-ity?
		var p = other.v1;
		return ((p.X * normal.X + p.Y * normal.Y + p.Z * normal.Z) == 0);
	}
	*/
}

