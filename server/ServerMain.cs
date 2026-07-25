using Godot;

namespace MeatU;

public partial class ServerMain : Node
{
    public override void _Ready()
    {
        Network.Instance.CreateServer(7856);
    }
}