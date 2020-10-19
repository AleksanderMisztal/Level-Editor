using LevelEditor.Troops;
using UnityEngine;

namespace LevelEditor.Tools
{
    public class TroopTool : HexTool<TroopTemplate>
    {
        protected override string saveFileName => "troops";
        
        protected override string GetName(TroopTemplate template) => template.name;

        protected override GameObject CreateObject(Vector3 position, TroopTemplate template)
        {
            GameObject go = new GameObject(template.name);
            go.transform.position = position;
            SpriteRenderer spriteRenderer = go.gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = template.Sprite;
            go.transform.localScale *= 7;
            return go;
        }
    }
}