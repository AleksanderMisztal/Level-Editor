﻿using System.Collections.Generic;
using GameDataStructures.Positioning;
using LevelEditor.Saving;
using LevelEditor.Tilemaps;
using UnityEngine;

namespace LevelEditor.Tools
{
    public abstract class HexTool<T> : HexTool where T : class
    {
        protected abstract string saveFileName { get; }
        
        // ReSharper disable once Unity.RedundantSerializeFieldAttribute
        [SerializeField] protected T[] templates;
        
        protected readonly Dictionary<string, T> templateByName = new Dictionary<string, T>();
        protected abstract string GetName(T template);
        private int activeId;
        
        protected HexGrid hexGrid;
        protected GridBase gridBase;
        
        public override bool Enabled { protected get; set; }

        private T GetTemplate(string templateName)
        {
            try
            {
                return templateByName[templateName];
            }
            catch (KeyNotFoundException)
            {
                if (!string.IsNullOrEmpty(templateName))Debug.Log("Couldn't find template with name " + templateName);
                return null;
            }
        }
        
        public override void Initialize(GridBase gridBase)
        {
            gridBase.gridResized += Resize;
            this.gridBase = gridBase;
            hexGrid = new HexGrid(gridBase);
            foreach (T template in templates)
                templateByName.Add(GetName(template), template);
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
                Vector3 position = gridBase.GetHexCenterWp();
                VectorTwo v = gridBase.ToOffset(position);
                if (!gridBase.IsInside(v.X, v.Y)) return;
                hexGrid.SetTile(position, CreateObject(position, templates[activeId]));
            }
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

        private GameObject CreateObject(int x, int y, T template)
        {
            Vector3 position = gridBase.ToWorld(x, y);
            return CreateObject(position, template);
        }

        protected abstract GameObject CreateObject(Vector3 position, T template);

        
        public override void Save()
        {
            Saver.Save(LevelConfig.name + "/" + saveFileName, hexGrid.Dto());
        }
        
        public override void Load()
        {
            GridDto dto = Saver.Read<GridDto>(LevelConfig.name + "/" + saveFileName);
            int id = 0;
            for (int y = 0; y < gridBase.YSize; y++)
            for (int x = 0; x < gridBase.XSize; x++)
            {
                T template = GetTemplate(dto.objects[id++]);
                hexGrid.SetTile(x, y, template is null ? null : CreateObject(x, y, template));
            }
        }
    }
}