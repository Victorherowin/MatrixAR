using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net.Sockets;
using System.Threading;
using System.Xml.Serialization;
using System.IO;
using System.Net;

namespace Broadcast
{
	public class Server{

		public string ServerName=ArCard.GlobalUserInfo.UserName+"的房间";
		private Thread m_loop_thread;
		private UdpClient m_server_udp;

		// Use this for initialization
		public Server() {
			m_server_udp = new UdpClient (new IPEndPoint(IPAddress.Any,8050));
			m_loop_thread = new Thread (RecvBroadcast);
			m_loop_thread.Start ();
		}

		void RecvBroadcast()
		{
			while (true) {
				try{
					IPEndPoint iep = new IPEndPoint (IPAddress.Any,0);
					byte[] data=m_server_udp.Receive (ref iep);
					XmlSerializer xs = new XmlSerializer (typeof(BroadcastRequest));
					MemoryStream ms = new MemoryStream ();
					ms.Write (data,0,data.Length);
					ms.Position=0;
					BroadcastRequest br=(BroadcastRequest)xs.Deserialize (ms);

					SendServerInfo (iep);
				}
				catch
				{
					return;
				}

			}
		}

		private void SendServerInfo(IPEndPoint iep)
		{
			MemoryStream ms = new MemoryStream();
			XmlSerializer xs = new XmlSerializer(typeof(BroadcastResult));
			BroadcastResult broadcast_result = new BroadcastResult ()
			{
				ServerName=this.ServerName,
				PlayerNumber=NetworkManager2.Instance.RoomPlayerNum,
				MaxPlayerNumber=8
			};
			xs.Serialize(ms,broadcast_result);
			ms.Position = 0;
			byte[] buffer = new byte[1024];
			int len=ms.Read (buffer, 0, 1024);

			UdpClient send_udp = new UdpClient ();
			send_udp.Send (buffer,len,new IPEndPoint(iep.Address,8051));
			send_udp.Close ();
		}

		private static string GetIPAddress()
		{
			AndroidJavaClass jc = new AndroidJavaClass("com.arcard.wifi.WifiController");

			// 调用方法 
			string ret = jc.CallStatic<string>("GetIPAddress");
			return ret;
		}

		public void Destroy()
		{
			m_server_udp.Close ();
		}
	}
}