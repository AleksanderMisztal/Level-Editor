using System;
using UnityEngine;

namespace LevelEditor.Tilemaps
{
    public static class LineDrawer
    {
        public static GameObject DrawHex(Vector3 center, float size)
        {
            LineRenderer lineRenderer = new GameObject().AddComponent<LineRenderer>();
            Vector3 left = Vector3.left * size;
            Vector3 upLeft = new Vector3(-.5f, (float)Math.Sqrt((double)3f/4)) * size;
            Vector3[] positions = {
                center + left,
                center + upLeft,
                center + upLeft - left,
                center + -left,
                center + -upLeft,
                center + -upLeft + left,
                center + left,
            };
            
            lineRenderer.positionCount = 7;
            lineRenderer.SetPositions(positions);
            return lineRenderer.gameObject;
        }
    }
}