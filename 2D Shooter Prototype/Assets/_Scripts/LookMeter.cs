using UnityEngine;
using UnityEngine.UI;

public class LookMeter : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private float increaseRate;

    public static LookMeter Instance;

    private void Start()
    {
        Instance = this;

        slider = GetComponent<Slider>();

        slider.maxValue = Player.Instance.GetLook();
        slider.value = slider.maxValue;
    }

    public void Decrease(float amount)
    {
        if (slider.value - amount >= 0)
            slider.value -= amount;
        else
            slider.value = 0;
    }

    public void Increase()
    {
        slider.value += increaseRate;
    }
}
