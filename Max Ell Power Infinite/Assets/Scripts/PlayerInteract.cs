using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    public GameObject currentInterObject = null;
    

    void Update()
    {
        if(Input.GetButtonDown("Interact") && currentInterObject)
        {
            AudioController.sharedInstance.FadeOut();
            Invoke("ChangeScene", 0.35f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("InterObject"))
        {
            currentInterObject = other.gameObject;
            other.SendMessage("SetActive");
        }

        if (other.CompareTag("ExitZone"))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("InterObject"))
        {
            if (other.gameObject == currentInterObject)
            {
                currentInterObject = null;
                other.SendMessage("SetUnactive");
            }
           
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("InfinityMode");
    }
}
