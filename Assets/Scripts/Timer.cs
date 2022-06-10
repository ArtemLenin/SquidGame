using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int Seconds = 180;
    public Text TimerPanel;

    private void Start()
    {
        InvokeRepeating(nameof(Tick), 0, 1);
    }

    private void Tick()
    {
        Seconds -= 1;

        string minutes = (Seconds / 60).ToString();
        string seconds = (Seconds % 60).ToString();

        if (minutes.Length == 1) minutes = "0" + minutes;
        if (seconds.Length == 1) seconds = "0" + seconds;

        TimerPanel.text = $"{minutes}:{seconds}";

        if (Seconds == 0)
        {
            TimerPanel.color = Color.red; 
            CancelInvoke(nameof(Tick));
            TimeOut();
        }
    }

    private void TimeOut()
    {
        GameManager.Instance.GameOver();
    }
}
