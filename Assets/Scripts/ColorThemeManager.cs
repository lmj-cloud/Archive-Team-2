using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ColorThemeManager : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Image[] colorSlots;
    [SerializeField] private TMP_Dropdown options;

    [Header("첫번째 테마")]
    [SerializeField] private Color[] firstColors;

    [Header("두번째 테마")]
    [SerializeField] private Color[] secondColors;

    string DROPDOWN_KEY = "DROPDOWN_KEY";

    int currentOption;

    List<string> optionList = new List<string>();

    void Awake()
    {
        if (PlayerPrefs.HasKey(DROPDOWN_KEY) == false) currentOption = 0;
        else currentOption = PlayerPrefs.GetInt(DROPDOWN_KEY);
    }

    void Start()
    {
        options.ClearOptions();

        optionList.Add("Option 1");
        optionList.Add("Option 2");

        options.AddOptions(optionList);

        options.value = currentOption;

        options.onValueChanged.AddListener(delegate { setDropDown(options.value); });
        setDropDown(currentOption); //최초 옵션 실행이 필요한 경우
    }

    void setDropDown(int option)
    {
        PlayerPrefs.SetInt(DROPDOWN_KEY, option);
    }

    private void Update()
    {
        if (options.value == 0)
        {
            for (int i = 0; i < colorSlots.Length; i++)
            {
                colorSlots[i].color = firstColors[i];
            }
        }

        if(options.value == 1)
        {
            for (int i = 0; i < colorSlots.Length; i++)
            {
                colorSlots[i].color = secondColors[i];
            }
        }
    }
}
