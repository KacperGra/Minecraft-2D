using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI textMesh;

    private RectTransform rectTransform;
    public RectTransform RectTransform => GetRectTransform();
    private int id;
    public int ID => id;

    public Player player;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private RectTransform GetRectTransform()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }

        return rectTransform;
    }

    public void Initalize(int id)
    {
        this.id = id;
    }

    public void SetItem(Sprite sprite, int amount)
    {
        if(sprite != null)
        {
            itemImage.color = Color.white;
        }
        else
        {
            itemImage.color = new Color(1f, 1f, 1f, 0f);
        }

        if(amount == 0)
        {
            itemImage.color = new Color(1f, 1f, 1f, 0f);
        }

        itemImage.sprite = sprite;
        if(amount <= 1)
        {
            textMesh.enabled = false;
        }
        else
        {
            textMesh.enabled = true;
            textMesh.text = amount.ToString();
        }
    }
}
