using LevelEditor.Saving;
using LevelEditor.Tilemaps;
using LevelEditor.Tools;
using UnityEngine;

namespace LevelEditor
{
    public class Initializer : MonoBehaviour
    {
        private DesignTool[] tools;
        private int activeTool;

        private GridBase gridBase;
        private new static Camera camera;
        private const float cellSize = 10f;

        private int xSize = 10, ySize = 10;

        private void Start()
        {
            gridBase = new GridBase(30, 30, cellSize);
            camera = Camera.main;
            tools = FindObjectsOfType<DesignTool>();
            foreach (DesignTool tool in tools) tool.Initialize(gridBase);
            tools[0].Enabled = true;
            BoardTool.resizeCallback = v => Resize(xSize + v.X, ySize + v.Y);

            if (LevelConfiguration.isLoaded) Load();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T)) NextTool();
            if (Input.GetKeyDown(KeyCode.S))
                foreach (DesignTool tool in tools)
                    tool.Save();
            if (Input.GetKeyDown(KeyCode.L)) Load();
        }

        private void Load()
        {
            GridDto dto = Saver.Read<GridDto>(LevelConfiguration.name + "/troops");
            Resize(dto.xSize, dto.ySize);
            foreach (DesignTool tool in tools)
                tool.Load();
        }

        private void Resize(int newX, int newY)
        {
            if (newX < 5 || newX >= gridBase.maxXSize || newY < 5 || newY >= gridBase.maxYSize) return;
            xSize = newX;
            ySize = newY;
            gridBase.Redraw(newX, newY);
            foreach (DesignTool tool in tools) tool.Resize();
        }

        private void NextTool()
        {
            tools[activeTool++].Enabled = false;
            if (activeTool >= tools.Length) activeTool -= tools.Length;
            tools[activeTool].Enabled = true;
        }

        private static Vector3 GetMouseWorldPosition()
        {
            Vector3 wp = camera.ScreenToWorldPoint(Input.mousePosition);
            wp.z = 0;
            return wp;
        }

        public static Vector3 GetHexCenterWp()
        {
            Vector3 wp = GetMouseWorldPosition();
            return Cube.ToCube(wp, cellSize).ToWorld(cellSize);
        }
    }
}