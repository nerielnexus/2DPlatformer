using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJ_Trigger_Howto : MonoBehaviour
{
    public bool isActivated = false;

    [Header("ACTIVE FLAG")]
    [SerializeField] private bool haveNoActiveFlag = false;
    [SerializeField] private GameObject activeFlag;
    [SerializeField] private GameObject fogVisibleArea;
    private KeyCode _myKeycode = KeyCode.Z;
    [Header("READ ONLY")]
    [SerializeField] private RigidbodySleepMode2D sleepMode;
    [SerializeField] private GJJ_TriggerAndDoor parent;
    [SerializeField] private GameObject fogVisibleInstance;

    bool CheckPlayer(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player"))
            return false;

        if (col.gameObject.layer != LayerMask.NameToLayer("Player"))
            return false;

        return true;
    }

    void DrawActiveFlag()
    {
        if (haveNoActiveFlag)
            return;

        activeFlag.SetActive(isActivated);
        fogVisibleInstance.SetActive(isActivated);
    }

    private void Awake()
    {
        parent = GetComponentInParent<GJJ_TriggerAndDoor>();

        if(haveNoActiveFlag)
        {
            activeFlag = null;
            fogVisibleArea = null;
        }
        else
        {
            activeFlag.SetActive(false);
            fogVisibleInstance = Instantiate(fogVisibleArea, transform.position, Quaternion.identity);
            fogVisibleInstance.SetActive(false);
        }

    }

    private void Update()
    {
        DrawActiveFlag();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CheckPlayer(collision))
            return;

        sleepMode = collision.gameObject.GetComponent<Rigidbody2D>().sleepMode;
        collision.gameObject.GetComponent<Rigidbody2D>().sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!CheckPlayer(collision))
            return;

        Debug.Log(collision.gameObject);

        if (Input.GetKey(_myKeycode))
            isActivated = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!CheckPlayer(collision))
            return;

        collision.gameObject.GetComponent<Rigidbody2D>().sleepMode = sleepMode;
    }
}
