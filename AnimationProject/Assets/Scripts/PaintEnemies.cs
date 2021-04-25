using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintEnemies : MonoBehaviour
{
    public float maxHealth = 100.0f;
    public Color colorDead;
    public Material basicMaterial;

    [Range(0, 100)]
    [SerializeField] private float health = 100.0f;

    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mat = new Material(mr.sharedMaterial);
        mr.material = mat;
    }

    // Update is called once per frame
    void Update()
    {
        //if (health <= 0)
        //{
        //    Destroy(this.gameObject);
        //}

        ChangeColor();
    }

    public void ChangeColor()
    {        //reset the timer
        if (health > 0)
        {
            //reduce the health of the enemy
            //change the color of the enemy based on the health
            float colT = Mathf.Clamp01(health / maxHealth);
            Color col = Color.Lerp(colorDead, basicMaterial.color, colT*2);
            mat.color = col;
        }
    }
}
