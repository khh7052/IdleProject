using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private void Awake()
    {
        fillImage ??= GetComponent<Image>();
    }

    public void SetFillAmount(float current, float max)
    {
        fillImage.fillAmount = current / max;
    }
}
