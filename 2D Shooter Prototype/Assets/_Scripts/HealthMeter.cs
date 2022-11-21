using UnityEngine;

public class HealthMeter : MonoBehaviour
{
    private int _maxValue;
    private int _currentValue;

    private void Start()
    {
        _maxValue = Player.Instance.GetHealth();
        _currentValue = _maxValue;
    }


}
