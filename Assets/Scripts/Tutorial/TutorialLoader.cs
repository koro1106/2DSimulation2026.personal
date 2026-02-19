using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
/// <summary>
/// チュートリアルCSV読み込みクラス
/// CSVの内容をDictionaryに格納する
/// </summary>
public class TutorialLoader : MonoBehaviour
{
    // IDをキーにして会話データを保存する辞書
    public Dictionary<int, TutorialData> dialogueDict = new Dictionary<int, TutorialData>();
    void Awake()
    {
        // ゲーム開始時にCSV読み込む
        LoadCSV();
    }

    void LoadCSV()
    {
        // Resourcesフォルダからcsv読み込む
        TextAsset csv = Resources.Load<TextAsset>("TutorialData");

        // CSV全体を改行で分割して1行ずつ配列にする
        string[] lines = csv.text.Split('\n');

        // 1行目はヘッダーなので　i = 1　からスタート
        for(int i = 1; i < lines.Length; i++)
        {
            // 空行はスキップ
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            // 1行をカンマで分割
            string[] columns = lines[i].Split(',');

            // データクラスを作成
            TutorialData data = new TutorialData();

            // 各列の値を代入　int.TryParse()で文字を数字に変換できて空の可能性があるのでこれ使う
            int.TryParse(columns[0], out data.ID);        // ID
            data.speaker = columns[1];                    // 話者
            data.text = columns[2];                       // テキスト
            int.TryParse(columns[3], out data.nextID);    // 次ID
            data.type = columns[4];                       // タイプ
            data.choiceA = columns[5];                    // 選択肢A
            int.TryParse(columns[6], out data.nextIDA);   // Aの次ID
            data.choiceB = columns[7];                    // 選択肢B
            int.TryParse(columns[8], out data.nextIDB);   // Bの次ID
            data.checkFlag = columns[9];                  // 表示条件
            data.setFlag = columns[10];                   // セットするフラグ

            // Dictionaryに追加（IDにキーにする）
            dialogueDict.Add(data.ID, data);
        }
        // 読み込み完了ログ
        Debug.Log("CSV読み込み完了:" + dialogueDict.Count);
    }
}
