using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CraftUI : MonoBehaviour
{
    //제작 UI
    public GameObject craft;

    //제작 재료 슬롯(인스펙터에서 수정)
    public Slot[] slots;

    //제작 재료 슬롯 최대 갯수(변경할 시 위의 slots도 인스펙터에서 수정)
    int maxSlot = 3;

    //제작 재료 슬롯 아이템의 주소 배열
    List<int> itemList;

    //현재 슬롯의 재료 갯수
    public int wood = 0;
    public int iron = 0;
    public int gold = 0;
    public int born = 0;
    public int scarab = 0;

    //제작가능여부(canCreate == 5 일 경우 제작가능)
    int canCreate = 0;

    //제작가능아이템 레시피
    int[] createRecipe = new int[] { };

    //제작 아이템 예정 주소
    int resultItemAddress;

    //제작 결과 슬롯
    public Slot resultSlot;

    //제작 레시피
    // int[] 레시피 = { 제작 결과 아이템, 나무, 철괴, 금괴, 뼈, 갑충석 }

    // 레시피1     팀원 1
    int[] recipe =  new int[6] { 102, 0, 2, 0, 0, 0 };
    int[] recipe2 = new int[6] { 103, 2, 0, 0, 0, 0 };
    int[] recipe3 = new int[6] { 104, 0, 1, 1, 1, 0 };

    // 레시피2     팀원 2
    int[] recipe4 = new int[6] { 105, 1, 1, 0, 1, 0 };
    int[] recipe5 = new int[6] { 113, 1, 1, 0, 0, 1 }; 
    int[] recipe6 = new int[6] { 112, 0, 1, 1, 0, 1 }; 

    // 레시피3     팀원 3
    int[] recipe7 = new int[6] { 110, 0, 0, 0, 2, 1 };   
    int[] recipe8 = new int[6] { 106, 0, 1, 0, 2, 0 };   
    int[] recipe9 = new int[6] { 111, 0, 0, 1, 2, 0 };  

    // 레시피4     팀원 4
    int[] recipe10 = new int[6] { 108, 0, 0, 0, 2, 0 };   
    int[] recipe11 = new int[6] { 109, 0, 1, 0, 1, 0 };   
    int[] recipe12 = new int[6] { 100, 0, 0, 0, 1, 2 };   


    //제작 레시피 모음
    public List<int[]> recipes = new List<int[]>();

    

    private void Start()
    {
        // 레시피 추가 메소드
        RecipeSet();
    }

    
    
    //제작 레시피 목록 추가
    void RecipeSet()
    {
        recipes.Add(recipe);
        recipes.Add(recipe2);
        recipes.Add(recipe3);
        recipes.Add(recipe4);
        recipes.Add(recipe5);
        recipes.Add(recipe6);
        recipes.Add(recipe7);
        recipes.Add(recipe8);
        recipes.Add(recipe9);
        recipes.Add(recipe10);
        recipes.Add(recipe11);
        recipes.Add(recipe12);
    }


    // 재료 주소 번호
    // 200 : 나무
    // 201 : 철괴
    // 202 : 금괴
    // 203 : 뼈
    // 204 : 갑충석


    // 제작 준비
    public void ReadyCraft()
    {
        itemList = new List<int>(3);

        wood = 0;
        iron = 0;
        gold = 0;
        born = 0;
        scarab = 0;

        // 재료 아이템 슬롯의 재료 아이템 주소 배열 준비
        for (int i = 0; i < maxSlot; i++)
        {
            itemList.Add(0);

            if(slots[i].itemOn)
            {
                itemList[i] = slots[i].item.address;
            }
        }

        EtcCheck();
    }

    //아이템 재료 슬롯 확인
    void EtcCheck()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            switch (itemList[i])
            {
                //나무
                case 200:
                    wood++;
                    break;
                //철괴
                case 201:
                    iron++;
                    break;
                //금괴
                case 202:
                    gold++;
                    break;
                //뼈
                case 203:
                    born++;
                    break;
                //갑충석
                case 204:
                    scarab++;
                    break;
            }
        }

        //레시피 확인 메소드
        RecipeCheck();
    }



    //레시피 확인
    void RecipeCheck()
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            createRecipe = recipes[i];

            // 각 재료가 레시피 목록에서 일치하는지 확인
            for (int j = 1; j < 6; j++)
            {
                switch (j)
                {
                    case 1:
                        if (createRecipe[j] == wood)
                        {
                            canCreate++;
                        }
                        break;
                    case 2:
                        if (createRecipe[j] == iron)
                        {
                            canCreate++;
                        }
                        break;
                    case 3:
                        if (createRecipe[j] == gold)
                        {
                            canCreate++;
                        }
                        break;
                    case 4:
                        if (createRecipe[j] == born)
                        {
                            canCreate++;
                        }
                        break;
                    case 5:
                        if (createRecipe[j] == scarab)
                        {
                            canCreate++;
                        }
                        break;
                }
            }

            // 레시피와 일치, 조합가능할 경우
            if (canCreate == 5)
            {
                // 레시피의 0번(결과 아이템)
                resultItemAddress = createRecipe[0];
                // 아이템 제작
                CraftItem(createRecipe[0]);

                break;
            }
            // 레시피와 불일치, 조합가능한 레시피가 없는경우
            else
            {
                //결과 슬롯 업데이트
                resultSlot.SlotUpdate();
                resultItemAddress = 0;

                //조합가능 여부 초기화
                canCreate = 0;
            }
        }
    }

    // 제작아이템 예정 이미지
    void CraftItem(int address)
    {
        //아이템 데이터 베이스에서 아이템 이미지 가져오기
        ItemData itemimage = ItemDataBase.itemDataBase.itemList[address];

        //결과슬롯에 아이템이미지 셋팅(실제 아이템 데이터 생성 X)
        resultSlot.SetItemImage(itemimage);
    }

    //제작 메소드
    public void Craft(string itemName)
    {
        if(resultItemAddress != 0)
        {
            //아이템 데이터 베이스에서 결과 아이템 주소값 확인 및 새로운 아이템 객체 생성
            ItemData item = ItemDataBase.itemDataBase.itemList[resultItemAddress];

            //결과슬롯에 아이템 추가(실제 아이템 데이터 생성 및 추가)
            resultSlot.AddItemData(item);

            //각 슬롯의 아이템제거
            slots[0].RemoveItem();
            slots[1].RemoveItem();
            slots[2].RemoveItem();

            //사용 변수 초기화
            resultItemAddress = 0;
            wood = 0;
            iron = 0;
            gold = 0;
            born = 0;
            scarab = 0;
        }
    }
}
