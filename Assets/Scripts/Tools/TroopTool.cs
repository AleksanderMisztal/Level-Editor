using System.Collections.Generic;
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

        public override void SetVisibleSize(int x, int y)
        {
            
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