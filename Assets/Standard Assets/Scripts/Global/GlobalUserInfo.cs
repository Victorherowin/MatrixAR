using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArCard
{
	public static class GlobalUserInfo  {
		/// <summary>
		/// 局域网联机使用的用户名
		/// </summary>
		static public string UserName{ get; set;}
		/// <summary>
		/// 局域网联机使用的用户ID
		/// </summary>
		static public string UserID{ get; set;}
		/// <summary>
		/// 用于互联网登陆的账号
		/// </summary>
		static public string UserAccount{ get; set;}
		/// <summary>
		/// 用于互联网登陆的密码
		/// </summary>
		/// <value>The user pass word.</value>
		static public string UserPassWord{ get; set;}
		/// <summary>
		/// 是否保存账户信息
		/// </summary>
		static public bool IsSave{ get; set;}

		static GlobalUserInfo()
		{
			UserID="unkown";
		}

	}
}