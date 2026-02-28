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
    [SerializeField] private CostManager costManager;

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
        if (hitUI.CompareTag("Tree") || hitUI.CompareTag("House") || hitUI.CompareTag("Mauntain") || hitUI.CompareTag("Other"))
        {
            Destroy(hitUI);
            CheckCost(hitUI);
        }
    }

    // タグ別コスト処理
    void CheckCost(GameObject obj)
    {
        if (obj.CompareTag("Tree"))
        {
            costManager.AddCost(1);
        }
        else if (obj.CompareTag("House"))
        {
            costManager.AddCost(1);
        }
        else if (obj.CompareTag("Mountain"))
        {
            costManager.AddCost(1);
        }
        else if (obj.CompareTag("Other"))
        {
            costManager.AddCost(1);
        }
    }
}
