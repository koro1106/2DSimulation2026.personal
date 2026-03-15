using UnityEngine;

/// <summary>
/// オプションボタンの動作クラス
/// </summary>
public class OptionMove : MonoBehaviour
{
    private RectTransform rect;
    private RectTransform canvasRect;

    Vector2 dir; // 間隔
    public float speed = 200f; // 移動スピード
    void Start()
    {
        rect = GetComponent<RectTransform>();
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        dir = new Vector2(1, 1).normalized; // 斜め移動
    }

    void Update()
    {
        // ボタンを現在の方向に移動させる
        // dir * sped * time.deltaTime
        rect.anchoredPosition += dir * speed * Time.deltaTime;

        // 現在のボタン位置を取得
        Vector2 pos = rect.anchoredPosition;

        // Canvasの横幅・立幅の半分（中心基準で判定するから）
        float canvasWidth = canvasRect.rect.width / 2;
        float canvasHeight = canvasRect.rect.height / 2;

        // ボタンの半分サイズ(端判定用)
        float buttonWidth = rect.rect.width / 2;
        float buttonHeight = rect.rect.height / 2;

        // 左右の画面端にぶつかったか判定
        // pos.x + buttonWidth → 右端
        // pos.x - buttonWidth → 左端
        if (pos.x + buttonWidth > canvasWidth || pos.x - buttonWidth < -canvasWidth)
        {
            dir.x *= -1; // X方向を反転（跳ね返るように）
        }

        // 上下の画面端にぶつかったか判定
        // pos.x + buttonHeight → 上端
        // pos.x - buttonHeight → 下端
        if (pos.y + buttonHeight > canvasHeight || pos.y - buttonHeight < -canvasHeight)
        {
            dir.y *= -1; // Y方向を反転（跳ね返るように）
        }
    }
}
