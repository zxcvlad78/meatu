using System.Threading.Tasks;
using Godot;

namespace MeatU;

public partial class CommonMain : Node
{
    public override void _Ready()
    {
        if (OS.HasFeature("dedicated_server"))
        {
            GetTree().CallDeferred("change_scene_to_file", "uid://cqu3jsht368md");
        }
        else
        {
            GetTree().CallDeferred("change_scene_to_file", "uid://4wuewgpxprhb");
        }
    }

}