using System;
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


    private void Awake()
    {
        grid = new GridBase(10, 20, cellSize);
    }

    private void Start()
    {
        camera = Camera.main;
        tools = FindObjectsOfType<DesignTool>();
        tools[0].Enabled = true;
    }

    public void NextTool()
    {
        tools[activeTool].Enabled = false;
        tools[++activeTool].Enabled = true;
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