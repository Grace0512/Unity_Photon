using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using ExitGames.Client.Photon;
using System;

public class ScenceManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public GameObject PlayerCtr;
    public PlayerContrl Player;
    
    public int timer = 5;
    public Text timeCount;
    public GameObject ButAgain;
    public Text winner;
    
    int turn;


    void Start()
    {
        if (PhotonNetwork.CountOfPlayersInRooms != 0)
        {
            GameObject Player2 =  PhotonNetwork.Instantiate("Player2", new Vector3(900,400,0), Quaternion.identity);
            Player2.transform.parent = PlayerCtr.transform;
            Player.player = Player2;

        }
           
       else
        {
            GameObject Player1 = PhotonNetwork.Instantiate("Player1", new Vector3(300, 400, 0), Quaternion.identity);
            Player1.transform.parent = PlayerCtr.transform;
            Player1.transform.rotation = new Quaternion(0, -180, 0, 0);
            Player.player = Player1;
        }
            
    }

    private void StartTurn()
    {
        turn = UnityEngine.Random.RandomRange(0, 2);
        if (turn % 2 == 0)
        {
            winner.text = "左方攻";
        }
        else
        {
            winner.text = "右方攻";
        }
        byte evntcode2 = 2;
        object[] obj2 = new object[] { winner.text };
        RaiseEventOptions EventOption2 = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(evntcode2, obj2, EventOption2, SendOptions.SendReliable);

    }



    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        if (PhotonNetwork.PlayerList.Length == 2 && PhotonNetwork.IsMasterClient)
        {
            StartTurn();
            StartCoroutine(Timer());
            
        }
    }


    IEnumerator Timer()
    {
        
        WaitForSecondsRealtime ww = new WaitForSecondsRealtime(1f);
        while (timer > 0)
        {
            yield return ww;
            timer--;
            timeCount.text = timer.ToString();
            byte evntcode = 1;
            object[] obj = new object[] { timer };
            RaiseEventOptions EventOption = new RaiseEventOptions { Receivers = ReceiverGroup.All };

            PhotonNetwork.RaiseEvent(evntcode, obj, EventOption, SendOptions.SendReliable);
        }
        if (timer <= 0)
        {
            Player.Compare();
            ButAgain.SetActive(true);
        }

    }

    public void Win()
    {
        if (timer <= 0)
        {
            
            if (turn % 2 == 0)
            {
                winner.text = "左方勝利";

            }
            else
            {
                winner.text = "右方勝利";

            }
            byte evntcode3 =3;
            object[] obj3 = new object[] { winner.text };
            RaiseEventOptions EventOption3 = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent(evntcode3, obj3, EventOption3, SendOptions.SendReliable);
        } 
           

    }
    public void End()
    {
        if (timer <= 0)
        {
            winner.text = "平手";
        byte evntcode3 = 3;
        object[] obj3 = new object[] { winner.text };
        RaiseEventOptions EventOption3 = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(evntcode3, obj3, EventOption3, SendOptions.SendReliable);
        }
       


    }

    public void Restart()
    {
        
        timer = 6;
        StartTurn();
        StartCoroutine(Timer());
        ButAgain.SetActive(false);

    }


    public void OnEvent(EventData photonEvent)
    {
        /*
        switch (photonEvent.Code)
        {
            case 1:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    timeCount.text = data[0].ToString();
                    break;
                }
            case 2:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    timeCount.text = data[0].ToString();
                    break;
                }
        }
        */
        if (photonEvent.Code == 1)
        {
            // 先轉出型態 將複數陣列轉成單一
            object[] date = (object[])photonEvent.CustomData;
            // 將date[0] 顯示在TIMER上
            timeCount.text = date[0].ToString();

        }
        if (photonEvent.Code == 2)
        {
            // 先轉出型態 將複數陣列轉成單一
            object[] date2 = (object[])photonEvent.CustomData;

            winner.text = date2[0].ToString();

        }
        if (photonEvent.Code == 3)
        {
            // 先轉出型態 將複數陣列轉成單一
            object[] date3 = (object[])photonEvent.CustomData;

            winner.text = date3[0].ToString();

        }


    }

   public override void OnEnable()

    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
