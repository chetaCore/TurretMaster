using DG.Tweening;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    public CanvasGroup Curtain;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Curtain.alpha = 1f;
    }

    public void Hide()
    {
        var hideSeq = DOTween.Sequence();
        hideSeq.Append(Curtain.DOFade(0, 1f)).
            SetEase(Ease.Linear).
            OnComplete(() => hideSeq.Kill());
    }
}