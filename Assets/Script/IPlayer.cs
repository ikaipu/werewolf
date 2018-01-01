using System;
using System.Collections.Generic;

namespace Script
{
	public interface IPlayer  {
		string  id { get; }
		
		EnumRole role { get; set; }
		string voteFor { get; set; }
		bool isDead { get; set; }

		void Vote(List<IPlayer> players, Func<string> SelectPlayerId);
	}
}
