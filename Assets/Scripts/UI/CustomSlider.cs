using UnityEngine;
using UnityEngine.UI;

public abstract class CustomSlider : MonoBehaviour
{
    protected Slider slider;

    protected virtual void Awake()
    {
        slider = GetComponent<Slider>();
    }
}
