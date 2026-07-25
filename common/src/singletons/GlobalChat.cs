using Godot;
using System;


public partial class GlobalChat : Node
{
    public static GlobalChat Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }
}
