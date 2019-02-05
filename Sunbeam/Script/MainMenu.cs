using Godot;

public class MainMenu : Panel
{
    private void Play()
    {
        var tree = GetTree();
        tree.ChangeScene("Scenes/TestLevel.tscn");
        tree.GetRoot().Dispose();
    }

    private void Quit()
    {
        GetTree().Quit();
    }
}