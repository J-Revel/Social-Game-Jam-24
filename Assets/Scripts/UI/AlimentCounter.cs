using System;
using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class AlimentCounter : MonoBehaviour
{
    [SerializeField] AnimationConfig animationConfig;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI counterTMPComponent;
    public void Set(int count, bool animated = false)
    {
        this.StopAllCoroutines();
        counterTMPComponent.text = count.ToString();

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
