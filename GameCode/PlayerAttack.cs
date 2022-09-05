
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireball;
    private Animator animate;
    private playerMovement player;
    private float cooldownTimer = 0.1f;

    private void Awake(){
        
        animate = GetComponent<Animator>();
        player = GetComponent<playerMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer >= attackCooldown && player.canAttack()) {
            attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void attack() {
        animate.SetTrigger("attack");
        cooldownTimer = 0;

        fireball[FindFireball()].transform.position = firePoint.position;
        fireball[FindFireball()].GetComponent<projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball() {
        for (int i = 0; i < fireball.Length; i++) {
            if (!fireball[i].activeInHierarchy) {
                return i;
            }
        }
        return 0;
    }
}
