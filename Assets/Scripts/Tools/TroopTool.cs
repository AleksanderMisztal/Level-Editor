using System.Collections.Generic;
using GameDataStructures.Positioning;
using Saving;
using Tilemaps;
using Troops;
using UnityEngine;

namespace Tools
{
    public class TroopTool : DesignTool
    {
        [SerializeField] private TroopTemplate[] troopTemplates;
        private int activeId = 0;
        private TroopGrid troopGrid;
        private static readonly Dictionary<string, TroopTemplate> troopByName = new Dictionary<string, TroopTemplate>();

        public static TroopTemplate GetTroop(string name)
        {
            try
            {
                return troopByName[name];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public override void Initialize()
        {
            troopGrid = new TroopGrid(Initializer.grid);
            foreach (TroopTemplate template in troopTemplates)
                troopByName.Add(template.name, template);
        }

        public override void Resize()
        {
            foreach (VectorTwo v in Initializer.grid.newReachable)
            {
                GameObject troop = troopGrid.GetTile(v.X, v.Y);
                if (troop is null) continue;
                troop.SetActive(true);
                Debug.Log(troop + "activated");
            }

            Debug.Log("Deactivating " + Initializer.grid.newUnreachable.Count + " tiles");
            if (Initializer.grid.newUnreachable.Count < 50)
                foreach (VectorTwo v in Initializer.grid.newUnreachable)
                {
                    Debug.Log(v);
                }
            foreach (VectorTwo v in Initializer.grid.newUnreachable)
            {
                GameObject troop = troopGrid.GetTile(v.X, v.Y);
                if (troop is null) continue;
                troop.SetActive(false);
                Debug.Log(troop + "deactivated");
            }
        }

        private void Update()
        {
            if (!Enabled) return;
            
            if (Input.GetKeyDown("n"))
            {
                activeId++;
                activeId %= troopTemplates.Length;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 position = Initializer.GetHexCenterWp();
                troopGrid.SetTile(position, troopTemplates[activeId]);
            }
        }

        public override void Load()
        {
            GridDto dto = Saver.Read<GridDto>(LevelConfiguration.name + "/troops");
            troopGrid.Load(dto);
        }

        public override void Save()
        {
            Saver.Save(LevelConfiguration.name + "/troops", troopGrid.Dto());
        }
    }
}