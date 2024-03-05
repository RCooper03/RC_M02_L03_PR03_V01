using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
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
            Debug.Log("Item Collected!");

            gameManager.Items += 1;

            gameManager.PrintLootReport();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
