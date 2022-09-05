
using UnityEngine;

public class Trap_Saw : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float moveDistance;
    [SerializeField] private float pauseTime;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;
    private float moveCooldown=0;

    private void Awake() {
        leftEdge = transform.position.x - moveDistance;
        rightEdge = transform.position.x + moveDistance;
    }
    private void Update() {
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else if (pauseTime > moveCooldown)
            {
                moveCooldown += Time.deltaTime;
            }
            else
            {
                movingLeft = false;
                moveCooldown = 0;
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else if (pauseTime > moveCooldown)
            {
                moveCooldown += Time.deltaTime;
            }
            else
            {
                movingLeft = true;
                moveCooldown = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
