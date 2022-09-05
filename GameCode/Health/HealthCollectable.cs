using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    [SerializeField] private float HealthValue;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<Health>().Heal(HealthValue);
            gameObject.SetActive(false);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
