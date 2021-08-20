using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuntajeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private int maxScore = 100;
    private int currentScore = 0;
    private void OnEnable() {
        Flash.OnFlashEvaded += SumScore;
    }

    private void OnDisable() {
        Flash.OnFlashEvaded -= SumScore;
    }

    private void SumScore() {
        if (currentScore >= maxScore) {
            return;
        }

        currentScore++;
        RenderChanges();
    }

    private void RenderChanges() {
        if (currentScore > maxScore) {
            currentScore = maxScore;
        }
        
        textMesh.text = $"{currentScore}/{maxScore}";
    }
}
