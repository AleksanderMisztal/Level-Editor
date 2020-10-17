using GameDataStructures;
using UnityEngine;

namespace LevelEditor.Troops
{
    [CreateAssetMenu(fileName="New Bomber", menuName="Troops/Bomber")]
    public class BomberTemplate : TroopTemplate
    {
        public override TroopType Type => TroopType.Bomber;
    }
}