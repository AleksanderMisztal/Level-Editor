using Saving;
using UnityEngine;

namespace Tools
{
    public class BoardTool : DesignTool
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private float moveSpeed;
        [SerializeField] private BackgroundManager backgroundManager;


        public override void Initialize()
        {
            
        }

        public override void SetVisibleSize(int x, int y)
        {
            
        }

        private void Update()
        {
            if (!Enabled) return;
            AdjustCameraPosition();
            AdjustCameraSize();
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