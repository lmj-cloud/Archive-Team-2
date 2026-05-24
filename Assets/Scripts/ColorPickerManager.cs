using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public enum GameDifficulty
{
    Easy,
    Normal,
    Hard
}

public static class GameData
{
    public static GameDifficulty difficultyData;
}

public class ColorPickerManager : MonoBehaviour
{
    public GameDifficulty difficulty;
    public Color[] colors;

    public Image[] answerSlots;
    public Image[] hintSlots;

    public Sprite correctImg;
    public Sprite wrongImg;
    public Sprite halfImg;
    public Sprite emptyImg;

    [SerializeField] private Canvas parentCanvas;
    [SerializeField] private GameObject starParticleObj;
    [SerializeField] private Camera cam;
    private GameObject spawnedParticle;

    public TMP_Text remainCountText;

    private int[] answer = new int[5];
    private int[] playerAnswer = new int[5];

    private int remainCount;

    void Start()
    {
        difficulty = GameData.difficultyData;

        SetRemainCount();
        CreateAnswer();
        UpdateRemainText();

        for(int i = 0; i < 5; i++)
        {
            playerAnswer[i] = -1;
            hintSlots[i].sprite = emptyImg;
            hintSlots[i].color = Color.white;
        }
    }

    private void SetRemainCount()
    {
        if (difficulty == GameDifficulty.Easy)
            remainCount = 10;
        else if(difficulty == GameDifficulty.Normal)
            remainCount = 15;
        else if(difficulty == GameDifficulty.Hard)
            remainCount = 10;
    }

    public void CreateAnswer()
    {
        for(int i = 0; i < answerSlots.Length; i++)
        {
            answer[i] = Random.Range(0, colors.Length);
            Debug.Log(answer[i]);
        }
    }

    public void SetAnswer(int slotIndex, int colorIndex)
    {
        playerAnswer[slotIndex] = colorIndex;
    }

    public void CheckAnswer()
    {
        if (remainCount <= 0)
            return;

        remainCount--;

        if (difficulty == GameDifficulty.Easy)
            ShowEasyHint();
        else
            ShowNormalHardHint();

        UpdateRemainText();

        if (IsClear())
        {
            Debug.Log("¤µ¤©");
        }
        else if(remainCount <= 0)
        {
            Debug.Log("¤½¤²");
        }
    }

    private void ShowEasyHint()
    {
        bool[] usedAnswer = new bool[5];
        bool[] usedPlayer = new bool[5];

        for (int i = 0; i < 5; i++)
        {
            hintSlots[i].sprite = wrongImg;
        }

        for (int i = 0; i < 5; i++)
        {
            if (playerAnswer[i] == answer[i])
            {
                hintSlots[i].sprite = correctImg;

                Vector3 spawnParticlePos = cam.ScreenToWorldPoint(answerSlots[i].transform.position);
                spawnedParticle = Instantiate(starParticleObj, spawnParticlePos, Quaternion.identity, parentCanvas.transform);
                Destroy(spawnedParticle, 1.5f);

                usedAnswer[i] = true;
                usedPlayer[i] = true;
            }
        }

        for (int i = 0; i < 5; i++)
        {
            if (usedPlayer[i])
                continue;

            for(int j = 0; j < 5; j++)
            {
                if (usedAnswer[j])
                    continue;

                if(playerAnswer[i] == answer[j])
                {
                    hintSlots[i].sprite = halfImg;
                    usedPlayer[i] = true;
                    break;
                }
            }
        }
    }

    private void ShowNormalHardHint()
    {
        int correctCount = 0;

        for(int i = 0; i < 5; i++)
        {
            if(playerAnswer[i] == answer[i]) { correctCount++; }
        }

        for(int i = 0; i < 5; i++)
        {
            if (i < correctCount)
            {
                hintSlots[i].color = Color.green;
                Vector3 spawnParticlePos = cam.ScreenToWorldPoint(answerSlots[i].transform.position);
                spawnedParticle = Instantiate(starParticleObj, spawnParticlePos, Quaternion.identity, parentCanvas.transform);
                Destroy(spawnedParticle, 1.5f);
            }
            else
                hintSlots[i].color = Color.white;
        }
    }

    private bool IsClear()
    {
        for(int i = 0; i < 5; i++)
        {
            if (playerAnswer[i] != answer[i])
                return false;
        }
        return true;
    }
    private void UpdateRemainText()
    {
        remainCountText.text = remainCount.ToString();
    }
}
