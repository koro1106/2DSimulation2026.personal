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
    [SerializeField] private TutorialViewer viewer;
    [SerializeField] private TutorialLoader loader;
    private bool isTyping = false; // 一文字ずつ表示中かどうか
    public bool isTypingEnd = false; // タイピング終わったかかどうか
    private string currentMessage;
    Coroutine typingCoroutine;

    private void Update()
    {
        int id = viewer.CurrentID;
        TutorialData data = loader.dialogueDict[id];


        if (data.type.Trim() == "Normal" && Input.GetMouseButtonDown(0)) // 選択肢無いのだけ左クリック可能
        {
           OnClick();
        }

        if (data.nextID == 100) return;
    }
    public void StartTyping(string message)
    {
        // 前のコルーチンがあれば止める
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        isTypingEnd = false;
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
        isTypingEnd = true;    // タイピング終了
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
            // 最後まで表示
            viewer.Next(); // チュートリアル次に進める
        }
    }
}
