using TMPro;
using UnityEngine;

public class UIScoreDisplay : MonoBehaviour
{
    [SerializeField] private ScoreCalculator _scoreCalculator = null;

    private void OnEnable()
    {
        if (!_scoreCalculator)
            _scoreCalculator = FindObjectOfType<ScoreCalculator>();
        GetComponent<TextMeshProUGUI>().text = _scoreCalculator.CurrentScoreText();
    }
}
