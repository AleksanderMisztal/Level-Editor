using System.Collections.Generic;
using Saving;
using Tilemaps;
using Troops;
using UnityEngine;

namespace Tools
{
    public class TroopInstantiator : DesignTool
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


        private void Start()
        {
            foreach (TroopTemplate template in troopTemplates)
                troopByName.Add(template.name, template);
            troopGrid = new TroopGrid(Initializer.grid);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Saver.Save(LevelConfiguration.name +  "_troops", troopGrid.Dto());
            }
            
            if (Input.GetKeyDown(KeyCode.L))
            {
                TroopGridDto dto = Saver.Read<TroopGridDto>(LevelConfiguration.name +  "_troops");
                troopGrid.Load(dto);
            }
            
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
    }
}