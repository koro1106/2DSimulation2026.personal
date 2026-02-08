using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using UnityEngine.UI;  
/// <summary>
/// カーソルに追従する目
/// </summary>
public class FollowingEye : MonoBehaviour
{
    RectTransform rect;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float maxOffsetX = 10f; // 左右の上限

    private float baseX; // 初期位置
    void Start()
    {
        rect = GetComponent<RectTransform>();
        baseX = rect.anchoredPosition.x;
    }

    void Update()
    {
        // マウスの位置取得(画面の左下が0,0)
        Vector2 mousePos = Input.mousePosition;

        // 自分のImgeの中心座標が取れる
        Vector2 myScreenPos = RectTransformUtility.WorldToScreenPoint(null, rect.position);

        float targetX = rect.anchoredPosition.x;

        if (mousePos.x > myScreenPos.x)
            targetX += moveSpeed;
        else
            targetX -= moveSpeed;

        // 上限を制限    
        targetX = Mathf.Clamp(targetX, baseX - maxOffsetX, baseX + maxOffsetX);
        
        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition,
                                                              new Vector2(targetX, rect.anchoredPosition.y),
                                                              Time.deltaTime * 5f);
    }
}
