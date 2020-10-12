using Tilemaps;
using Tools;
using UnityEngine;
using Terrain = Scriptables.Terrain;

public class Initializer : MonoBehaviour
{
    [SerializeField] private Terrain terrain;
        
    private HexGrid<Terrain> grid;
    private new static Camera camera;

    private void Start()
    {
        camera = Camera.main;
        grid = new HexGrid<Terrain>(100, 100, 10f);
        FindObjectOfType<TileSetter>().Grid = grid;
    }

    public static Vector3 GetMouseWorldPosition()
    {
        return camera.ScreenToWorldPoint(Input.mousePosition);
    }
}