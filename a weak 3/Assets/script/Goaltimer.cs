using UnityEngine;
using UnityEngine.UI;

public class Goaltimer : MonoBehaviour
{
    public Text timerText;

    void Start()
    {
        float finalTime = PlayerPrefs.GetFloat("FinalTime");

        int minutes = Mathf.FloorToInt(finalTime / 60f);
        int seconds = Mathf.FloorToInt(finalTime % 60f);
        string hexMinutes = minutes.ToString("X2");
        string hexSeconds = seconds.ToString("X2");

        timerText.text = hexMinutes + ":" + hexSeconds;
    }
}