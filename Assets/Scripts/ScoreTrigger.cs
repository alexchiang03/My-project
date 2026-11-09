using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private bool scored = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger geraakt door: " + other.name);

        if (other.CompareTag("Player") && !scored)
        {
            scored = true;

            Debug.Log("Score +1");

            ScoreManager.Instance.AddScore();
            AudioManager.Instance.PlayPoint();
        }
    }
}