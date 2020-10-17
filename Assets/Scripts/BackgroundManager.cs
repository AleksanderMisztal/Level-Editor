using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    private void Start()
    {
        SetBackground(LevelConfiguration.background);
    }

    public void SetBackground(string imageName)
    {
        LevelConfiguration.background = imageName;
        Sprite background = Resources.Load<Sprite>(imageName);
        GetComponent<Image>().sprite = background;
    }
}