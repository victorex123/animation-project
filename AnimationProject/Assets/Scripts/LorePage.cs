using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LorePage
{
    [TextArea(1, 10)]
    public string[] enemySentenceList;
    public Image[] enemyImages;
}