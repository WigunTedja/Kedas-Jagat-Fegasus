using UnityEngine;

public class ObstFlood : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStatus playerActiveStatuses = collision.GetComponent<PlayerStatus>();
        PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();

        if (playerActiveStatuses != null && playerMovement != null)
        {
            if (!playerActiveStatuses.hasStatus("Slowed"))
            {
                playerActiveStatuses.addStatus("Slowed");

                playerMovement.MoveSpeed = playerMovement.baseMoveSpeed * 0.6f;
                Debug.Log("Player Slowed");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerStatus playerActiveStatuses = collision.GetComponent<PlayerStatus>();
        PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();

        if (playerActiveStatuses != null && playerMovement != null)
        {
            if (playerActiveStatuses.hasStatus("Slowed"))
            {
                playerActiveStatuses.removeStatus("Slowed");

                playerMovement.MoveSpeed = playerMovement.baseMoveSpeed;
                Debug.Log("Blind is no more more");
            }
        }
    }
}