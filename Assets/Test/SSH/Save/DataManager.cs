using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//임시 저장, 로드 (수정할것)
public class DataManager : MonoBehaviour
{
    //인벤토리UI 초기화
    public InvenUI invenUI;
    public StageManager scene;

    string GameDataFileName = "SaveData.json";

    public Data saveData;

    void Awake()
    {
        // 2022/12/30 김현석 - 추가
        var obj = FindObjectsOfType<DataManager>();
        if(obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 데이터 저장
    /// </summary>
    public void SaveData()
    {
        saveData = new Data();

        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        //인벤토리 아이템 저장
        for (int i = 0; i < invenUI.slotList.Length; i++)
        {
            if (invenUI.slotList[i].itemOn)
            {
                // 장비 아이템일 경우 장비 데이터 생성후 저장
                if (invenUI.slotList[i].item.itemType == 2 || invenUI.slotList[i].item.itemType == 3)
                {
                    saveData.itemData.Add(new SaveItemData(invenUI.slotList[i].item as EquipData));
                }
                // 장비 아이템를 제외한 아이템데이터 저장
                else
                {
                    saveData.itemData.Add(new SaveItemData(invenUI.slotList[i].item));
                }
            }
            else
            {
                saveData.itemData.Add(null);
            }
        }

        // 장비창 아이템 불러오기(true 는 save)
        EquipInvenData(true);

        for (int i = 0; i < scene.stage.Length; i++)
        {
            saveData.stage[i] = scene.stage[i];
        }

        string ToJsonData = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log(filePath);
    }


    /// <summary>
    /// 데이터 로드
    /// </summary>
    public void LoadData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        string FromJsonData;

        //세이브 파일 존재 검사
        if (File.Exists(filePath))
        {
            FromJsonData = File.ReadAllText(filePath);
            saveData = JsonUtility.FromJson<Data>(FromJsonData);

            // 인벤토리 아이템 불러오기
            for (int i = 0; i < 14; i++)
            {
                // 세이브데이터 아이템 검사
                if (saveData.itemData[i] != null)
                {
                    //불러온 아이템 인벤에 넣기
                    invenUI.SetItem(i, LoadItemData(saveData.itemData[i].address, i));
                }
            }

            // 장비창 아이템 불러오기(false 는 load)
            EquipInvenData(false);

            for (int i = 0; i < scene.stage.Length; i++)
            {
                scene.stage[i] = saveData.stage[i];
            }

        }
        // 세이브 파일 없을 시
        else
        {
            Debug.Log("세이브 파일이 없습니다.");
            return;
        }
    }

    /// <summary>
    /// 아이템데이터 로드
    /// address = 주소
    /// equip = saveData.itemData 주소 값
    ///         -1일 경우) 장비 아이템이 아님
    /// </summary>
    /// <param name="i" ></param>
    ItemData LoadItemData(int address, int equip)
    {
        //로드할 아이템
        ItemData loadItem;

        //베이스 아이템 데이터 검사
        if (ItemDataBase.itemDataBase.itemList.TryGetValue(address, out ItemData itemData))
        {
            //장비 아이템
            if (1 <= itemData.itemType && itemData.itemType <= 3 && equip != -1)
            {
                loadItem = LoadEquipItemData(itemData, saveData.itemData[equip]);

                return loadItem;
            }
            //장비 외의 아이템
            else
            {
                loadItem = itemData.CreateItemData();

                return loadItem;
            }
        }
        else
        {
            return null;
        }
    }

   /// <summary>
   /// 로드할 아이템 데이터 새 객체 생성
   /// </summary>
   /// <param name="itemData"></param>
   /// <param name="i"></param>
    ItemData LoadEquipItemData(ItemData itemData, SaveItemData loadEquipData)
    {
        EquipData baseEquipData = itemData as EquipData;

        EquipData equipData = baseEquipData.CreateItemData() as EquipData;

        //장비데이터 변수 초기화
        equipData.itemName = loadEquipData.itemName;
        equipData.durability = loadEquipData.durability;
        equipData.value = loadEquipData.value;
        equipData.enforceValue = loadEquipData.enforceValue;


        equipData.fire = loadEquipData.fire;


        equipData.water = loadEquipData.water;

        equipData.light = loadEquipData.light;

        equipData.dark = loadEquipData.dark;

        equipData.gemSocket = new Dictionary<int, GemItemData>(3);

        for (int i = 0; i < 3; i++)
        {
            if(LoadItemData(loadEquipData.gemData[i], -1) != null)
            {
                equipData.gemSocket[i] = LoadItemData(loadEquipData.gemData[i], -1) as GemItemData;
            }
        }

        ItemData data = equipData as ItemData;

        return data;
    }


    // 장비창 데이터 관리
    // true일 경우 세이브, false일 경우 로드
    void EquipInvenData(bool saveOn)
    {
        //장비창 아이템 저장
        if (saveOn)
        {
            //무기 저장
            if (EquipInven.equipInven.waeponSlot.itemOn)
            {
                saveData.waeponData = new SaveItemData(EquipInven.equipInven.waeponSlot.item as EquipData);
            }
            else
            {
                return;
            }

            //방어구 저장
            if (EquipInven.equipInven.armorSlot.itemOn)
            {
                saveData.armorData = new SaveItemData(EquipInven.equipInven.armorSlot.item as EquipData);
            }
            else
            {
                return;
            }
        }
        //장비창 아이템 불러오기
        else
        {
            //무기 데이터
            if (ItemDataBase.itemDataBase.itemList.TryGetValue(saveData.waeponData.address, out ItemData loadWaepon))
            {
                EquipInven.equipInven.waeponSlot.AddItemData(LoadEquipItemData(loadWaepon, saveData.waeponData));
            }
            //방어구 데이터
            if (ItemDataBase.itemDataBase.itemList.TryGetValue(saveData.armorData.address, out ItemData loadArmor))
            {
                EquipInven.equipInven.armorSlot.AddItemData(LoadEquipItemData(loadArmor, saveData.armorData));
            }
        }
    }



    /// <summary>
    /// 세이브파일 지우기
    /// </summary>
    public void ResetData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        File.Delete(filePath);
    }
}
