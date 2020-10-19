using System.Collections.Generic;
using GameDataStructures.Positioning;
using LevelEditor.Saving;
using LevelEditor.Tilemaps;
using LevelEditor.Troops;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelEditor.Tools
{
    public class TroopTool : HexTool
    {
        [FormerlySerializedAs("troopTemplates")] [SerializeField] private TroopTemplate[] templates;
        private int activeId;
        
        private static readonly Dictionary<string, TroopTemplate> templateByName = new Dictionary<string, TroopTemplate>();
        
        private TroopGrid troopGrid;
        private GridBase gridBase;

        public static TroopTemplate GetTroop(string name)
        {
            try
            {
                return templateByName[name];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }
        

        public override void Initialize(GridBase gridBase)
        {
            this.gridBase = gridBase;
            troopGrid = new TroopGrid(gridBase);
            foreach (TroopTemplate template in templates)
                templateByName.Add(template.name, template);
        }

        public override void Resize()
        {
            foreach (VectorTwo v in gridBase.newReachable)
            {
                GameObject troop = troopGrid.GetTile(v.X, v.Y);
                if (troop is null) continue;
                troop.SetActive(true);
            }
            foreach (VectorTwo v in gridBase.newUnreachable)
            {
                GameObject troop = troopGrid.GetTile(v.X, v.Y);
                if (troop is null) continue;
                troop.SetActive(false);
            }
        }

        private void Update()
        {
            if (!Enabled) return;
            
            if (Input.GetKeyDown("n"))
            {
                activeId++;
                activeId %= templates.Length;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 position = Initializer.GetHexCenterWp();
                troopGrid.SetTile(position, templates[activeId]);
            }
        }

        public override void Load()
        {
            GridDto dto = Saver.Read<GridDto>(LevelConfig.name + "/troops");
            troopGrid.Load(dto);
        }

        public override void Save()
        {
            Saver.Save(LevelConfig.name + "/troops", troopGrid.Dto());
        }
    }
}