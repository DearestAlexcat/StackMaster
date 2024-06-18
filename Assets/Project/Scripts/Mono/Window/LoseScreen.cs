using Client;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoseScreen : Screen
{
    [SerializeField] Image whiteBrush;
    [SerializeField] Button buttonForward;
    [Space]
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeDuration = 1f;
    [SerializeField] Ease fadeEase;
    [Space]
    [SerializeField] Image blackout;
    [SerializeField] float blackoutDuration = 1f;
    [SerializeField] Ease blackoutEase;

    private void OnEnable()
    {
        whiteBrush.DOFillAmount(1f, fadeDuration).SetEase(fadeEase).OnComplete(() => buttonForward.enabled = true);
        canvasGroup.DOFade(1f, fadeDuration).SetEase(fadeEase);
    }

    private void OnDisable()
    {
        canvasGroup.alpha = 0f;
        whiteBrush.fillAmount = 0f;

        Color color = blackout.color;
        color.a = 0f;
        blackout.color = color;
    }

    private void Start()
    {
        buttonForward.onClick.AddListener(() =>
        {
            buttonForward.enabled = false;
            blackout.DOFade(1f, blackoutDuration).SetEase(blackoutEase).OnComplete(() => Levels.LoadCurrent());
        });
    }
}
