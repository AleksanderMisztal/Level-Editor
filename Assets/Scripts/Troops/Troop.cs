using UnityEngine;

namespace Troops
{
    public class Troop : MonoBehaviour
    {
        [SerializeField] private TroopTemplate template;

        private void Start()
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.sprite = template.Sprite;
            renderer.size *= 10f;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                transform.position = Initializer.GetHexCenterWp();
            }
        }
    }
}