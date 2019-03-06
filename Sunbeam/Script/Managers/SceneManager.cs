using Godot;
using Sunbeam.Data;
using System.Collections.Generic;

namespace Sunbeam
{
    public class SceneManager : Node
    {
        public static SceneManager Instance;

        [Export]
        public bool TrackTime;
        public bool Paused;
        private float m_time;
        private List<Checkpoint> m_checkpoints;
        private List<IContainer> m_containers;
        private SceneData m_sceneData;

        public override void _EnterTree()
        {
            base._EnterTree();
            Instance = this;
            m_checkpoints = new List<Checkpoint>();
            m_containers = new List<IContainer>();
            m_sceneData = GameManager.Instance.GetSceneData(GetName());
        }

        public override void _Ready()
        {
            m_time = 0;
            Tree.Paused = false;
            MoveToCheckpoint();
            SetData();
        }

        public override void _Process(float delta)
        {
            if(TrackTime)
            {
                m_time += delta;
            }
        }

        public void ReqisterCheckpoint(Checkpoint checkpoint)
        {
            m_checkpoints.Add(checkpoint);
        }

        private void MoveToCheckpoint()
        {
            m_checkpoints?.ForEach((point) =>
            {
                if(point.CheckpointIndex == m_sceneData.CheckPoint)
                {
                    var player = (KinematicBody2D)GetNode("Game/Player");
                    player.Position = point.Position;
                }
            });
        }

        public void ReqisterContainer(IContainer data)
        {
            m_containers.Add(data);
        }

        public List<IData> GetData()
        {
            var list = new List<IData>();
            m_containers.ForEach((container) =>
            {
                list.Add(container.GetData());
            });
            return list;
        }

        private void SetData()
        {
            m_sceneData?.Data.ForEach((data) =>
            {
                foreach(var container in m_containers)
                {
                    if(data.Name == container.GetName())
                    {
                        container.SetData(data);
                        break;
                    }
                }
            });
        }

        public void EndLevel(string nextLevel)
        {
            if(TrackTime)
            {
                GD.Print("Level completed in " + (Mathf.Round(m_time * 10) / 10) + " seconds!");
            }
            ChangeScene(nextLevel);
        }

        public void ChangeScene(string name)
        {
            Tree.ChangeScene(GetScene(name));
        }

        /// <summary>
        /// Reload scene without deleting saved data.
        /// Use GameManager.Instance.ResetSceneData() to erase saved data.
        /// </summary>
        public void Reload()
        {
            Tree.ReloadCurrentScene();
        }

        public static string GetScene(string _name)
        {
            return string.Format("res://Scenes/{0}.tscn", _name.Trim());
        }

        private SceneTree Tree => GetTree();
    }
}