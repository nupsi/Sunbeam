using Godot;

public class MainMenu : Menu
{
    [Export]
    public string PlaySceneName;

    private void Play()
    {
        GetTree().ChangeScene(SceneManager.GetScene(PlaySceneName));
    }
}