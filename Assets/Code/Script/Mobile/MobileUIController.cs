using UnityEngine;

public class MobileUIController : MonoBehaviour
{
    void Start()
    { 
        bool isMobileUI = PlayerPrefs.GetInt("MobileUI", 1) == 1;
        gameObject.SetActive(isMobileUI);
    }
}