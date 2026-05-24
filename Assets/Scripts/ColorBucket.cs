using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorBucket : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public int colorIndex;
    public AnswerSlot[] answerSlots;
    public float checkDistance = 70f;

    public void SetColor(int index, Color color)
    {
        colorIndex = index;
        GetComponent<Image>().color = color;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        AnswerSlot target = null;
        float minDistance = checkDistance;

        foreach (AnswerSlot slot in answerSlots)
        {
            float distance = Vector2.Distance(transform.position, slot.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                target = slot;
            }
        }

        if (target != null)
            target.SetBucket(this);

        Destroy(gameObject);
    }

}
