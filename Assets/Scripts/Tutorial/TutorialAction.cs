using System.Collections;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// チュートリアルの動作クラス
/// </summary>
public class TutorialAction : MonoBehaviour
{
    public TutorialLoader loader; //csv読み込みクラス
    public TutorialViewer viewer;
    public Typewriter typewriter;

    public Image bgImage;
    public Image lineImage;
    [SerializeField]float duration = 1.5f; // フェード時間

    private bool isBGFadeing = false;
    private bool isLineFadeing = false;
    void Update()
    {
        int id = viewer.CurrentID;
        TutorialData data = loader.dialogueDict[id];

        // 背景出現
        if (!isBGFadeing && data.setFlag.Trim() == "onBG")
        {
            StartCoroutine(FadeInBG());
            Debug.Log("onBG");
        }
        // 縁出現
        if (!isLineFadeing && data.setFlag.Trim() == "onLine")
        {
            StartCoroutine(FadeInLine());
        }

    }


    // 背景
    IEnumerator FadeInBG() 
  　{
        isBGFadeing = true;
        float time = 0f;

        Color color = bgImage.color;

        while(time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, time / duration);

            color.a = alpha;
            bgImage.color = color;

            yield return null;
        }
　  }
    // 縁
    IEnumerator FadeInLine() 
  　{
        isLineFadeing = true;
        float time = 0f;

        Color color = lineImage.color;

        while(time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, time / duration);

            color.a = alpha;
            lineImage.color = color;

            yield return null;
        }
　  }
}
