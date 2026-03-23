using System.Collections.Generic;

namespace WarGame.Core
{   
    //Stores all players and their corresponding hands
    public class PlayerHands
    {        
        //Key = Player name, Value = Their hand of cards
        public Dictionary<string, Hand> Hands { get; } = new Dictionary<string, Hand>();
    }
}