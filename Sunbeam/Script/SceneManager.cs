using Godot;

public class SceneManager : Node
{
    public static SceneManager Instance;

    private float m_time;

    public override void _Ready()
    {
        Instance = this;
        m_time = 0;
    }

    public override void _Process(float delta)
    {
        m_time += delta;
    }

    public void ChangeScene(string name)
    {
        GD.Print("Level completed in " + (Mathf.Round(m_time * 10) / 10) + " seconds!");
        GetTree().ChangeScene(GetScene(name));
    }

    private void RealoadScene(object body)
    {
        var node = (Node)body;
        if(node.GetName() == "Player")
        {
            GetTree().ReloadCurrentScene();
        }
    }

    public static string GetScene(string _name)
    {
        return string.Format("Scenes/{0}.tscn", _name.Trim());
    }
}