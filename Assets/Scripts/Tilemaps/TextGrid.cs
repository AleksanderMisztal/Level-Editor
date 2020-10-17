using LevelEditor.CodeMonkey.Utils;
using UnityEngine;

namespace LevelEditor.Tilemaps
{
    public class TextGrid : HexGrid<TextMesh>
    {
        public TextGrid(GridBase gridBase) : base(gridBase) { }

        public void SetTile(int x, int y, string text)
        {
            if (!gridBase.IsInside(x, y)) return;
            if (tiles[x, y] is null) 
                tiles[x, y] = UtilsClass.CreateWorldText(text, null, gridBase.ToWorld(x, y));
            else
            {
                tiles[x, y].text = text;
            }
        }
    }
}