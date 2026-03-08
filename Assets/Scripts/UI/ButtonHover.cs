using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// ボタンにカーソルを合わせると上に上がる用クラス
/// </summary>
public class ButtonHover: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Vector3 startPos;
    public float moveAmount = 10f; // 上に動く量

    void Start()
    {
        startPos = transform.localPosition;
    }

    // カーソル乗ったら
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localPosition = startPos + new Vector3(0, moveAmount, 0);
        Debug.Log("カーソル乗った");
    }

    // カーソルから離れたら
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localPosition = startPos;
        Debug.Log("カーソルでた");
    }
}
