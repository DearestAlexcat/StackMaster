using UnityEngine;

namespace Client
{ 
    public class UI : MonoBehaviour
    {
        [field: SerializeField] public LoseScreen LoseScreen { get; set; }
        [field: SerializeField] public StartScreen StartScreen { get; set; }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void CloseAll()
        {
            LoseScreen.Show(false);
            StartScreen.Show(false);
        }
    }
}