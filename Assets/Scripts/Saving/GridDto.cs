using System;
using UnityEngine.Serialization;

namespace LevelEditor.Saving
{
    [Serializable]
    public class GridDto
    {
        public int xSize;
        public int ySize;
        [FormerlySerializedAs("data")] public string[] objects;
    }
}