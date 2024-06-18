using UnityEngine;

namespace Client
{
    struct PlayerInputComponent
    {
        public float OffsetX;
    }

    struct PopUpRequest
    {
        public string TextUP;
        public Vector3 SpawnPosition;
        public Quaternion SpawnRotation;
        public Transform Parent;
    }

    struct CameraShakeReguest { }

    struct FallingRequest { }

    struct CubeStackFXRequest 
    {
        public Vector3 Position;
    }

    public struct SpawnTrackSegmentRequest
    {
        public int Number;
        public bool IsUpper;
    }

    public struct ExecutionDelay
    {
        public float time;
        public System.Action action;
    }

    struct ChangeStateEvent
    {
        public GameState NewGameState;
    }
}