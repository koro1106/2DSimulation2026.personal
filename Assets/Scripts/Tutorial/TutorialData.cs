using UnityEngine;
/// <summary>
/// チュートリアル1行分のデータを保持するクラス
/// </summary>
public class TutorialData
{
   public int ID;
    public string speaker; // 話者
    public string text;
    public int nextID;
    public string type;
    public string choiceA;
    public int nextIDA;
    public string choiceB;
    public int nextIDB;
    public string checkFlag; // この行を表示する条件
    public string setFlag;   // この行を通ったらONにする
}
