using UnityEngine;
/// <summary>
/// ▽用（表示・非表示・点滅・入力待ち中）
/// </summary>
public class NextIndicator : MonoBehaviour
{
    [SerializeField] private RectTransform indicator; // ▽
    [SerializeField] private float speed = 0.5f;    // 速さ
    [SerializeField] private float amplitude = 10f; // 上下幅

    Vector2 startPos;
    bool isActive = false;

    void Start()
    {
        startPos = indicator.anchoredPosition;
    }

    void Update()
    {
        if (!isActive) return;

        float y = Mathf.Sin(Time.time * speed) * amplitude; //speed大きいと波が早くなる
        indicator.anchoredPosition = startPos + new Vector2(0, y);
    }

    public void Show()
    {
        startPos = indicator.anchoredPosition; // 位置保存
        isActive = true;
        indicator.gameObject.SetActive(true);
    }
    public void Hide()
    {
        isActive = false;
        indicator.gameObject.SetActive(false);
        indicator.anchoredPosition = startPos;
    }
}
