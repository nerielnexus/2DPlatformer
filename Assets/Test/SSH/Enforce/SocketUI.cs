using UnityEngine;

public class SocketUI : MonoBehaviour
{
    //소켓 창
    public GameObject Socket;

    //장비슬롯(소켓)
    public Slot imageSlot;

    //장비슬롯(인벤토리)
    public Slot EquipSlot;

    //소켓슬롯
    public SocketSlot[] SocketSlots = new SocketSlot[3];
   

    //소켓UI셋팅
    public void SetUI(Slot slot)
    {
        imageSlot.AddItemData(slot.item);

        EquipSlot = slot;
        EquipData equipData = slot.item as EquipData;

        //소켓젬셋팅
        for (int i = 0; i < SocketSlots.Length; i++)
        {
            if(equipData.gemSocket != null)
            {
                if (equipData.gemSocket.TryGetValue(i, out GemItemData gem))
                {
                    if(gem != null)
                    {
                        SocketSlots[i].SocketCheck(gem);
                    }
                }
                else
                {
                    SocketSlots[i].RemoveItem();
                }
            }
            else
            {
                SocketSlots[i].RemoveItem();
            }
        }

        Socket.SetActive(true);
    }

    //소켓UI제거
    public void RemoveUI()
    {
        EquipSlot = null;

        Socket.SetActive(false);
    }
}
