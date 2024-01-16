using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImage;
    public Image powerImage;

    public float healthDelaySpeed = 1;

    public void OnHealthChange(float percent)
    {
        StartCoroutine(HealthBarUpdateWithDelay(percent));
    }

    public void ResetStatus()
    {
        healthImage.fillAmount = 1;
        healthDelayImage.fillAmount = 1;
        powerImage.fillAmount = 1;
    }

    IEnumerator HealthBarUpdateWithDelay(float percent)
    {
        healthImage.fillAmount = percent;
        healthDelayImage.fillAmount = Mathf.Max(healthDelayImage.fillAmount, percent);

        while (healthDelayImage.fillAmount > healthImage.fillAmount)
        {
            healthDelayImage.fillAmount -= healthDelaySpeed * Time.deltaTime;
            yield return null;
        }
    }
}
