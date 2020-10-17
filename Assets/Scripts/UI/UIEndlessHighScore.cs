using TMPro;
using UnityEngine;

public class UIEndlessHighScore : MonoBehaviour
{
    private void Start() => GetComponent<TextMeshProUGUI>().text = $"High Score : {PlayerPrefs.GetFloat("EndlessRunHighScore", 0)} AU";
}
