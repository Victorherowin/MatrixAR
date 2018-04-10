using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Serialization;

namespace Broadcast
{
	[XmlRoot("Result")]
	public class BroadcastResult {
		public string ServerName{ set; get;}
		public int PlayerNumber{ set; get;}
		public int MaxPlayerNumber{ set; get;}
	}
}