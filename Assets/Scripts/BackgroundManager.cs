using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class BackgroundManager : MonoBehaviour
    {
        private static string GetLocation(string fileName) => Application.dataPath + "/Backgrounds/" + fileName;


        private void Start()
        {
            StartCoroutine(Co_SetBackground(LevelConfig.background));
        }

        public void SetBackground(string imageName)
        {
            StartCoroutine(Co_SetBackground(imageName));
        }

        private IEnumerator Co_SetBackground(string imageName)
        {
            LevelConfig.background = imageName;
            Texture2D texture = new Texture2D(4, 4, TextureFormat.DXT1, false);
            string url = GetLocation(imageName);
            WWW www = new WWW(url);
            yield return www;
            www.LoadImageIntoTexture(texture);
            GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
    }
}