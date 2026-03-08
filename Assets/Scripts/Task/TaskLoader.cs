using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// タスクCSV読み込みクラス
/// CSVの内容をDictionaryに格納する
/// </summary>
public class TaskLoader : MonoBehaviour
{
    // IDをキーにしてタスクデータを保存する
    public Dictionary<int, TaskData> taskDict = new Dictionary<int, TaskData>();

    void Awake()
    {
        // 開始時にCSV読み込み
        LoadTaskCSV();
    }

    void LoadTaskCSV()
    {
        // Resourcesフォルダからcsv読み込む
        TextAsset csv = Resources.Load<TextAsset>("TaskData");

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
            TaskData data = new TaskData();

            // 各列の値を代入　int.TryParse()で文字を数字に変換できて空の可能性があるのでこれ使う
            int.TryParse(columns[0], out data.stageID);     // ステージID
            int.TryParse(columns[1], out data.taskID);      // タスクID
            data.text = columns[2];                         // テキスト
            data.type = columns[3];                         // オブジェクトのタイプ（タグ名）
            int.TryParse(columns[4], out data.count);       // オブジェクト必要数
            int.TryParse(columns[5], out data.reward);      // 報酬

            // Dictionaryに追加（stageIDをキーにする）
            taskDict.Add(data.stageID, data);
        }
        // 読み込み完了ログ
        Debug.Log("CSV読み込み完了:" + taskDict.Count);
    }
}
