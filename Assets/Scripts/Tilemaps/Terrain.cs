using System;
using UnityEngine;

namespace Tilemaps
{
    [CreateAssetMenu(fileName="New Terrain", menuName="Terrain")]
    [Serializable]
    public class Terrain : ScriptableObject
    {
        public int moveCost;
    }
}