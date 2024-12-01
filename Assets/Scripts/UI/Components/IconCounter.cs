using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconCounter : MonoBehaviour
{
    [SerializeField] AnimationConfig animationConfig;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI counterTMPComponent;
    [SerializeField] float ratio = 1;
    [SerializeField] string format = "0";
    public void Set(int count, bool animated = false)
    {
        this.StopAllCoroutines();
        counterTMPComponent.text = ((float)count * ratio).ToString(format);

        if(animated)
        {
            this.StartCoroutine(AnimateBump());
        }
        else
        {
            this.ResetAnimation();
        }
    }

    private void ResetAnimation()
    {
        counterTMPComponent.transform.localScale = Vector3.one;
    }

    private IEnumerator AnimateBump()
    {
        return AnimationUtils.DoCoroutine((value) => counterTMPComponent.transform.localScale = Mathf.Lerp(animationConfig.Intensity, 1f, value) * Vector3.one , animationConfig);
    }
}
