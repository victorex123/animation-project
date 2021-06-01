using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractuableController : MonoBehaviour
{
    [SerializeField]
    private Camera primaryCamera;
    [SerializeField]
    private Camera secondaryCamera;
    private Camera cameraUsed;
    [SerializeField]
    private float interactRange = 10f;
    private bool equipped = false;
    private bool buttonCheck = false;
    private GameObject equipedObject;
    private RaycastHit hit;
    private int layerobject = 1 << 3;
    [SerializeField]
    private GameObject equipPosition;
    [SerializeField]
    private GameObject zaHando;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float dropForwardForce = 10;
    [SerializeField]
    private HingeJoint joint;
    private float gunCooldown;
    private float gunTimer = 0;
    [SerializeField]
    private Text textAmmo;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkCamera();
        manager();
        checkText();
        gunTimer += Time.deltaTime;
    }
     public bool checkInteract()
    {
        if (Physics.Raycast(cameraUsed.transform.position, cameraUsed.transform.forward,out hit, interactRange, layerobject))
        {
            equipedObject = hit.collider.gameObject;
            zaHando.gameObject.SetActive(false);
            if (equipedObject.CompareTag("Gun"))
            {
                equipGun();
            }
            else
            {
                equipJoint();
            }
            return true;
        }

        return false;
    }
    public void manager()
    {

        if (Input.GetKeyDown(KeyCode.E) && !equipped && !buttonCheck && checkInteract())
        {
            buttonCheck = true;
        }

        if (equipped)
        {
            if (equipedObject.CompareTag("Gun"))
            {
                equippedUpdate();
                withGun();
            }
            else
            {
                withObject();
            }
        }


        if (!Input.GetKeyDown(KeyCode.E))
        {
            buttonCheck = false;
        }
    }

    /*public void equip()
    {
        equipedObject.transform.position = equipPosition.transform.position;
        equipedObject.transform.SetParent(player.transform);
        equipped = true;
        equipedObject.GetComponent<Rigidbody>().useGravity = false;
    }

    public void unequip()
    {
        equipedObject.transform.SetParent(null);
        equipedObject.GetComponent<Rigidbody>().useGravity = true;
        equipedObject = null;
        equipped = false;
    }
    */

    public void throwing()
    {
        Destroy(equipedObject.GetComponent<HingeJoint>());
        equipedObject.GetComponent<Rigidbody>().useGravity = true;
        equipedObject.GetComponent<Rigidbody>().AddForce(transform.forward * dropForwardForce, ForceMode.Impulse);
        float random = Random.Range(-1f, 1f);
        equipedObject.GetComponent<Rigidbody>().AddTorque(new Vector3(random, random, random) * 10);
        equipedObject = null;
        equipped = false;
    }

    public void equipJoint()
    {
        equipedObject.transform.position = equipPosition.transform.position;
        equipped = true;
        joint = equipedObject.AddComponent<HingeJoint>();
        configureJoint();
    }

    public void unequipJoint()
    {
        Destroy(equipedObject.GetComponent<HingeJoint>());
        equipedObject = null;
        equipped = false;
    }

    public void configureJoint()
    {
        JointSpring aux = new JointSpring();
        aux.spring = 100;
        aux.damper = 10;
        joint.autoConfigureConnectedAnchor = false;
        joint.anchor = Vector3.right;
        joint.axis = new Vector3(1, 1, 1);
        joint.connectedAnchor = Vector3.zero;
        joint.useSpring = true;
        joint.spring = aux;
        joint.connectedBody = equipPosition.GetComponent<Rigidbody>();
        joint.enableCollision = false;
    }

    public void withObject()
    {
        if (!buttonCheck)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                unequipJoint();
                buttonCheck = true;
                zaHando.gameObject.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                throwing();
                zaHando.gameObject.SetActive(true);
            }
        }

    }

    public void withGun()
    {
        if (!buttonCheck)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                unequipGun();
                buttonCheck = true;
            }

            if (Input.GetKey(KeyCode.Mouse0) && gunTimer >= gunCooldown)
            {
                equipedObject.GetComponent<GunScript>().Shoot();
                gunTimer = 0;
            }
        }
    }

    public void equipGun()
    {
        equipedObject.GetComponent<Rigidbody>().isKinematic = true;
        equipedObject.GetComponent<BoxCollider>().enabled = false;
        equipedObject.GetComponent<SphereCollider>().enabled = false;
        equipedObject.transform.position = equipPosition.transform.position;
        equipedObject.transform.forward = player.transform.forward;
        gunCooldown = equipedObject.GetComponent<GunScript>().GetShootCooldown();
        equipped = true;
    }

    public void unequipGun()
    {
        equipedObject.GetComponent<Rigidbody>().isKinematic = false;
        equipedObject.GetComponent<BoxCollider>().enabled = true;   
        equipedObject.GetComponent<SphereCollider>().enabled = true;
        zaHando.gameObject.SetActive(true);
        equipedObject = null;
        equipped = false;
    }

    public void equippedUpdate()
    {
        equipedObject.transform.position = equipPosition.transform.position;
        if (cameraUsed == primaryCamera)
        {
            equipedObject.transform.right = player.transform.right;
        }
        else
        {
            equipedObject.transform.forward = cameraUsed.transform.forward;
        }
        
    }

    public void checkCamera()
    {
        if (primaryCamera.gameObject.activeInHierarchy)
        {
            cameraUsed = primaryCamera;
        }
        else
        {
            cameraUsed = secondaryCamera;
        }
    }

    public void giveAmmo(int ammo)
    {
        equipedObject.GetComponent<GunScript>().setAmmo(ammo);
    }

    public void checkText()
    {
        if(equipedObject != null && equipedObject.CompareTag("Gun"))
        {
            textAmmo.text = equipedObject.GetComponent<GunScript>().GetAmmo() + "";
            textAmmo.gameObject.SetActive(true);
        }
        else if(textAmmo.IsActive())
        {
            textAmmo.gameObject.SetActive(false);
        }
    }

   private void OnTriggerEnter(Collider other)
    {
        if(equipedObject != null)
        {
            if (other.CompareTag("Ammo") && equipedObject.CompareTag("Gun"))
            {
                giveAmmo(other.GetComponent<AmmoPackage>().returnAmmo(equipedObject.GetComponent<GunScript>().GetMode()));
                Destroy(other.gameObject);
            }
        }
    }
}
