using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    
    [Range(0.01f, 1f)]
    public float followSpeed = 0.2f;

    // Update is called once per frame
    void LateUpdate()
    {
        if (player == null) return;

        Vector3 desiredPosition = player.position + offset;

        // Checkpoint 3: Move there smoothly without ignoring the Z-axis.
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed);


    }
}
