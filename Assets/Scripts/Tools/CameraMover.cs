using UnityEngine;

namespace Tools
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private float moveSpeed;
        

        private void Update()
        {
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
    }
}