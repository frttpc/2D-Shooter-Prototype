using UnityEngine;
using UnityEngine.UI;

public class HealthMeter : MonoBehaviour
{
    private Slider slider;

    public static HealthMeter Instance;

    private void Start()
    {
        Instance = this;

        slider = GetComponent<Slider>();

        slider.maxValue = Player.Instance.GetHealth();
        slider.value = slider.maxValue;
    }

    public void Decrease(int amount)
    {
        if (slider.value - amount >= 0)
            slider.value -= amount;
        else
            slider.value = 0;
    }

}
