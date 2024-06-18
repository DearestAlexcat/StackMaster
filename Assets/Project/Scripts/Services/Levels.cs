using UnityEngine;
using UnityEngine.SceneManagement;

namespace Client
{
    [CreateAssetMenu]
    public class Levels : ScriptableObject
    {
        public string[] Scenes;

        public string this[int index]
        {
            get
            {
                return Scenes[index];
            }
        }

        public static void LoadCurrent()
        {
            var level = Progress.CurrentLevel;
            var staticData = Service<StaticData>.Get();

            var totalLevels = staticData.ThisLevels.Scenes.Length;
            var index = level;

            if (level >= totalLevels)
            {
                index = level % totalLevels;
            }

            var levelName = staticData.ThisLevels.Scenes[index];
            SceneManager.LoadSceneAsync(levelName);
        }

        public static void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
