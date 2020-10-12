using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName="New Terrain", menuName="Terrain")]
    public class Terrain : ScriptableObject
    {
        public int moveCost;

        public string Data => name + " " + moveCost;

        private void OnEnable() => Debug.Log(Data);

        public override string ToString() => name;
    }
}