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
        
        private HexGrid hexGrid;
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
            hexGrid = new HexGrid(gridBase);
            foreach (TroopTemplate template in templates)
                templateByName.Add(template.name, template);
        }

        public override void Resize()
        {
            foreach (VectorTwo v in gridBase.newReachable)
            {
                GameObject go = hexGrid.GetTile(v.X, v.Y);
                if (go is null) continue;
                go.SetActive(true);
            }
            foreach (VectorTwo v in gridBase.newUnreachable)
            {
                GameObject go = hexGrid.GetTile(v.X, v.Y);
                if (go is null) continue;
                go.SetActive(false);
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
                VectorTwo v = gridBase.ToOffset(position);
                if (!gridBase.IsInside(v.X, v.Y)) return;
                hexGrid.SetTile(position, CreateObject(position, templates[activeId]));
            }
        }

        private GameObject CreateObject(int x, int y, TroopTemplate template)
        {
            Vector3 position = gridBase.ToWorld(x, y);
            return CreateObject(position, template);
        }
        private static GameObject CreateObject(Vector3 position, TroopTemplate template)
        {
            GameObject go = new GameObject(template.name);
            go.transform.position = position;
            SpriteRenderer spriteRenderer = go.gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = template.Sprite;
            go.transform.localScale *= 7;
            return go;
        }

        public override void Load()
        {
            GridDto dto = Saver.Read<GridDto>(LevelConfig.name + "/troops");
            int id = 0;
            for (int y = 0; y < gridBase.YSize; y++)
            for (int x = 0; x < gridBase.XSize; x++)
            {
                TroopTemplate template = GetTroop(dto.objects[id++]);
                hexGrid.SetTile(x, y, template is null ? null : CreateObject(x, y, template));

            }
        }

        public override void Save()
        {
            Saver.Save(LevelConfig.name + "/troops", hexGrid.Dto());
        }
    }
}