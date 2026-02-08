using System.Collections;
using UnityEngine;
/// <summary>
///  UIウィンドウのスライド＋(フェードアニメーション)
/// </summary>
public class WindowAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform window; // 自分自身（親）
    [SerializeField] public CanvasGroup canvasGroup;
    [SerializeField] private Vector2 targetPos; // 目標位置
    [SerializeField] private float moveTime = 0.5f;
    [SerializeField] private bool  useFade = true; // フェード使うか

    public System.Action<WindowAnimation> OnClosed; // 閉じた通知
    public bool onWindow = false; // ウィンドウ表示されてるか
    private bool isMoving = false; // 連打防止 
    Vector2 offScreenPos;
    void Awake()
    {
        // 最初に置いてある位置を画面外として保存する
        offScreenPos = window.anchoredPosition; 

        if(useFade)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
    public void OpenWindow()
    {
        if (isMoving || onWindow) return;
        StartCoroutine(Opne());
    }
    public void CloseWindow()
    {
        if (isMoving || !onWindow) return;
        StartCoroutine(Close());
    }
    public void Toggle() // 表示・非表示切り替え
    {
        if (isMoving) return;

        // ウィンドウあいてたらほか閉じる
        if (onWindow) CloseWindow();
        else OpenWindow();
    }

    // 右から左に
   public IEnumerator Opne()
    {
        isMoving = true;

        // 今いる位置(右の画面外)とゴール位置
        Vector2 startPos = offScreenPos;
        Vector2 endPos = targetPos;

       if(useFade)
       {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
       }
      
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / moveTime;
            window.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            if(useFade)
              canvasGroup.alpha = Mathf.Lerp(0f, 1f, t); // 透明度１に
            yield return null;
        }

        window.anchoredPosition = endPos;

        if(useFade)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        onWindow = true;  // ウィンドウ開いてる
        isMoving = false;
   }

    // 左から右に
    public IEnumerator Close()
    {
        isMoving = true;

        // 今いる位置(画面内)と ゴール位置(画面外)
        Vector2 startPos = window.anchoredPosition;
        Vector2 endPos = offScreenPos;

        if(useFade)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / moveTime;
            window.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            if(useFade)
              canvasGroup.alpha = Mathf.Lerp(1f, 0f, t); // 透明度０に
            yield return null;
        }

        window.anchoredPosition = endPos;
        if(useFade) 
             canvasGroup.alpha = 0f;

        onWindow = false; // ウィンドウ閉じてる
        isMoving = false;

        OnClosed?.Invoke(this); // 素材ウィンドウ閉じた通知
    }
}
