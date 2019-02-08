using Godot;

public class SceneManager : Node
{
    private void RealoadScene(object body)
    {
        var node = (Node)body;
        if(node.GetName() == "Player")
        {
            GetTree().ReloadCurrentScene();
        }
    }
}