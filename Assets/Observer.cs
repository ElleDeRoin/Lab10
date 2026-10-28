using UnityEngine;

public class Observer : MonoBehaviour
{
    [SerializeField] private int pointValue = 100;
    private ScoreManager _scoreManager;

    //subscribe to score manager events
    public void Setup(ScoreManager scoreManager)
    {
        _scoreManager = scoreManager;
    }

    //kill enemy and add score when it takes damage
    public void TakeDamage(int damage)
    {
        Die();
    }
 
    //death logic
    private void Die()
    {
        _scoreManager.AddScore(pointValue);

        Destroy(gameObject);
    }
}
