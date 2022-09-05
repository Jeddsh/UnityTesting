
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currenthealth { get; private set; }
    private Animator animate;
    private bool dead = false;

    private void Awake() {
        currenthealth = startingHealth;
        animate = GetComponent<Animator>();
    }
    public void Heal(float _heal) {
        currenthealth = Mathf.Clamp(currenthealth + _heal, 0, startingHealth);
    }
    public void TakeDamage(float _damage)
    {
        currenthealth = Mathf.Clamp(currenthealth - _damage, 0, startingHealth);
        //currenthealth += -_damage;
        if (currenthealth > 0)
        {
            //player hurt
            animate.SetTrigger("hurt");
        }
        else
        {
            //player ded
            if (!dead)
            {
                animate.SetTrigger("dead");
                GetComponent<playerMovement>().die(true);
               GetComponent<playerMovement>().enabled = false;
                dead = true;
            }
        }
    }
}
