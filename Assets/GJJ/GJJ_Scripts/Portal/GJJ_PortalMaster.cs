using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PORTALSTATUS
{
    NONE = 0,
    CLOSE,
    OPEN
};

public class GJJ_PortalMaster : MonoBehaviour
{
    // public

    // private
    [SerializeField] private float portalCoolDownTime = 2.0f;
    [SerializeField] private bool portalOnCoolDown = false;
    [SerializeField] private List<GJJ_Portal> portaList;

    // method
    IEnumerator PortalCooldown()
    {
        if(!portalOnCoolDown)
        {
            portalOnCoolDown = true;
            yield return new WaitForSeconds(portalCoolDownTime);
            portalOnCoolDown = false;
        }
    }

    void GetPortalInChild()
    {
        for (int i = 0; i < transform.childCount; i++)
            portaList.Add(transform.GetChild(i).gameObject.GetComponent<GJJ_Portal>());
    }

    public bool CheckPlayer(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return false;

        string _layerName = LayerMask.LayerToName(col.gameObject.layer);
        if (_layerName != "Player") return false;

        if (col.gameObject.name.Equals("PlayerMan"))
            return true;

        return true;
    }

    public Vector2 GetOppositePortalCoord(GameObject _self)
    {
        GJJ_Portal _tmp = _self.GetComponent<GJJ_Portal>();
        Vector2 _errVec = new Vector2(-17321732.17321732f, -17321732.17321732f);
        Vector2 _resultVec = _errVec;

        if (portalOnCoolDown)
            return _resultVec;

        foreach(GJJ_Portal iter in portaList)
        {
            if (iter == _tmp) continue;

            if (iter.portalCurrentStatus == PORTALSTATUS.OPEN)
                _resultVec = iter.gameObject.transform.position;
        }

        if (_resultVec == _errVec)
            return _resultVec;
        else
        {
            StartCoroutine(PortalCooldown());
            return _resultVec;
        }
    }

    // unity
    private void Awake()
    {
        GetPortalInChild();
    }
}
