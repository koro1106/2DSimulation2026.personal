using System.Collections;
using UnityEngine;
/// <summary>
///  UIウィンドウフェードアニメーション
/// </summary>
public class WindowAnimation : MonoBehaviour
{
    [SerializeField] public CanvasGroup canvasGroup;
    [SerializeField] private float fadeTime = 0.3f;

    public System.Action<WindowAnimation> OnClosed; // 閉じた通知
    public bool onWindow = false; // ウィンドウ表示されてるか
    private bool isMoving = false; // 連打防止 
    void Awake()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    public void OpenWindow()
    {
        if (isMoving || onWindow) return;
        StartCoroutine(Fade(0f, 1f)); // 開いた
    }
    public void CloseWindow()
    {
        if (isMoving || !onWindow) return;
        StartCoroutine(Fade(1f, 0f)); // 閉じた
    }

    // 右から左に
   public IEnumerator Fade(float start, float end)
   {
      isMoving = true;
      float t = 0f;

      // 開くときだけ先に操作不可にする
      if(end == 1f)
      {
          canvasGroup.interactable = false;
          canvasGroup.blocksRaycasts = false;
      }
    
      while (t < 1f)
      {
          t += Time.deltaTime / fadeTime;
          canvasGroup.alpha = Mathf.Lerp(start, end, t);
          yield return null;
      }

        canvasGroup.alpha = end;

        // フェードが終わったあと、このウィンドウは開いてる状態かどうか
        // end = 1：開いた　end = 0: 閉じた
        bool isOpen = end == 1f;
      
        canvasGroup.interactable = isOpen;
        canvasGroup.blocksRaycasts = isOpen;

        onWindow = isOpen;
        isMoving = false;

        // ウィンドウ閉じたら
        if (!isOpen)
            OnClosed?.Invoke(this); // OnClosed に登録されている処理があれば実行する
   }
}
