using Tilemaps;
using UnityEngine;
using Terrain = Scriptables.Terrain;

namespace Tools
{
    public class TileSetter : MonoBehaviour
    {
        [SerializeField] private Terrain[] terrains;
        public HexGrid<Terrain> Grid;
        private int currentTerrainId = 0;

        private void Update()
        {
            if (Input.GetKeyDown("n"))
            {
                currentTerrainId++;
                currentTerrainId %= terrains.Length;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouse = Initializer.GetMouseWorldPosition();
                Terrain terrain = terrains[currentTerrainId];
                Grid.SetTile(mouse, terrain);
            }
        }
    }
}