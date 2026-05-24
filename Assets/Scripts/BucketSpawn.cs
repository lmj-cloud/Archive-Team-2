using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BucketSpawn : MonoBehaviour, IPointerDownHandler
{
    public ColorBucket bucketImg;
    public Canvas canvas;
    public AnswerSlot[] answerSlots;

    private Vector3 spawnPos;
    private static ColorBucket spawnedbucket;

    public int colorIndex;
    public Color color;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (spawnedbucket != null)
        {
            Destroy(spawnedbucket.gameObject);
        }

        spawnPos = transform.position + new Vector3(0, 75, 0);
        spawnedbucket = Instantiate(bucketImg, spawnPos, Quaternion.identity, canvas.transform);
        spawnedbucket.answerSlots = answerSlots;
        spawnedbucket.SetColor(colorIndex, color);
    }
}
