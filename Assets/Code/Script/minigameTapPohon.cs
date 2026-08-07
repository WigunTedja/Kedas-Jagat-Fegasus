using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MinigameTapPohon : MonoBehaviour, IPointerClickHandler
{
    //skrip untuk UI image pohon
    public int health = 3;
    public UnityEvent onObstacleCleared;
    //private GameObject hiddenItem = Component.

    public void OnPointerClick(PointerEventData eventData)
    {
        health--;

        // TODO:  trigger animasi goyang/memudar di sini
        Debug.Log("Obstacle disentuh! Sisa health: " + health);

        if (health <= 0)
        {
            // Memanggil event (misal: menjatuhkan kunci atau memunculkan kunci)
            //onObstacleCleared.Invoke();

            MinigameKeyItem hiddenItem = GetComponentInChildren<MinigameKeyItem>(true);
            hiddenItem.gameObject.SetActive(true);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Vector3 treePosition = transform.position;
            hiddenItem.transform.position = treePosition - new Vector3(0,1.4f,0);    
        }
    }
}