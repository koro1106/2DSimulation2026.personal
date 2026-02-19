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

    public TextMeshProUGUI speakerText;     // 話者表示用
    public TextMeshProUGUI mainText;        // 本文表示用
    public TextMeshProUGUI selectAText;     // 選択肢A用
    public TextMeshProUGUI selectBText;     // 選択肢B用
    public int currentID = 0;              // 今表示しているID

    public GameObject buttonA;
    public GameObject buttonB;
    public int CurrentID => currentID; // currentIDを外から読めるようにする
    void Start()
    {
        ShowDialogue();
        ShowSelectA();
        ShowSelectB();
    }

    // メインテキスト
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
        mainText.text = data.text;

        typewriter.StartTyping(data.text);
    }

    // 選択肢Aテキスト
    public void ShowSelectA()
    {
        if (!loader.dialogueDict.ContainsKey(currentID))
        {
            Debug.Log("IDが存在しない：" + currentID);
            return;
        }

        TutorialData data = loader.dialogueDict[currentID];

        // ボタン表示
        if (data.setFlag.Trim() == "onButton") // Trim()は文字列前後の空白や改行を削除してくれる
        {
            buttonA.SetActive(true);
        }
        else if (data.setFlag.Trim() == "offButton")
            buttonA.SetActive(false);

        // ButtonUIに代入
        selectAText.text = data.choiceA;
    }
    public void ShowSelectB()
    {
        if (!loader.dialogueDict.ContainsKey(currentID))
        {
            Debug.Log("IDが存在しない：" + currentID);
            return;
        }

        TutorialData data = loader.dialogueDict[currentID];

        // ボタン表示
        if (data.setFlag.Trim() == "onButton")
            buttonB.SetActive(true);
        else if(data.setFlag.Trim() == "offButton")
            buttonB.SetActive(false);

        // ButtonUIに代入
        selectBText.text = data.choiceB;
    }

    // 次に進む用
    public void Next()
    {
        TutorialData data = loader.dialogueDict[currentID];

        currentID = data.nextID;

        ShowDialogue();
        ShowSelectA();
        ShowSelectB();
    }
}
