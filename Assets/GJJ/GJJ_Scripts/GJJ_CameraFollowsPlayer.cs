using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJ_CameraFollowsPlayer : MonoBehaviour
{
    [SerializeField] private GameObject fogVisibleArea;
    [SerializeField] private bool isNightMap = false;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject fog;

    // method
    void SetCameraViewArea()
    {
        Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y, -10.0f);
        Vector3 cameraMovePos = new Vector3(0f, 4.0f, 0f);

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            transform.position = targetPos + cameraMovePos;
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            transform.position = targetPos - cameraMovePos;
        else
            transform.position = targetPos;
    }

    void InstantiateFOGVisibleArea()
    {
        try
        {
            if (!isNightMap)
                return;


            fog = Instantiate(fogVisibleArea, player.transform.position, Quaternion.identity);
            fog.transform.SetParent(player.transform);
        }
        catch (System.NullReferenceException e)
        {
            if (!isNightMap)
            {
                Debug.LogError("의도한 에러니 무시하세요. (GJJ_CameraFollowPlayer.cs/InstantiateFOGVisibleArea)");
                return;
            }
            else
                throw e;
        }
    }

    void SetFOGVisivleArea()
    {
        if (!isNightMap)
            return;
        
        fogVisibleArea.transform.position = player.transform.position;
    }

    void IncreaseFOGVisibleArea()
    {
        if (!isNightMap)
            return;

        // area scale - original : 3, magnified : 6
        if (Input.GetKey(KeyCode.C))
            fog.transform.localScale = new Vector3(6, 6, 0);
        else
            fog.transform.localScale = new Vector3(3, 3, 0);
    }

    // unity
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InstantiateFOGVisibleArea();
    }

    void LateUpdate()
    {
        SetCameraViewArea();
        SetFOGVisivleArea();
        IncreaseFOGVisibleArea();
    }
}
