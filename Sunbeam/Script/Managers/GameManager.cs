namespace Sunbeam
{
    public class GameManager
    {
        private static GameManager m_instance;
        private SceneData m_sceneData;

        public GameManager()
        {
            m_sceneData = new SceneData(string.Empty);
        }

        public void SetCheckpoint(int index)
        {
            m_sceneData.CheckPoint = index;
        }

        public SceneData GetSceneData(string name)
        {
            return (m_sceneData.Name == name)
                ? m_sceneData
                : m_sceneData = new SceneData(name);
        }

        public static GameManager Instance => m_instance ?? (m_instance = new GameManager());
    }
}