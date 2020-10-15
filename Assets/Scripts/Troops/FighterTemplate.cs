using GameDataStructures;
using UnityEngine;

namespace Troops
{
    [CreateAssetMenu(fileName="New Fighter", menuName="Troops/Fighter")]
    public class FighterTemplate : TroopTemplate
    {
        public override TroopType Type => TroopType.Fighter;
    }
}
