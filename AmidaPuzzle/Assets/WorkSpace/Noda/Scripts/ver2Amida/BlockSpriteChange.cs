using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockSpriteChange : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> blockSpriteList = new List<Sprite>();

    public void ChangeBlockSprite(GameObject block, int Count)
    {
        block.GetComponent<Image>().sprite = blockSpriteList[Count];
    }
}
