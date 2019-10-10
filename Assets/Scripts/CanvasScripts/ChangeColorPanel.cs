using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject panelColorDrugs;

    private Color green = new Color(0, 255, 0, 0.2f);
    private Color magenta= new Color(255, 0, 255, 0.2f);
    private Color yellow = new Color(255, 255, 0, 0.2f);
    private Color blue = new Color(0, 0, 255, 0.2f);

    public void SetActivePanelFalse()
    {
        panelColorDrugs.SetActive(false);
    }


   public void ChangeColorCocaine()
    {
      
            panelColorDrugs.SetActive(true);
        panelColorDrugs.GetComponent<Image>().color = magenta;
       
    }

    public void ChangeColorHash()
    {

        panelColorDrugs.SetActive(true);
        panelColorDrugs.GetComponent<Image>().color = green;

    }

    public void ChangeColorSpeed()
    {

        panelColorDrugs.SetActive(true);
        panelColorDrugs.GetComponent<Image>().color = yellow;

    }

    public void ChangeColorMeth()
    {

        panelColorDrugs.SetActive(true);
        panelColorDrugs.GetComponent<Image>().color = blue;

    }
}
