using System;
using UnityEngine;

namespace Client
{
    [Serializable]
    public class RuntimeData
    {
        [field: SerializeField] public GameState GameState { get; set; }
        [field: SerializeField] public Vector3 Pivot { get; set; }
    }
}


