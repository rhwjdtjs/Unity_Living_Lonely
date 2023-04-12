using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlotUtil : MonoBehaviour
{
    [SerializeField] private Image imageItem;
    static public SlotUtil instance;
    public Slot slotUtil;


    void Start()
    {
        instance = this;
    }

    public void SetImage(Image _itemImage)
    {
        imageItem.sprite = _itemImage.sprite;
        SetColor(1);
    }

    public void SetColor(float _alpha)
    {
        Color color = imageItem.color;
        color.a = _alpha;
        imageItem.color = color;
    }
}
