using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour
{
    public Slider slider;

    public void SetCooldownMax(float cooldown)
    {
        slider.maxValue = cooldown;
        slider.value = 0;
    }

    public void SetCooldown(float cooldown)
    {
        slider.value = cooldown;
    }
}
