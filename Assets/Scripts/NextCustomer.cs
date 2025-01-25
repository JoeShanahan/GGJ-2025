using UnityEngine;

public class NextCustomerHandler : MonoBehaviour
{
    [Header("References")]
    public GameState gameState;
    public float score;

    public void NextCustomer()
    {
        if (gameState == null)
        {
            Debug.LogError("GameState reference is missing.");
            return;
        }

        if (score >= 0 && score < 50)
        {
            gameState.CurrentHorrorLevel = GameState.HorrorLevel.Normal;
        }
        else if (score >= 50 && score < 100)
        {
            gameState.CurrentHorrorLevel = GameState.HorrorLevel.Unsettling;
        }
        else if (score >= 100 && score <= 150)
        {
            gameState.CurrentHorrorLevel = GameState.HorrorLevel.FullHorror;
        }
        else
        {
            Debug.LogWarning("Score is out of range. No changes to the horror level.");
        }

        Debug.Log($"Score: {score}, Horror Level: {gameState.CurrentHorrorLevel}");
    }

    public void IncreaseScore()
    {
        score += 25;
        Debug.Log($"Score increased. New score: {score}");
    }

    public void DecreaseScore()
    {
        score -= 25;
        Debug.Log($"Score decreased. New score: {score}");
    }
}