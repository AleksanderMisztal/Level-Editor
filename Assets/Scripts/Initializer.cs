using LevelEditor.Saving;
using LevelEditor.Tilemaps;
using LevelEditor.Tools;
using UnityEngine;

namespace LevelEditor
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] private HexTool[] tools;
        private BoardTool boardTool;
        private int activeTool;

        private ResizableGridBase gridBase;
        private const float cellSize = 10f;

        private void Start()
        {
            gridBase = new ResizableGridBase(cellSize);
            boardTool = GetComponent<BoardTool>();
            boardTool.Initialize(gridBase);
            foreach (HexTool tool in tools) tool.Initialize(gridBase);
            tools[0].Enabled = true;

            if (LevelConfig.isLoaded) Load();
            else Save();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T)) NextTool();
            if (Input.GetKeyDown(KeyCode.S)) Save();
            if (Input.GetKeyDown(KeyCode.L)) Load();
        }

        private void NextTool()
        {
            tools[activeTool++].Enabled = false;
            if (activeTool >= tools.Length) activeTool -= tools.Length;
            tools[activeTool].Enabled = true;
        }

        private void Save()
        {
            boardTool.Save();
            foreach (HexTool tool in tools)
                tool.Save();
        }

        private void Load()
        {
            GridDto dto = Saver.Read<GridDto>(LevelConfig.name + "/troops");
            gridBase.Resize(dto.xSize, dto.ySize);
            boardTool.Load();
            foreach (HexTool tool in tools)
                tool.Load();
        }
    }
}