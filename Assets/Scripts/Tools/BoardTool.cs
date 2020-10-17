using System;
using GameDataStructures.Positioning;
using LevelEditor.Saving;
using LevelEditor.Tilemaps;
using UnityEngine;

namespace LevelEditor.Tools
{
    public class BoardTool : DesignTool
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private float moveSpeed;
        [SerializeField] private BackgroundManager backgroundManager;
        public static Action<VectorTwo> resizeCallback;


        public override void Initialize(GridBase _)
        {
            
        }

        public override void Resize()
        {
            
        }

        private void Update()
        {
            if (!Enabled) return;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                ResizeGrid();
            }
            else
            {
                AdjustCameraPosition();
                AdjustCameraSize();
            }
        }

        private void AdjustCameraPosition()
        {
            Vector3 dp = Vector3.zero;
            if (Input.GetKey(KeyCode.DownArrow)) dp -= Vector3.down;
            if (Input.GetKey(KeyCode.UpArrow)) dp -= Vector3.up;
            if (Input.GetKey(KeyCode.LeftArrow)) dp -= Vector3.left;
            if (Input.GetKey(KeyCode.RightArrow)) dp -= Vector3.right;
            camera.transform.position += dp * moveSpeed;
        }
        private static void ResizeGrid()
        {
            int dx = 0;
            int dy = 0;
            if (Input.GetKeyDown(KeyCode.DownArrow)) dy--;
            if (Input.GetKeyDown(KeyCode.UpArrow)) dy++;
            if (Input.GetKeyDown(KeyCode.LeftArrow)) dx--;
            if (Input.GetKeyDown(KeyCode.RightArrow)) dx++;

            if (dx == 0 && dy == 0) return;
            
            resizeCallback(new VectorTwo(dx, dy));
        }

        private void AdjustCameraSize()
        {
            if (Input.GetKey(KeyCode.Minus)) camera.orthographicSize += moveSpeed;
            if (Input.GetKey(KeyCode.Equals)) camera.orthographicSize -= moveSpeed;
        }

        public override void Save()
        {
            BoardDto dto = new BoardDto()
            {
                background = LevelConfiguration.background, 
                offset = camera.transform.position,
                cameraSize = camera.orthographicSize
            };
            Saver.Save(LevelConfiguration.name + "/board", dto);
        }

        public override void Load()
        {
            BoardDto dto = Saver.Read<BoardDto>(LevelConfiguration.name + "/board");
            camera.orthographicSize = dto.cameraSize;
            camera.transform.position = dto.offset;
            backgroundManager.SetBackground(dto.background);
        }
    }
}