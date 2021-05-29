using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPackage : MonoBehaviour
{
    [SerializeField]
    private int rifleAmmo;
    [SerializeField]
    private int pistolAmmo;
    [SerializeField]
    private int bazookaAmmo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int returnAmmo(int n)
    {
        if(n == 0)
        {
            return pistolAmmo;
        }
        if(n == 1)
        {
            return rifleAmmo;
        }
        if(n == 2)
        {
            return bazookaAmmo;
        }
        return 0;
    }
}
