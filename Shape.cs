using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Shape
{
	public List<Vector3> points;
	public Color color;

	public Shape(List<Vector3> points)
	{
		this.points = points;
	}
}
