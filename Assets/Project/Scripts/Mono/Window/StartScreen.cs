
using Client;
using DG.Tweening;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : Screen
{
    [SerializeField] Image blackout;
    [SerializeField] float blackoutDuration = 1f;
    [SerializeField] Ease blackoutEase;

    private void OnEnable()
    {
        blackout.DOFade(0f, blackoutDuration)
                .SetEase(blackoutEase)
                .OnComplete(() => Service<EcsWorld>.Get().ChangeState(GameState.NONE));
    }

    private void OnDisable()
    {
        Color color = blackout.color;
        color.a = 1f;
        blackout.color = color;
    }
}
