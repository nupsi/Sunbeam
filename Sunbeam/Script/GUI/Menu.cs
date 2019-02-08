using Godot;

public class Menu : Control
{
    public void Quit()
    {
        GetTree().Quit();
    }

    public static string GetScene(string _name)
    {
        return string.Format("Scenes/{0}.tscn", _name.Trim());
    }
}