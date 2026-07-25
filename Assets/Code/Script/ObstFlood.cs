using UnityEngine;

public class ObstFlood : MonoBehaviour
{
    private float playerOriginalSpeed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStatus playerActiveStatuses = collision.GetComponent<PlayerStatus>();
        PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
        playerOriginalSpeed = playerMovement.MoveSpeed;
        
        if (playerActiveStatuses != null)
        {
            if (!playerActiveStatuses.hasStatus("Slowed"))
            {
                playerActiveStatuses.addStatus("Slowed");
                playerMovement.MoveSpeed = (playerMovement.MoveSpeed * 0.6f);
                Debug.Log("Player Slowed");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerStatus playerActiveStatuses = collision.GetComponent<PlayerStatus>();
        PlayerMovement playerMovement = collision.GetComponent <PlayerMovement>();

        if (playerActiveStatuses.hasStatus("Slowed"))
        {
            playerActiveStatuses.removeStatus("Slowed");
            playerMovement.MoveSpeed = playerOriginalSpeed;
            Debug.Log("Blind is no more more");
        }
    }
}
