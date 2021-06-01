using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LorePage
{
    [TextArea(1, 10)]
    public string[] enemyCharacteristicList;
    public Sprite[] enemyImagesList;
    [TextArea(1, 10)]
    public string[] enemyNamesList;

    [TextArea(1, 10)]
    public string[] controlCharacteristicsList;
    public Sprite[] controlImagesList;
    [TextArea(1, 10)]
    public string[] controlNamesList;
}
