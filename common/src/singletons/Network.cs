using Godot;

namespace MeatU;

public partial class Network : Node
{
    public static Network Instance { get; private set; }

    public enum Channel: byte
    {
        Default = 0,
        GlobalMessage,
    }

    public override void _Ready()
    {
        Instance = this;

        SceneMultiplayer api = new();
        api.ServerRelay = false;
        GDNet.Instance.Setup(api);
    }

    public Error CreateServer(int port)
    {
        var peer = new ENetMultiplayerPeer();
        var err = peer.CreateServer(port, 4000);
        if (err == Error.Ok)
            Multiplayer.MultiplayerPeer = peer;
        return err;
    }

    public Error CreateClient(string address, int port)
    {
        var peer = new ENetMultiplayerPeer();
        var err = peer.CreateClient(address, port);
        if (err == Error.Ok)
            Multiplayer.MultiplayerPeer = peer;
        return err;
    }
}
