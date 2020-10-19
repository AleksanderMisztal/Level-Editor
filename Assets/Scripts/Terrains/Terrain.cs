using System;
using UnityEngine;

namespace LevelEditor.Terrains
{
    [CreateAssetMenu(fileName="New Terrain", menuName="Terrain")]
    [Serializable]
    public class Terrain : ScriptableObject
    {
        public int moveCost;
    }
}