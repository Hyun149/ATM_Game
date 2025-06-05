using UnityEngine;
using DG.Tweening;

public class AbilityPanelController : MonoBehaviour
{
    [SerializeField] private GameObject statusUI;
    [SerializeField] private float animDuration = 0.3f;

    private bool isOpen = false;

    private void Start()
    {
        statusUI.transform.localScale = Vector3.zero;
        statusUI.SetActive(false);
    }

    public void ToggleStatusUI()
    {
        if (isOpen)
        {
            statusUI.transform.DOScale(Vector3.zero, animDuration).SetEase(Ease.InBack).OnComplete(() =>
            {
                statusUI.SetActive(false);
                isOpen = false;
            });
        }
        else
        {
            statusUI.SetActive(true);
            statusUI.transform.localScale = Vector3.zero;

            statusUI.transform.DOScale(Vector3.one, animDuration).SetEase(Ease.OutBack).OnComplete(() =>
            {
                isOpen = true;
            });
        }
    }
}
