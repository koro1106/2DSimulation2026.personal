using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 配置判定クラス
/// </summary>
public class PlacementCheck: MonoBehaviour
{
    [SerializeField] Image mapImage; // 設置可能エリアになる地図の画像  　    
    private GameObject objPrefab; // Prefub
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject effectPrefab; // エフェクト

    Texture2D mapTexture; // 地図画像のTexture(a値判定用)

    void Start()
    {
        // mapImageに設定されているSpriteからTextureを取得
        // GetPixel() を使うためRead/Write Enabled が必要
        mapTexture = mapImage.sprite.texture;

        objPrefab = null; // 最初どのボタンも押されてなければ設定しない
    }

    void Update()   
    {
        if(Input.GetMouseButtonDown(0)) // 左クリック
        {
            // クリックしたスクリーン座標を取得
            Vector2 screenPos = Input.mousePosition;

            // クリック位置が設置可能か判定
            if (CanPlace(screenPos))
            {
                PlaceObject(screenPos);
            }
            // 範囲外なら何もしない
        }
    }

    // Prefab を外から変更する
    public void SetPrefab(GameObject prefab)
    {
        objPrefab = prefab;
    }
    // クリック位置が地図のおける場所かどうか判定する
    bool CanPlace(Vector2 screenPos)
    {
        // 地図ImageのRectTransformを取得
        RectTransform rect = mapImage.rectTransform;

        // スクリーン座標→RectTransform内のローカル座標に変換
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rect, screenPos, canvas.worldCamera, out localPos);

        // 地図のRect(UI上の範囲)
        Rect r = rect.rect;

        // 地図の四角範囲外なら即NGに
        if (!r.Contains(localPos)) return false;

        // ローカル座標を０～１の割合に変換
        float x = (localPos.x - r.x) / r.width;
        float y = (localPos.y - r.y) / r.height;

        // 地図のTextureを取得
        Texture2D tex = mapImage.sprite.texture;

        // 割合→テクスチャのピクセル座標に変換
        int px = Mathf.FloorToInt(x * tex.width);
        int py  = Mathf.FloorToInt(y * tex.height);

        // そのピクセルの色を取得
        Color c = tex.GetPixel(px, py);

        // a値があるなら（不透明）なら置ける
        // 不透明部分（海など）なら置けない
        return c.a > 0.1f;
    }

    // クリック位置に創造物を配置する
    void PlaceObject(Vector2 screenPos)
    {
        // Prefabを地図の子として生成
        GameObject tree = Instantiate(objPrefab, mapImage.transform);

        // PrefabのRectTransform取得
        RectTransform rt  = tree.GetComponent<RectTransform>();
        
        // スクリーン座標→地図内ローカル座標に変換
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
           mapImage.rectTransform, screenPos, canvas.worldCamera, out localPos);

        // UI座標としてその位置に配置
        rt.anchoredPosition = localPos;

        // エフェクト生成
        if (effectPrefab != null)
        {
            GameObject effect = Instantiate(effectPrefab, mapImage.transform);

            RectTransform effectRt = effect.GetComponent<RectTransform>();
            effectRt.anchoredPosition = localPos;
        }
    }
}
