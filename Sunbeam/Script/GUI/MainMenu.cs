public class MainMenu : Menu
{
    private void Play()
    {
        GetTree().ChangeScene(GetScene("TestLevel"));
    }
}