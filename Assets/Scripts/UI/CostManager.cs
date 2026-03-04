using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// コストマネージャー
/// </summary>
public class CostManager : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public void AddCost(float value)
    {
        slider.value += value;
    }

    public void UseCost(float value)
    {
        slider.value -= value;
    }
}
