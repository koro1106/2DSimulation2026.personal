/// <summary>
/// タスク1行分のデータを保持するクラス
/// </summary>

public class TaskData 
{
    public int stageID;
    public int taskID;
    public string text;
    public string type; // オブジェクトのタイプ（タグ名）
    public int count;   // オブジェクト必要数
    public int reward;  // 報酬
}
