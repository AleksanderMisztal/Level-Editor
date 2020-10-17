using System;
using UnityEngine;

namespace LevelEditor.Saving
{    
    [Serializable]
    public class BoardDto
    {
        public string background;
        public Vector3 offset;
        public int xSize;
        public int ySize;
        public float cameraSize;
    }
}