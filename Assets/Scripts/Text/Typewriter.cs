using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// 一文字ずつ表示するクラス
/// </summary>
public class Typewriter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mainText; // 表示するText
    [SerializeField] private float speed = 0.05f;             // 一文字表示する間隔
    [SerializeField] private TutorialViewer tutorialViewer;
    private bool isTyping = false; // 一文字ずつ表示中かどうか
    private string currentMessage;
    Coroutine typingCoroutine;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 左クリック
        {
           OnClick();
        }
    }
    public void StartTyping(string message)
    {
        // 前のコルーチンがあれば止める
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(message));
    }

    IEnumerator TypeText(string message)
    {
        isTyping = true;
        currentMessage = message;
        mainText.text = "";

        foreach (char c in message)
        {
            mainText.text += c;
            yield return new WaitForSeconds(speed);
        }

        isTyping = false;
    }

    public void OnClick() // 表示中なら一瞬で全文表示
    {
        if (isTyping) // タイピング中だったら
        {
            StopAllCoroutines(); // タイピング終了
            mainText.text = currentMessage;
            isTyping = false;
        }
        else
        {
            // 次に進める
            tutorialViewer.Next(); // チュートリアル次に進める
        }
    }
}
