using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum ScaleAnimationType
{
    SmallToBig,
    BigToSmall
}

public class ButtonAnimator : MonoBehaviour
{
    public ScaleAnimationType animationType;
    public Button button;
    public float duration = 0.5f;
    public Vector3 smallScale = new Vector3(0.8f, 0.8f, 0.8f);
    public Vector3 bigScale = new Vector3(1.2f, 1.2f, 1.2f);

    private void Start()
    {
        if (button == null)
            button = GetComponent<Button>();

        StartContinuousAnimation();
    }

    public void StartContinuousAnimation()
    {
        if (animationType == ScaleAnimationType.SmallToBig)
        {
            button.transform.localScale = smallScale;
            button.transform.DOScale(bigScale, duration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .SetUpdate(true); 
        }
        else if (animationType == ScaleAnimationType.BigToSmall)
        {
            button.transform.localScale = bigScale;
            button.transform.DOScale(smallScale, duration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .SetUpdate(true); 
        }
    }
}
