using Godot;
using System;

public partial class SimpleControls : Node3D
{
    public override void _Process(double _delta)
    {
        var delta = (float)_delta;

        this.Rotate(Vector3.Up, 1.0f * delta);
    }
}
