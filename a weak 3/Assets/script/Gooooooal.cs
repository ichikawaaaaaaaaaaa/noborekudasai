using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // プレイヤーに触れたか判定
        {
            Debug.Log("ゴールに触れました！");
            SceneManager.LoadScene("Goal"); // シーン名を指定

            FindObjectOfType<t>().StopTimer();

        }
    }
}