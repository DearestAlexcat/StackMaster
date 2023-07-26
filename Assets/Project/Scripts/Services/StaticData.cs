using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu]
    public class StaticData : ScriptableObject
    {
        [Header("REQUIRED PREFABS")]
        public UI UI;
        public PopUpText PlusOne;
        public List<TrackView> TrackViews;
        public GameObject CubeStackEffect;

        [Header("CAMERA")]
        public float hFOV = 30f;
        public float orthoSize = 10f;
        public Vector2 DefaultResolution = new Vector2(1920, 1080);
        public Vector3 cameraOffset;
        public Vector3 cameraRotation;
        public float cameraSmoothness;

        [Header("CAMERA SHAKE")]
        public float shakeTime = 0.15f;
        public float shakeAmount = 1.1f;
        public float shakeSpeed = 3f;

        [Header("TRACK")]
        public float spawnSpeed = 1f;
        public Ease spawnEase = Ease.Linear;
        public float border;
        public float intervalToTrackSpawn = 2f;
        public float intervalToTrackDestroy = 2f;

        [Header("PLAYER")]
        public float playerSpeed = 1f;
        public float playerVerticalSpeed = 1f;
        public float deadZone = 0.01f;
        public float yLowerLimit = -20;

        [Header("LEVELS")]
        public Levels ThisLevels;
    }
}
