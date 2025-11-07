using UnityEngine;

public class t : MonoBehaviour
{
    public static float finalTime = 0f;
    private float elapsedTime = 0f;
    private bool isRunning = true;

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // シーンをまたいでも残す
    }

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    public void StopTimer()
    {
        isRunning = false;
        finalTime = elapsedTime; // タイムを保存
        UpdateTimerDisplay();

        PlayerPrefs.SetFloat("FinalTime", elapsedTime); // 次のシーン用


    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        string hexMinutes = minutes.ToString("X2");
        string hexSeconds = seconds.ToString("X2");
        GetComponent<UnityEngine.UI.Text>().text = hexMinutes + ":" + hexSeconds;
    }
}