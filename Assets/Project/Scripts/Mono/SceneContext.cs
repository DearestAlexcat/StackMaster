using UnityEngine;

namespace Client
{ 
    public class SceneContext : MonoBehaviour
    {
        [field: SerializeField] public Player PlayerView { get; private set; }
        [field: SerializeField] public Transform LevelRoot { get; private set; }
    }
}