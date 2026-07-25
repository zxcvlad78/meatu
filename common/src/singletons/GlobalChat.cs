using Godot;
using System;

namespace MeatU {

public partial class GlobalChat : Node
{
    public static GlobalChat Instance { get; private set; }

    private GDNetRpc _rpc = new();

    public event Action<Message> OnMessageReceived;

    public struct Message : IGDNetSerializable
    {
        public string UserId;
        public string Content;

        public Message(string userId, string content)
        {
            UserId = userId;
            Content = content;
        }

        public void Deserialize(GDNetBuffer buffer)
        {
            UserId = buffer.ReadString();
            Content = buffer.ReadString();
        }

        public void Serialize(GDNetBuffer buffer)
        {
            buffer.WriteString(UserId);
            buffer.WriteString(Content);
        }
    }

    public override void _Ready()
    {
        Instance = this;
        _rpc.BindOwnerAsNode(this);
        _rpc.BindAll(this);
    }

    public void SendMessage(string message)
    {
        _rpc.InvokeOnServer(ServerMessageReceivedFromClientRpc, message, OS.GetUniqueId());
    }

    [GDNetRpc(Permission = Permission.Any, Channel = (int)Network.Channel.GlobalMessage)]
    private void ServerMessageReceivedFromClientRpc(string message, string osUniqueId)
    {
        int sender = _rpc.GetRemoteSender();
        string userId = "User/" + osUniqueId;

        Message broadcastMessage = new(userId, message);

        _rpc.Invoke(BroadcastMessageToAllRpc, sender, broadcastMessage);
    }

    [GDNetRpc(Permission = Permission.ServerOrAuth, Channel = (int)Network.Channel.GlobalMessage)]
    private void BroadcastMessageToAllRpc(Message message)
    {
        OnMessageReceived?.Invoke(message);
    }

}

}