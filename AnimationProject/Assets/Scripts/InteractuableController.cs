using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractuableController : MonoBehaviour
{

    public Camera playerCamera;
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
    private GameObject player;
    [SerializeField]
    private float dropForwardForce = 10;
    [SerializeField]
    private float dropUpwardForce = 10;
    private HingeJoint joint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        manager();
    }
     public bool checkInteract()
    {
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward,out hit, interactRange, layerobject))
        {
            equipedObject = hit.collider.gameObject;
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
        //Debug.DrawRay(transform.position, gameObject.transform.forward * interactRange, Color.yellow);
        if (Input.GetKeyDown(KeyCode.E) && !equipped && !buttonCheck && checkInteract())
        {
            buttonCheck = true;
        }

        if (equipped)
        {
            if (equipedObject.CompareTag("Gun"))
            {
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
        equipedObject.GetComponent<Rigidbody>().AddForce(transform.up * dropUpwardForce, ForceMode.Impulse);
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
    }

    public void withObject()
    {
        if (!buttonCheck)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                unequipJoint();
                buttonCheck = true;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                throwing();
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

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //xdshoot();
            }
        }
        equipedObject.transform.forward = playerCamera.transform.forward;
    }

    public void equipGun()
    {
        equipedObject.GetComponent<Rigidbody>().isKinematic = true;
        equipedObject.GetComponent<Collider>().enabled = false;
        equipedObject.transform.position = equipPosition.transform.position;
        equipedObject.transform.forward = player.transform.forward;
        equipedObject.transform.SetParent(player.transform);
        equipped = true;
    }

    public void unequipGun()
    {
        equipedObject.GetComponent<Rigidbody>().isKinematic = false;
        equipedObject.GetComponent<Collider>().enabled = true;
        equipedObject.transform.SetParent(null);
        equipedObject = null;
        equipped = false;
    }

}
