using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public int damage = 1;
    public bool shouldPlayerBeMoved = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);

                if (shouldPlayerBeMoved)
                {
                    // Przywrócenie gracza do pozycji obok dziury
                    Vector3 newPosition = other.transform.position;
                    Vector3 holePosition = transform.position;

                    // Przesuwanie gracza w kierunku z którego przyszed³
                    if (holePosition.x < newPosition.x)
                    {
                        newPosition.x += 0.3f;
                    }
                    else if (holePosition.x > newPosition.x)
                    {
                        newPosition.x -= 0.3f;
                    }

                    if (holePosition.y < newPosition.y)
                    {
                        newPosition.y += 0.3f;
                    }
                    else if (holePosition.y > newPosition.y)
                    {
                        newPosition.y -= 0.3f;
                    }

                    other.transform.position = newPosition;
                }
            }
        }
    }
}
