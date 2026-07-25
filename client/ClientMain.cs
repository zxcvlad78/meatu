using System;
using System.Linq;
using Godot;

namespace MeatU;

public partial class ClientMain : Node
{
    [Export] Label labelMessages;
    [Export] LineEdit lineEditInput;
    [Export] LineEdit ip;
    [Export] Button buttonConnect;

    public override void _Ready()
    {
        GlobalChat.Instance.OnMessageReceived += OnMessageReceived;
        lineEditInput.TextSubmitted += LineeeditTextSubmitted;
        buttonConnect.Pressed += ConnectPressed;
    }

    private void ConnectPressed()
    {
        string[] sex = ip.Text.Split(":");
        if (sex.Length != 2)
        {
            GD.PushError("Idi naher!");
            return;
        }
        Network.Instance.CreateClient(sex[0], sex[1].ToInt());
    }

    private void OnMessageReceived(GlobalChat.Message message)
    {
        labelMessages.Text += $"{message.UserId}: {message.Content}\n";
    }


    private void LineeeditTextSubmitted(string newText)
    {
        GlobalChat.Instance.SendMessage(newText);
    }
}

