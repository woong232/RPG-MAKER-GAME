using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    public int _count;
    public int itemID;
    public string pickUpSound;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AudioManager.instance.Play(pickUpSound);
            Inventory.instance.GetAnItem(itemID, _count);
            Destroy(this.gameObject);
        }
    }


}
