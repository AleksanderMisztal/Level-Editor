using LevelEditor.Tilemaps;
using UnityEngine;

namespace LevelEditor.Tools
{
    public abstract class HexTool : MonoBehaviour
    {
        public bool Enabled { protected get; set; }
        public abstract void Initialize(GridBase gridBase);
        public abstract void Resize();
        public abstract void Save();
        public abstract void Load();
    }
}