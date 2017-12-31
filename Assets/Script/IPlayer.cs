using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
	public interface IPlayer  {
		string  id { get; }
		
		EnumRole role { get; set; }
		int votedNum { get; set; }

		void Vote(List<IPlayer> players, Func<string> SelectPlayerId);
	}
}
