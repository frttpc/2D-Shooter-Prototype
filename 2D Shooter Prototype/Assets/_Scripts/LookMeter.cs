using UnityEngine;

public class Look : MonoBehaviour
{
    private float _maxValue;
    private float _currentValue;

    private void Start()
    {
        _maxValue = Player.Instance.GetLook();
        _currentValue = _maxValue;
    }


}
