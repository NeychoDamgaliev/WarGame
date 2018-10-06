using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarCardGame
{
    class Player
    {
        private string _name { get ; set; }
        public string Name
        {
            get
            {
                return this._name;
            }
        }
        public int CardsCount
        {
            get
            {
                return this.cards.Count;
            }
        }
        private Queue<Card> cards = new Queue<Card>();

        public Player(string name) {
            this._name = name;
        }

        public bool hasCards()
        {
            return this.cards.Count != 0;
        }

        public Card getCardFromPlayer()
        {
            if(this.hasCards())
            {
                return this.cards.Dequeue();
            }
            return null;
        }
        public void addCardToPlayerDeck(Card card)
        {
            this.cards.Enqueue(card);
        }

        public string getPlayerInfo()
        {

            StringBuilder playerInfo = new StringBuilder();

            playerInfo
                .Append("Player: ")
                .Append(this._name)
                .Append(", has in hand: ");
            foreach (var card in cards)
            {
                playerInfo.Append(card.Name).Append(" ");
            }
            return playerInfo.ToString();
        }
    }
}
