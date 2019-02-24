using Godot;
using System;
using System.Collections.Generic;

namespace Sunbeam
{
    public class SceneManager : Node
    {
        public static SceneManager Instance;

        [Export]
        public bool TrackTime;

        private float m_time;
        private List<Checkpoint> m_checkpoints;
        private SceneData m_sceneData;

        public override void _EnterTree()
        {
            base._EnterTree();
            Instance = this;
            m_checkpoints = new List<Checkpoint>();
            m_sceneData = GameManager.Instance.GetSceneData(GetName());
        }

        public override void _Ready()
        {
            m_time = 0;
            Tree.Paused = false;
            MoveToCheckpoint();
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
            GD.Print("Move to checkpoint " + m_sceneData.CheckPoint + " | " + m_checkpoints.Count);
            m_checkpoints?.ForEach((point) =>
            {
                if(point.CheckpointIndex == m_sceneData.CheckPoint)
                {
                    GD.Print("Checkpoint Found");
                    var player = (KinematicBody2D)GetNode("Game/Player");
                    player.Position = point.Position;
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

        public void Reload()
        {
            Tree.ReloadCurrentScene();
        }

        private void RealoadScene(object body)
        {
            var node = (Node)body;
            if(node.GetName() == "Player")
            {
                Reload();
            }
        }

        public static string GetScene(string _name)
        {
            return string.Format("res://Scenes/{0}.tscn", _name.Trim());
        }

        private SceneTree Tree => GetTree();
    }
}