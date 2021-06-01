using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHeal : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public GameObject bloodSplat;
    public GameObject bloodSplat2;

    public HealthBarEnemy healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        healthBar.SetHealth(currentHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage((int)other.GetComponent<Bullet>().damage);
            GameObject blood = Instantiate(bloodSplat, transform.position, Quaternion.identity);
            blood.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            blood.transform.position -= Vector3.up * 0.08f;
            if (currentHealth > 0)
            {
                GameObject newBloodSplat = Instantiate(bloodSplat2, other.transform.position, transform.rotation);
            }
        }
    }
}
