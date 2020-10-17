﻿using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LevelEditor.Scenes
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private InputField background;
        [SerializeField] private InputField levelName;
        [SerializeField] private GameObject loadableLevels;
        [SerializeField] private Button loadablePrefab;
        

        public void DisplayLoadableLevels()
        {
            string[] levels = GetDirs();
            foreach (string level in levels)
            {
                string currentLevel = level.Split('/').Last();
                Button button = Instantiate(loadablePrefab, loadableLevels.transform);
                Text text = button.transform.GetChild(0).GetComponent<Text>();
                text.text = currentLevel;
                button.onClick.AddListener(() => LoadLevel(text.text));
            }
        }
        
        private static string[] GetDirs()
        {
            string path = Application.dataPath + "/Saves/";
            try
            {
                return Directory.GetDirectories(path);
            }
            catch
            {
                return new string[0];
            }
        }

        private void LoadLevel(string level)
        {
            LevelConfiguration.name = level;
            LevelConfiguration.isLoaded = true;
            TransitionToEditing();
        }

        public void TransitionToNewLevel()
        {
            LevelConfiguration.name = levelName.text;
            LevelConfiguration.background = background.text;
            TransitionToEditing();
        }

        private void TransitionToEditing()
        {
            string path = Application.dataPath + "/Saves/" + levelName.text;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            else Debug.Log("Will override");
            SceneManager.LoadScene("LevelEditor");
        }
    }
}