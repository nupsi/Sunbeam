using Godot;

public class PauseMenu : Menu
{
    public override void _Ready()
    {
        SetProcessInput(true);
        TogglePause(false);
    }

    public override void _Input(InputEvent @event)
    {
        if(Escape)
        {
            TogglePause(!Paused);
        }
    }

    public void Pause()
    {
        TogglePause(true);
    }

    public void Continue()
    {
        TogglePause(false);
    }

    private void TogglePause(bool paused)
    {
        Paused = paused;
        Visible = paused;
    }

    private bool Escape => Input.IsActionJustPressed("ui_cancel");

    private bool Paused
    {
        get => GetTree().Paused;
        set => GetTree().Paused = value;
    }
}