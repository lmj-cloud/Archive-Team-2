using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Net.NetworkInformation;

public class AnswerSlot : MonoBehaviour
{
    public int slotIndex;

    public ColorPickerManager colorPickerManager;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetBucket(ColorBucket bucket)
    {
        image.color = bucket.GetComponent<Image>().color;
        colorPickerManager.SetAnswer(slotIndex, bucket.colorIndex);
    }
}
