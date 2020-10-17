using UnityEngine;

namespace LevelEditor.Tools
{
    public abstract class DesignTool : MonoBehaviour
    {
        public bool Enabled { protected get; set; }
        public abstract void Initialize();
        public abstract void Resize();
        public abstract void Save();
        public abstract void Load();
    }
}