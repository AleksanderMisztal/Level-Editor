using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    private void Start()
    {
        Sprite background = Resources.Load<Sprite>(LevelConfiguration.background);
        GetComponent<Image>().sprite = background;
    }
}