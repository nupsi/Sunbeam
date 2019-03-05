using System;

namespace Sunbeam.Data
{
    [Serializable]
    public class ActionData : IData
    {
        private bool m_completed;
        public bool Completed
        {
            set
            {
                m_completed = value;
                GameManager.Instance.UpdateData();
            }

            get
            {
                return m_completed;
            }
        }

        public ActionData(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}