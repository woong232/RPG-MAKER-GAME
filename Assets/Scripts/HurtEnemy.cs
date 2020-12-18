﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public GameObject prefabs_Floating_Text;
    public GameObject parent;
    public GameObject effect;

    public string atksound;

    private PlayerStat thePlayerStat;

    // Start is called before the first frame update
    void Start()
    {
        thePlayerStat = FindObjectOfType<PlayerStat>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            int dmg = collision.gameObject.GetComponent<EnemyStat>().Hit(thePlayerStat.atk);
            AudioManager.instance.Play(atksound);

            Vector3 vector = collision.transform.position;

            Instantiate(effect, vector, Quaternion.Euler(Vector3.zero));

            vector.y += 60;

            GameObject clone = Instantiate(prefabs_Floating_Text, vector, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingText>().text.text = dmg.ToString();
            clone.GetComponent<FloatingText>().text.color = Color.white;
            clone.GetComponent<FloatingText>().text.fontSize = 25;
            clone.transform.SetParent(parent.transform);
        }
    }
}
