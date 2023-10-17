using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KHS_UI_Screen : MonoBehaviour
{
    // 실행파일을 아직 만들지 못해서 확인방법이 딱히 없음

    public Dropdown resolutionDropdown;
    public int dropNum; // 클릭한 dropdown 넘버링
    
    List<Resolution> resolutions = new List<Resolution>();
    FullScreenMode screenMode;
    int baseNum; // 변경전 dropdown 넘버링(확인을 안누르면 전넘버 해상도로 유지해야하기 때문)
    int optionNum = 0;

    void Start()
    {
        for(int i=0; i<Screen.resolutions.Length; i++)
        {
            Debug.Log(Screen.resolutions[i]);
            if (Screen.resolutions[i].refreshRate == 60)
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }

        resolutionDropdown.options.Clear();

        foreach (Resolution r in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = r.width + " x " + r.height;   // Dropdown안에 들어갈 text부분
            resolutionDropdown.options.Add(option);     // 해상도의 종류만큼 Dropdown안에 들어간다

            if (r.width == Screen.width && r.height == Screen.height)
            {
                resolutionDropdown.value = optionNum;
            }
            optionNum++;
        }

        resolutionDropdown.RefreshShownValue(); // 새로고침 느낌
        resolutionDropdown.value = 18;

        baseNum = resolutionDropdown.value;
    }

    public void DropBoxChangeNumber(int x)
    {
        dropNum = x;
    }

    // 해상도 박스 닫으면 기존 해상도 유지
    public void ScreenBoxClose(GameObject screenBox)
    {
        resolutionDropdown.value = baseNum;
        screenBox.SetActive(false);
    }

    // [확인 버튼]을 누르면 해상도 변경
    public void BtnClick()
    {
        Screen.SetResolution(resolutions[dropNum].width, resolutions[dropNum].height, screenMode);
        baseNum = dropNum;
    }
}
