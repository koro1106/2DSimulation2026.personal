using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 素材ボタン以外のボタンのマネージャー
/// </summary>
public class ButtonManager : MonoBehaviour
{
    [SerializeField] private WindowAnimation[] windows;
    [SerializeField] private WindowAnimation objectWindows;
    [SerializeField] private CanvasGroup objCanvasGroup;

    [SerializeField] private GameObject option;
    public bool openOption = false;
    public void OnObjectsButton() // 創造物ウィンドウ
    {
        if(objectWindows.onWindow) // ウィンドウ開いてたら
        {
            // 創造物のボタン全部アルファ0にする
            objectWindows.CloseWindow();

            foreach(var w in windows)
            {
                w.CloseWindow();
            }

            SetCanvasGroup(objCanvasGroup, true); // 非表示
        }
        else
        {
            // ウィンドウ閉じてたら創造物のボタン全部アルファ１にする
            objectWindows.OpenWindow();
            SetCanvasGroup(objCanvasGroup, true); // 表示
            OpenOnly(windows[0]); // 木だけ表示用
        }
    }

    // キャンバスグループON・OFFセットする関数
    void SetCanvasGroup(CanvasGroup cg, bool visible)
    {
        cg.alpha = visible ? 1f : 0f;
        cg.interactable = visible;   // interactableをfalseにしないとボタン押せちゃう
        cg.blocksRaycasts = visible; // blocksRaycastsをfalseにしないとクリック判定残る
    }
    public void OnTreeButton() // 木ボタン
    {
        OpenOnly(windows[0]);
    }
    public void OnHomeButton() // 家ウィンドウ
    {
        OpenOnly(windows[1]);
    }
    public void OnMountainButton() // 山ウィンドウ
    {
        OpenOnly(windows[2]);
    }
    public void OnOtherButton() // その他ウィンドウ
    {
        OpenOnly(windows[3]);
    }

    void OpenOnly(WindowAnimation target) //自分以外を全部閉じる関数(alpha0に)
    {
        foreach(var w in windows)
        {
            if (w == target)
                w.OpenWindow();
            else
                w.CloseWindow();
        }
    }

    public void OnOptionButton() // オプション表示
    {
        openOption = true;
        option.SetActive(true);
    }
    public void OffOptionButton() // オプション非表示
    {
        openOption = false;

        option.SetActive(false);
    }

    public void StartGame() // スタート
    {
        if(!openOption)
            SceneManager.LoadScene("TutorialScene");
    }
}