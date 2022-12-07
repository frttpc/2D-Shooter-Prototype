using UnityEngine;
using UnityEngine.UI;

public class ManaMeter : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private float increaseRate;

    public static ManaMeter Instance;

    private void Start()
    {
        Instance = this;

        slider = GetComponent<Slider>();

        slider.maxValue = Player.Instance.GetMana();
        slider.value = slider.maxValue;
    }

    public void Decrease(int amount)
    {
        if (slider.value - amount >= 0)
            slider.value -= amount;
        else
            slider.value = 0;
    }

    public void Increase()
    {
        slider.value += increaseRate;
        if (slider.value > slider.maxValue)
            slider.value = slider.maxValue;
    }

    public void Increase(float amount)
    {
        slider.value += amount;
        if (slider.value > slider.maxValue)
            slider.value = slider.maxValue;
    }
}
