using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJ_Portal : MonoBehaviour
{
    // public
    public PORTALSTATUS portalCurrentStatus = PORTALSTATUS.NONE;

    // private
    [SerializeField] Sprite portalTextureActivated;
    [SerializeField] Sprite portalTextureDeactivated;
    [SerializeField] Sprite portalTextureNone;

    [SerializeField] private GJJ_PortalMaster portalMaster;
    [SerializeField] private SpriteRenderer portalSpriteRenderer;

    // method
    void ChangePortalImage()
    {
        if (portalCurrentStatus == PORTALSTATUS.NONE)
            portalSpriteRenderer.sprite = portalTextureNone;
        else if (portalCurrentStatus == PORTALSTATUS.OPEN)
            portalSpriteRenderer.sprite = portalTextureActivated;
        else if (portalCurrentStatus == PORTALSTATUS.CLOSE)
            portalSpriteRenderer.sprite = portalTextureDeactivated;
    }

    // unity
    private void Awake()
    {
        portalMaster = GetComponentInParent<GJJ_PortalMaster>();
        portalSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ChangePortalImage();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!portalMaster.CheckPlayer(collision))
            return;

        if(Input.GetKey(KeyCode.Z))
        {
            if (portalCurrentStatus == PORTALSTATUS.CLOSE)
                return;

            Vector2 _dest = portalMaster.GetOppositePortalCoord(gameObject);

            if (_dest != new Vector2(-17321732.17321732f, -17321732.17321732f))
                collision.gameObject.transform.position = _dest;
        }
    }
}
