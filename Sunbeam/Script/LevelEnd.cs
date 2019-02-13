using Godot;

public class LevelEnd : Area2D
{
    [Export]
    public string SceneName;

    private void LoadScene(object body)
    {
        var node = (Node)body;
        if(node.GetName() == "Player")
        {
            GetTree().ChangeScene(Menu.GetScene(SceneName));
        }
    }
}