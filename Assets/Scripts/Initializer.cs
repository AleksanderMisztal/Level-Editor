using Tilemaps;
using Tools;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    private DesignTool[] tools;
    private int activeTool = 0;
        
    public static GridBase grid;
    private new static Camera camera;
    private const float cellSize = 10f;

    private void Start()
    {
        grid = new GridBase(100, 100, cellSize);
        camera = Camera.main;
        tools = FindObjectsOfType<DesignTool>();
        foreach (DesignTool tool in tools) tool.Initialize();
        tools[0].Enabled = true;

        if (LevelConfiguration.isLoaded)
            foreach (DesignTool tool in tools)
                tool.Load();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            NextTool();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            foreach (DesignTool tool in tools) 
                tool.Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            foreach (DesignTool tool in tools) 
                tool.Load();
        }
    }

    private void NextTool()
    {
        tools[activeTool++].Enabled = false;
        if (activeTool >= tools.Length) activeTool -= tools.Length;
        tools[activeTool].Enabled = true;
    }

    public static Vector3 GetMouseWorldPosition()
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