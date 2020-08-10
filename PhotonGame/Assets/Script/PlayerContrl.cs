using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerContrl : MonoBehaviour
{
   
    public Sprite[] glasses;

    public GameObject player;
    public ScenceManager sm;



    public void WearGlasses1(int index)
    {
        player.GetComponent<PlayerPropty>().Glasses(index);
           
    }

    public void Compare()
    {
        int A = gameObject.transform.GetChild(0).GetComponent<PlayerPropty>().glassnum;
        int B = gameObject.transform.GetChild(1).GetComponent<PlayerPropty>().glassnum;
        
        if(A==B)
        {
            sm.Win();
        }
        else
        {
            sm.End();
        }
    }

   
   
   
}