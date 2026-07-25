using Godot;


public partial class Network : Node
{
    public static Network Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }
}