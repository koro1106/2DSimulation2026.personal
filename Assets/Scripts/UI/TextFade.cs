using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
/// <summary>
/// タイトルテキストのフェード用
/// </summary>
public class ClickTextFade : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] private float speed = 1f; // 点滅スピード

    void Update()
    {
        float alpha = Mathf.PingPong(Time.time * speed, 1f); // Mathf.PingPong(時間,最大値)

        Color c = text.color;
        c.a = alpha;
        text.color = c;
    }
}
