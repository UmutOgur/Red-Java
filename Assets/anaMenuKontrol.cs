using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class anaMenuKontrol : MonoBehaviour {

    //GameObject level1, level2, level3;
    //GameObject kilit1, kilit2, kilit3;
    GameObject leveller,kilitler;
	void Start ()
    {
        //level1 = GameObject.FindGameObjectWithTag("lvl1");
        //level2 = GameObject.FindGameObjectWithTag("lvl2");
        //level3 = GameObject.FindGameObjectWithTag("lvl3");

        //kilit1 = GameObject.Find("kilit 1");
        //kilit2 = GameObject.Find("kilit 2");
        //kilit3 = GameObject.Find("kilit 3");

        //level1.SetActive(false);
        //level2.SetActive(false);
        //level3.SetActive(false);

        //kilit1.SetActive(false);
        //kilit2.SetActive(false);
        //kilit3.SetActive(false);

        leveller = GameObject.Find("leveller");
        kilitler = GameObject.Find("kilitler");
        for (int i = 0; i < leveller.transform.childCount; i++)
        {
            leveller.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < kilitler.transform.childCount; i++)
        {
           kilitler.transform.GetChild(i).gameObject.SetActive(false);
        }
    // PlayerPrefs.DeleteAll(); // oyun resetlemek için 

        for (int i = 0; i < PlayerPrefs.GetInt("kacincilevel"); i++)
        {
            leveller.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }
    public void butonSec(int gelenButon)
    {
        if (gelenButon==1)
        {
            SceneManager.LoadScene(1);
        }
       else if (gelenButon == 2)
        {
           
            for (int i = 0; i < kilitler.transform.childCount; i++)
            {
               kilitler.transform.GetChild(i).gameObject.SetActive(true);
            }
            for (int i = 0; i < leveller.transform.childCount; i++)
            {
                leveller.transform.GetChild(i).gameObject.SetActive(true);
            }
          

            for (int i = 0; i < PlayerPrefs.GetInt("kacincilevel"); i++)
            {
                kilitler.transform.GetChild(i).gameObject.SetActive(false);
            }

        }
       else if (gelenButon == 3)
        {
            Application.Quit();
           
        }
    }
	public void levellerButon(int gelenlevel)
    {
        SceneManager.LoadScene(gelenlevel);
    }
}
