using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoPanel : MonoBehaviour
{
    public Image SW;
    public Image SE;
    public Image NW;
    public Image NE;
    public Image W;
    public Image E;
    public Image Center;

    
    public void SetCount(int count)
    {
        var images = new Image[] { SW, SE, NW, NE, W, E, Center };
        foreach (var image in images) {
            image.enabled = false;
        }


        switch (count)
        {
            case 1:
                Center.enabled = true;
                break;
            case 2:
                SW.enabled = true;
                NE.enabled = true;
                break;
            case 3:
                SW.enabled = true;
                NE.enabled = true;
                Center.enabled = true;
                break;
            case 4:
                NE.enabled = true;
                NW.enabled = true;
                SW.enabled = true;
                SE.enabled = true;
                break;
            case 5:
                NE.enabled = true;
                NW.enabled = true;
                SW.enabled = true;
                SE.enabled = true;
                Center.enabled = true;
                break;
            case 6:
                W.enabled = true;
                E.enabled = true;
                NE.enabled = true;
                NW.enabled = true;
                SW.enabled = true;
                SE.enabled = true;
                break;
            default: break;
        }
    }
}
