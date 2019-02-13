using Godot;

public class Menu : Control
{
    public void Quit()
    {
        GetTree().Quit();
    }
}