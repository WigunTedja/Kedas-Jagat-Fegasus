using UnityEngine;


public class ObstSmoke : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStatus playerActiveStatuses = collision.GetComponent<PlayerStatus>();
        if(playerActiveStatuses != null)
        {
            if (!playerActiveStatuses.hasStatus("Blinded"))
            {
                playerActiveStatuses.addStatus("Blinded");
                Debug.Log("Player Blinded");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerStatus playerActiveStatuses = collision.GetComponent<PlayerStatus>();

        if (playerActiveStatuses.hasStatus("Blinded"))
        {
            playerActiveStatuses.removeStatus("Blinded");
            Debug.Log("Blind is no more more");
        }
    }
}
