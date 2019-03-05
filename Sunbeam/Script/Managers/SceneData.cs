using System.Collections.Generic;
using Sunbeam.Data;

namespace Sunbeam
{
    public class SceneData
    {
        public string Name;
        public int CheckPoint;
        public List<IData> Data;

        public SceneData(string name)
        {
            Name = name;
            CheckPoint = 0;
            Data = new List<IData>();
        }
    }
}