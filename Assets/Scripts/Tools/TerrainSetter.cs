using System.Collections.Generic;
using CodeMonkey.Utils;
using Saving;
using Tilemaps;
using UnityEngine;
using Terrain = Tilemaps.Terrain;

namespace Tools
{
    public class TerrainSetter : DesignTool
    {
        [SerializeField] private Terrain[] terrains;
        private int currentTerrainId;
        public static Dictionary<string, Terrain> terrainByName = new Dictionary<string, Terrain>();
        
        private TerrainGrid terrainGrid;
        private TextGrid textGrid;

        public static Terrain GetTerrain(string name)
        {
            try
            {
                return terrainByName[name];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        private void Start()
        {
            foreach (Terrain terrain in terrains) 
                terrainByName.Add(terrain.name, terrain);
            
            terrainGrid = new TerrainGrid(Initializer.grid);
            textGrid = new TextGrid(Initializer.grid);
        }

        protected void Update()
        {
            if (!Enabled) return;
            
            if (Input.GetKeyDown("n"))
            {
                currentTerrainId++;
                currentTerrainId %= terrains.Length;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Initializer.GetHexCenterWp();
                Terrain terrain = terrains[currentTerrainId];
                
                terrainGrid.SetTile(mousePosition, terrain);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Saver.Save("terrainSave", terrainGrid.Dto());
                Saver.Save("test123", Vector3.up);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                TerrainGridDto dto = Saver.Read<TerrainGridDto>("terrainSave");
                terrainGrid.Load(dto);
            }
        }
    }
}