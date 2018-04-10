using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Xml.Serialization;
using System.IO;

namespace Broadcast
{
	public class Client {
		private UdpClient m_recv_udp;
		private Thread m_loop_thread;
		public delegate void BroadcastResultCallback(IPEndPoint iep,BroadcastResult br);
		public event BroadcastResultCallback OnRecvBroadcastResult;

	// Use this for initialization
		public Client () {
			m_recv_udp= new UdpClient (new IPEndPoint(IPAddress.Any, 8051));
			m_loop_thread = new Thread (RecvSeverInfo);
			StartBroadcast();
		}

		void StartBroadcast()
		{
			m_loop_thread.Start();
			SendBroadcast ();
		}

		public void SendBroadcast()
		{
			IPEndPoint iep = new IPEndPoint (IPAddress.Broadcast, 8050);
			MemoryStream ms = new MemoryStream();
			XmlSerializer xs = new XmlSerializer(typeof(BroadcastRequest));
			BroadcastRequest br = new BroadcastRequest ()
			{
				UserName=ArCard.GlobalUserInfo.UserName,
				UserID=ArCard.GlobalUserInfo.UserID
			};
			xs.Serialize(ms,br);
			ms.Position = 0;
			byte[] buffer = new byte[1024];
			int len=ms.Read (buffer, 0, 1024);
			UdpClient send_udp = new UdpClient ();
			send_udp.Send (buffer,len,iep);
			send_udp.Close ();
		}

		private void RecvSeverInfo()
		{
			IPEndPoint iep = new IPEndPoint (IPAddress.Any,0);
			XmlSerializer xs = new XmlSerializer (typeof(BroadcastResult));
			MemoryStream ms;
			byte[] data;
			while (true) {
				try{
					data=m_recv_udp.Receive (ref iep);
					ms = new MemoryStream ();
					ms.Write (data,0,data.Length);
					ms.Position=0;
				}catch {
					return;
				}
				BroadcastResult br=(BroadcastResult)xs.Deserialize (ms);
				if (OnRecvBroadcastResult != null)
					OnRecvBroadcastResult (iep,br);
			}
		}

		public void Destroy()
		{
			m_recv_udp.Close ();
		}
	}
}