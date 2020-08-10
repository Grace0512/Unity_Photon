using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerPropty : MonoBehaviour
{
    public PhotonView pv;
    public PlayerContrl PlayerCtr;
    public Sprite[] glasses;
    public Image myglasses;
    public int glassnum;




    void Start()
    {
        PlayerCtr = FindObjectOfType<PlayerContrl>();
        pv = GetComponent<PhotonView>();
       
        if (!pv.IsMine)
        {
            PlayerCtr = FindObjectOfType<PlayerContrl>();
            pv.transform.parent = PlayerCtr.transform;
        }
        if (pv.IsMine)
        {
            PlayerCtr= FindObjectOfType<PlayerContrl>();
            pv.transform.parent = PlayerCtr.transform;
        }

    }

    

    public void Glasses(int glasses)
    {
       pv.RPC("ChangeGlasses", RpcTarget.All, glasses);
    }

    


    [PunRPC]
    public void ChangeGlasses(int index)
    {
        glassnum = index;
        myglasses.sprite = glasses[index];
    }

    /*
    public void Glassesindex(int glasses)
    {
        pv.RPC("SaveGlasses", RpcTarget.All, glasses);
    }

    [PunRPC]
    public void SaveGlasses(int index)
    {
        glassnum = index;
    }
    */

}
