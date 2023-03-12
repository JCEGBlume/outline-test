using Godot;
using System;

public partial class SimpleControls : Node3D
{
	[Export] float rotationPerSecond = 0.5f;
	 
	public override void _Process(double _delta)
	{
		var delta = (float)_delta;

		this.Rotate(Vector3.Up, rotationPerSecond * delta);
	}
}
