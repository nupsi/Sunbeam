using System;
using Godot;

namespace Sunbeam
{
    public class Checkpoint : Trigger
    {
        [Export]
        public int CheckpointIndex;

        public override void _Ready()
        {
            base._Ready();
            SceneManager.Instance.ReqisterCheckpoint(this);
        }

        protected override void EnterArea(object body)
        {
            GameManager.Instance.SetCheckpoint(CheckpointIndex);
        }

        protected override void ExitArea(object body) { }

        protected override bool ListenEnter => true;
        protected override bool ListenExit => false;
    }
}