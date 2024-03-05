using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armourBehaviour : MonoBehaviour
{
    public gameBehaviour gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<gameBehaviour>();
    }
    // 1
    private void OnCollisionEnter(Collision collision)
    {
        // 2
        if(collision.gameObject.name == "Player")
        {
            // 3
            Destroy(this.transform.parent.gameObject);

            // 4
            Debug.Log("You picked up armour.");

            gameManager.Items += 1;
            gameManager.armourCount += 25;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
