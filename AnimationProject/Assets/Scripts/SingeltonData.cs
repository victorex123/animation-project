using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingeltonData : MonoBehaviour
{
    public static SingeltonData instance;

    #region Player
    //public float maxHealth = 100;
    public float actualHealth = 100;
    public int specialInitialState = 0;
    public Color fadeColor;
    public int deads = 0;

    public int weapon;
    public int ammo;

    public GameObject pistol;
    public GameObject rifle;
    public GameObject bazooka;
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance !=this)
            {
                Destroy(gameObject);
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(maxHealth);
    }
}
