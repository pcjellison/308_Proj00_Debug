using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jellison_Payton_Debug
{
    class Dealer
    {
        public List<Card> cards = new List<Card>();

        public Dealer()
        {

        }

        public void Add(Card card)
        {
            cards.Add(card);
        }

        public List<Card> GetHand
        {
            get
            {
                return cards;
            }
        }
    }
}
