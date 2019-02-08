using Godot;

public class LevelEnd : Area2D
{
    private void LoadScene(object body)
    {
        var node = (Node)body;
        if(node.GetName() == "Player")
        {
            GetTree().ChangeScene(Menu.GetScene(GetName()));
        }
    }
}