
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthbar;
    [SerializeField] private Image currentHealthbar;

    private void Start() {
        currentHealthbar.fillAmount = playerHealth.currenthealth / 10;
    }
    private void Update() {
        currentHealthbar.fillAmount = playerHealth.currenthealth / 10;
    }
}
