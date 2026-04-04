using System.Globalization;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float _elapsedTime;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        timerText.text = _elapsedTime.ToString(CultureInfo.InvariantCulture);
    }
}
