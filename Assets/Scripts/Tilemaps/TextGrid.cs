using CodeMonkey.Utils;
using UnityEngine;

namespace Tilemaps
{
    public class TextGrid : HexGrid<TextMesh>
    {
        public TextGrid(GridBase gridBase) : base(gridBase) { }

        public void SetTile(int x, int y, string text)
        {
            if (tiles[x, y] is null) 
                tiles[x, y] = UtilsClass.CreateWorldText(text, null, gridBase.ToWorld(x, y));
            else
            {
                tiles[x, y].text = text;
            }
        }
    }
}