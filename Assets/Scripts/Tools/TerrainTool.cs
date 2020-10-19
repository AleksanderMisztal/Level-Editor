using UnityEngine;
using Terrain = LevelEditor.Terrains.Terrain;

namespace LevelEditor.Tools
{
    public class TerrainTool : HexTool<Terrain>
    {
        protected override string SaveFileName => "terrains";
        
        protected override string GetName(Terrain template) => template.name;

        protected override GameObject CreateObject(Vector3 position, Terrain template)
        {
            GameObject go = TextCreator.CreateWorldText(template.name, null, position).gameObject;
            go.name = template.name;
            return go;
        }
    }
}