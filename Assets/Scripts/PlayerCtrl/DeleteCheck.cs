using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 削除チェッククラス
/// </summary>
public class DeleteCheck : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))　// 右クリック
        {
            TryDelete(Input.mousePosition);
        }
    }

    // 削除できるかどうか
    void TryDelete(Vector2 screenPos)
    {
        //EventSystem用のデータ作成
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = screenPos;

        // Raycast結果格納
        List<RaycastResult> results = new List<RaycastResult>();

        // CanvasのGraphicRayCast取得
        GraphicRaycaster raycaster = canvas.GetComponent<GraphicRaycaster>();
        raycaster.Raycast(pointer, results);

        if (results.Count == 0) return;

        // 一番手前のUI
        GameObject hitUI = results[0].gameObject;

        // タグで判定
        if (hitUI.CompareTag("PlaceObject"))
        {
            Destroy(hitUI);
        }
    }
}
