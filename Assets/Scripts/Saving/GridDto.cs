using System;

namespace Saving
{
    [Serializable]
    public class GridDto
    {
        public int xSize;
        public int ySize;
        public string[] data;
    }
}