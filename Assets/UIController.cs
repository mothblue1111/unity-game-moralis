using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject HomeScreen, MaruPanel, Maru, DetailedMaruPanel, ItemsTab;
    [SerializeField]
    private GameObject[] panels, ListOfPlusButton;
    [SerializeField] private GameObject fadein;
    public Sprite plus;
    private int maruindexcounter = 0;
    [SerializeField] private CanvasGroup mycanvasgroup;
    [SerializeField] private Sprite ActiveSprite, mainSprite;
    private void Start()
    {
        fadein.SetActive(true);
      
    }

    public ToggleGroup togglegroup;
    private bool Zoom = true;
    public void OpenTab(GameObject currentTab)
    {
        foreach (GameObject p in panels)
        {
            if (currentTab == p)
            {
                //mycanvasgroup.alpha = 1;
                p.SetActive(true);
            }
            else
                p.SetActive(false);
        }
        foreach (Toggle t in togglegroup.GetComponentsInChildren<Toggle>())
        {
            if (t.isOn)
            {
                t.interactable = false;
                t.GetComponent<Image>().sprite = ActiveSprite;
               // t.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = t.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta + new Vector2(5f, 5f);
            }
            else
            {
                t.interactable = true;
                t.GetComponent<Image>().sprite = mainSprite;
                //t.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = t.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta - new Vector2(5f, 5f);
            }
        }
       
    }
    public void ToggleIcons()
    {
        

    }
    public void ConfirmChoosedMaru()
    {
        for (int i = 0; i < maruindexcounter; i++)
        {
            
        }
    }
    public void ToggleClicked(int index)
    {
        
    }

    public void Counter(Toggle tgl)
    {
        if (tgl.isOn)
        {
            ListOfPlusButton[maruindexcounter].GetComponent<Image>().sprite = tgl.transform.parent.GetComponent<Image>().sprite;
            maruindexcounter++;
        }
        else
        {
            maruindexcounter--;
            ListOfPlusButton[maruindexcounter].GetComponent<Image>().sprite = plus;
        }
        
        
        
 
    }


   

}
