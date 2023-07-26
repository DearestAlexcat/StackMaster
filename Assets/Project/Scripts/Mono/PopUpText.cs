using UnityEngine;
using DG.Tweening;
using TMPro;

public class PopUpText : MonoBehaviour
{
    public TMP_Text textUP;

    public float durationUP;
    //public float durationFade;
    public float positionUP;
    public Ease ease;
    public float timeToDestruction;

    private void Start()
    {
        transform.DOLocalMove(transform.localPosition + Vector3.up * positionUP, durationUP).OnComplete(() => Destroy(gameObject, timeToDestruction));
        //textUP.DOFade(0f, durationFade).SetEase(ease);
    }
}
