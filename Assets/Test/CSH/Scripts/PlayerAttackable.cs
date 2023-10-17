using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackable : MonoBehaviour
{
    //마우스 왼쪽 클릭으로 공격, 공격 모션 중에는 클릭 무효, 공격 모션 후 일정 시간 내에 클릭하면 다음 공격 모션, 그렇지 않으면 초기화
    public Transform weapon;    
    Animator _ani;

    [HideInInspector]
    public Equip _equip;
    public SpriteRenderer _weaponSprite;
    DamageCollider _dmgCol;
    PolygonCollider2D _polygon;
    
    int weaponCnt = 0;
    bool isUse = false; //코루틴 제어용 플래그

    private void Awake()
    {
        _ani = weapon.GetComponent<Animator>();
        _equip = GetComponent<Equip>();
        _dmgCol = _weaponSprite.GetComponent<DamageCollider>();
        _polygon = _weaponSprite.GetComponent<PolygonCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WeaponClick();

        
    }

    public void EquipWeapon(EquipData weaponItem)
    {
        _weaponSprite.sprite = weaponItem.itemSprite;

        _dmgCol.dmgValue = weaponItem.value; //값변경

        List<Vector2> points = new List<Vector2>();
        List<Vector2> simplifiedPoints = new List<Vector2>();
        _polygon.pathCount = _weaponSprite.sprite.GetPhysicsShapeCount();

        for(int i = 0; i < _polygon.pathCount; i++)
        {
            _weaponSprite.sprite.GetPhysicsShape(i, points);
            LineUtility.Simplify(points, 0.05f, simplifiedPoints);
            _polygon.SetPath(i, simplifiedPoints);
        }
    }

    public void UnEquipWeapon()
    {
        _weaponSprite.sprite = null;
        _dmgCol.dmgValue = 0;
    }
   

    void WeaponClick()
    {
        if (Input.GetKey(KeyCode.Z) && !isUse)
        {
            StartCoroutine(WeaponUse());
        }
        else if (!Input.GetKey(KeyCode.Z) && !isUse)
        {
            weaponCnt = 0;
            _ani.SetInteger("Slashing", weaponCnt);
        }
    }

    IEnumerator WeaponUse()
    {
        isUse = true;

        weaponCnt++;
        _ani.SetInteger("Slashing", weaponCnt);

        yield return new WaitForSeconds(0.5f);

        if (weaponCnt >= 3)
        {
            weaponCnt = 0;
        }
        
        isUse = false;

    }


}
