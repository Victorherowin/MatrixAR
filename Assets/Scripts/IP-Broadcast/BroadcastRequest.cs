using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.Xml.Serialization;

namespace Broadcast
{
	[XmlRoot("Request")]
	public class BroadcastRequest  {
		public string UserName{ set; get;}
		public string UserID{ set; get;}
	}
}
