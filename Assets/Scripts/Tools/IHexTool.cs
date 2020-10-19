using LevelEditor.Tilemaps;
using UnityEngine;

namespace LevelEditor.Tools
{
    public abstract class HexTool : MonoBehaviour
    {
        public abstract bool Enabled { protected get; set; }
        public abstract void Initialize(GridBase gridBase);
        public abstract void Resize();
        public abstract void Load();
        public abstract void Save();
    }
}