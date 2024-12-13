using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterApartment : MonoBehaviour
{
    private string ApartmentScene;

    /*private void OnCollisionEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            
    }*/

    public void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("ApartmentScene");
    }
}
