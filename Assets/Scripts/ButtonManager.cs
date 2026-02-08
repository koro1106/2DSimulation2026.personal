using UnityEngine;
/// <summary>
/// 素材ボタン以外のボタンのマネージャー
/// </summary>
public class ButtonManager : MonoBehaviour
{
    [SerializeField] private WindowAnimation[] windows;
    [SerializeField] private WindowAnimation objectWindows;

    public void OnObjectsButton() // 素材ウィンドウ
    {
        if(objectWindows.onWindow) // ウィンドウ開いてたら
        {
            // 素材ウィンドウ閉じたら全部閉じる
            objectWindows.CloseWindow();

            foreach(var w in windows)
            {
                w.CloseWindow();
            }
        }
        else
        {
            // ウィンドウ閉じてるときにボタン押したら開く
            objectWindows.OpenWindow(); 
        }
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
}