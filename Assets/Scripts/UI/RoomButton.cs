using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net;

namespace UI
{
    public class RoomButton : MonoBehaviour
    {
        public Button LastButton;
        public Button NextButton;
        public Button CreateHostButton;
        public Button RandomJoinServerButton;
        public Button FlushRoomButton;
        public Text PageMessage;
        public GameObject[] RoomMessages;

        private Broadcast.Client m_broadcast_client;

        void Start()
        {
            LastButton.onClick.AddListener(LastPage);
            NextButton.onClick.AddListener(NextPage);
            CreateHostButton.onClick.AddListener(CreateHost);
            FlushRoomButton.onClick.AddListener(FlushRoom);
            RandomJoinServerButton.onClick.AddListener(RandomJoinServer);

            m_broadcast_client = new Broadcast.Client();
            m_broadcast_client.OnRecvBroadcastResult += OnRecvRoomResult;

            UpdateItem();
            /*	AddItem (new RoomItem(){
                    RoomName="666",
                    PlayerNumber=1,
                    MaxPlayerNumber=8,
                    ServerIP="192.168.137.1"
                }
                );*/
        }

        void OnDestroy()
        {
            if (m_broadcast_client != null)
                m_broadcast_client.Destroy();
        }

        void FlushRoom()
        {
            items.Clear();
            m_broadcast_client.SendBroadcast();
        }

        void OnRecvRoomResult(IPEndPoint iep, Broadcast.BroadcastResult br)
        {
            AddItem(new RoomItem()
            {
                RoomName = br.ServerName,
                PlayerNumber = br.PlayerNumber,
                MaxPlayerNumber = br.MaxPlayerNumber,
                ServerIP = iep.Address.ToString()
            });
        }

        List<RoomItem> items = new List<RoomItem>();
        delegate void UpdateItemAction();
        Queue<UpdateItemAction> _action_queue = new Queue<UpdateItemAction>();

        public void AddItem(RoomItem item)
        {
            items.Add(item);
            if (items.Count < (page + 1) * 4 && items.Count > page * 4)
            {
                _action_queue.Enqueue(UpdateItem);
            }
        }

        int page = 0;
        void UpdateItem()
        {
            for (int i = page * 4, j = 0; i < (page + 1) * 4; ++i, ++j)
            {
                if (i < items.Count)
                {
                    foreach (var t in RoomMessages[j].GetComponentsInChildren<Text>())
                    {
                        if (t.name == "Text")
                            t.text = (i + 1).ToString();
                        if (t.name == "Text1")
                            t.text = items[i].RoomName;
                        if (t.name == "Text2")
                            t.text = items[i].PlayerNumber + "/" + items[i].MaxPlayerNumber;
                    }

                    foreach (var obj in RoomMessages[j].GetComponentsInChildren<Transform>())
                    {
                        if (obj.name == "Button")
                        {
                            obj.GetComponent<Image>().enabled = true;
                            obj.GetComponent<Button>().enabled = true;
                            obj.GetComponentInChildren<Text>().enabled = true;
                        }
                    }
                }
                else
                {
                    foreach (var t in RoomMessages[j].GetComponentsInChildren<Text>())
                    {
                        if (t.name == "Text")
                            t.text = "";
                        if (t.name == "Text2")
                            t.text = "";
                        if (t.name == "Text1")
                            t.text = "";
                    }
                    foreach (var obj in RoomMessages[j].GetComponentsInChildren<Transform>())
                    {
                        if (obj.name == "Button")
                        {
                            obj.GetComponent<Image>().enabled = false;
                            obj.GetComponent<Button>().enabled = false;
                            obj.GetComponentInChildren<Text>().enabled = false;
                        }
                    }
                }
            }
            PageMessage.text = (page + 1) + "/" + (items.Count / 4 + 1);
        }

        void LastPage()
        {
            if (page <= 0)
                return;
            page--;
            UpdateItem();
        }

        void NextPage()
        {
            if (page >= items.Count / 4)
                return;
            page++;
            UpdateItem();
        }

        void CreateHost()
        {
            JoinServerArgs.isCreateHost = true;
            SceneManager.LoadSceneAsync("003-local-multiplay");
        }

        void RandomJoinServer()
        {
            if (items.Count == 0)
                return;
            JoinServerArgs.isCreateHost = false;
            JoinServerArgs.ServerIP = items[Random.Range(0, items.Count - 1)].ServerIP;
            SceneManager.LoadSceneAsync("003-local-multiplay");
        }

        public void JoinServer(int select)
        {
            JoinServerArgs.isCreateHost = false;
            JoinServerArgs.ServerIP = items[select].ServerIP;
            SceneManager.LoadSceneAsync("003-local-multiplay");
        }

        void Update()
        {
            while (_action_queue.Count != 0)
            {
                _action_queue.Dequeue()();
            }
        }
    }
}