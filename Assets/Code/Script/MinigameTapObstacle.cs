using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MinigameTapObstacle : MonoBehaviour, IPointerClickHandler
{
    //skrip untuk UI image lumppur atau pohon
    public int health = 3; 
    public UnityEvent onObstacleCleared; 

    public void OnPointerClick(PointerEventData eventData)
    {
        health--;

        // TODO:  trigger animasi goyang/memudar di sini
        Debug.Log("Obstacle disentuh! Sisa health: " + health);

        if (health <= 0)
        {
            // Memanggil event (misal: menjatuhkan kunci atau memunculkan kunci)
            onObstacleCleared.Invoke();

            // Hapus objek rintangan (lumpur menghilang)
            Destroy(gameObject);
        }
    }
}