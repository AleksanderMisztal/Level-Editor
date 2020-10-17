using UnityEngine;

namespace Tools
{
    public abstract class DesignTool : MonoBehaviour
    {
        public bool Enabled { protected get; set; }
        public abstract void Initialize();
        public abstract void SetVisibleSize(int x, int y);
        public abstract void Save();
        public abstract void Load();
    }
}