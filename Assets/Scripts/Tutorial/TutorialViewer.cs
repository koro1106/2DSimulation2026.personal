using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// チュートリアルテキスト表示用
/// </summary>
public class TutorialViewer : MonoBehaviour
{
    public TutorialLoader loader; //csv読み込みクラス
    public Typewriter typewriter; //タイプライタークラス

    public TextMeshProUGUI speakerText;        // 話者表示用
    public TextMeshProUGUI mainText;        // 本文表示用
    int currentID = 0;              // 今表示しているID
    void Start()
    {
        ShowDialogue();
    }

    public void ShowDialogue()
    {
        // Dictionaryからデータを取得
        if (!loader.dialogueDict.ContainsKey(currentID))
        {
            Debug.Log("IDが存在しない：" + currentID);
            return;
        }

        TutorialData data  = loader.dialogueDict[currentID];

        // UIに代入
        speakerText.text = data.speaker;
        //mainText.text = data.text;

        typewriter.StartTyping(data.text);
    }

    // 次に進む用
    public void Next()
    {
        TutorialData data = loader.dialogueDict[currentID];

        currentID = data.nextID;

        ShowDialogue();
    }
}
