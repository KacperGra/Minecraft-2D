using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI textMesh;
    public RectTransform rectTransform;

    private int id;
    public int ID => id;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
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

        itemImage.sprite = sprite;
        if(amount == 0)
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
