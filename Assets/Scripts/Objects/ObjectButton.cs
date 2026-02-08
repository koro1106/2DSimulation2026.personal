using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 素材ボタン専用
/// </summary>

public class ObjectButton : MonoBehaviour
{
    [SerializeField] private ObjectData data;
    [SerializeField] private PlacementCheck placementCheck;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        placementCheck.SetPrefab(data.prefab); // Prefabセットする
    }
}
