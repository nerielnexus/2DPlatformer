using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Move", menuName = "Enemy/Move")]
public class SSH_MonMoveTest : SSH_MonMove
{
    public int x;
    public int y;

    public int speed;

    public override void Move()
    {
        if (pos != null)
        {
            pos.Translate(new Vector3(x, y, 0) * speed * 0.1f * Time.deltaTime);

            Debug.Log("이동중");
        }

        Debug.Log("이동 x : " + x + " 이동 y : " + y);
    }
}
