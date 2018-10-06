using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarCardGame
{
    class Deck
    {
        //private List<string> cards;
        private List<Card> cards;
        //public string[] CARD_TYPES = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        //private string[] CARD_COLOR = new string[] { "S", "H", "D", "C" };

        public Deck()
        {
            this.cards = new List<Card>();
            for (int cardColor = 0; cardColor < 4; cardColor++)
            {
                for (int cardType = 0; cardType < 13; cardType++)
                {
                    cards.Add(new Card(cardType,cardColor));
                }
            }
        }
        public int getCardsCount()
        {
            return this.cards.Count();
        }

        public Card getCard()
        {
            Random rnd = new Random();
            int cardIndex = rnd.Next(0, cards.Count);
            Card currentCard = cards[cardIndex];
            cards.RemoveAt(cardIndex);
            return currentCard;
        }

        public Boolean hasCards()
        {
            return this.cards.Count != 0;
        }
       


    }
    class Card
    {
        private static string[] CARD_TYPES = new string[] {"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        private static string[] CARD_COLOR = new string[] { "S", "H", "D", "C" };


        private string _name;
        public string Name
        {
            get
            {
                return this._name;
            }
        }

        private int _power;
        public int Power
        {
            get
            {
                return this._power;
            }
        }

        public Card(int cardType, int cardColor)
        {
            this._name = Card.CARD_TYPES[cardType] + Card.CARD_COLOR[cardColor];
            this._power = cardType;
        }

        public string getCardInfo()
        {
        
            return this._name;
        }
    }
}
