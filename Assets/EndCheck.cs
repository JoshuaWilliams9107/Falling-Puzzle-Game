using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndCheck : MonoBehaviour {

    // Use this for initialization
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Landed")
        {
            Debug.Log("Hit");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
