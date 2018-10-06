using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarCardGame
{
    static class Constants
    {
        public const int DELAY_TIME_IN_MILISECONDS = 1000;
        public const int START_GAME_COUNTDOWN_INTERVAL = 3;
    }
    class Program
    {
        private static List<Player> players = new List<Player>();
        private static Deck deck = new Deck();

        static void Main(string[] args)
        {
            gameStart();
        }

        private static void gameStart()
        {
            printWellcomeScreen();
            createPlayers();
            givePlayersCardsFromDeck();
            battleRound();
        }

        private static void battleRound()
        {
            Player winner = null;
            List<Card> cardsOnTable = new List<Card>();
            List<Card> p1CardsOnTable = new List<Card>();
            List<Card> p2CardsOnTable = new List<Card>();

            Player p1 = players[0];
            Player p2 = players[1];

            int round = 1;
            while (true)
            {
                p1CardsOnTable.Clear();
                p2CardsOnTable.Clear();
                Card p1Card = null;
                Card p2Card = null;
                Player roundWinner = null;

                if (!p1.hasCards())
                {
                    winner = players[1];
                    break;   
                }
                if (!p2.hasCards())
                {
                    winner = players[0];
                    break;
                }
                    

                p1Card = p1.getCardFromPlayer();
                p2Card = p2.getCardFromPlayer();

                p1CardsOnTable.Add(p1Card);
                p2CardsOnTable.Add(p2Card);

                Console.WriteLine($"{p1.Name} ( {p1.CardsCount} cards) draws: {p1Card.Name}" );
                Console.WriteLine($"{p2.Name} ( {p2.CardsCount} cards) draws: {p2Card.Name}");

                if (p1Card.Power >p2Card.Power)
                {
                    roundWinner = p1;    
                } 
                else if (p1Card.Power < p2Card.Power)
                {
                    roundWinner = p2;
                }
                else
                {
                    // WAR CASE
                    Console.WriteLine("W A R ! ! !");
                    while (true)
                    {
                        try
                        {
                            p1CardsOnTable.Add(p1.getCardFromPlayer());
                            p1CardsOnTable.Add(p1.getCardFromPlayer());
                            p1CardsOnTable.Add(p1.getCardFromPlayer());
                        }
                        catch (Exception ex) {; }

                        try
                        {
                            p2CardsOnTable.Add(p2.getCardFromPlayer());
                            p2CardsOnTable.Add(p2.getCardFromPlayer());
                            p2CardsOnTable.Add(p2.getCardFromPlayer());
                        }
                        catch (Exception ex) {; }

                        p1CardsOnTable.RemoveAll(el => el == null);
                        p2CardsOnTable.RemoveAll(el => el == null);
                    
                        Card p1LastCard = p1CardsOnTable[p1CardsOnTable.Count - 1];
                        Card p2LastCard = p2CardsOnTable[p2CardsOnTable.Count - 1];
                        if (p1LastCard.Power > p2LastCard.Power)
                        {
                            roundWinner = p1;
                            break;
                        }
                        else if (p1LastCard.Power < p2LastCard.Power)
                        {
                            roundWinner = p2;
                            break;
                        }
                    }
                }

                
                Console.WriteLine($"Round {round++}: {roundWinner.Name} takes all!!!");
                p1CardsOnTable.ForEach(c => roundWinner.addCardToPlayerDeck(c));
                p2CardsOnTable.ForEach(c => roundWinner.addCardToPlayerDeck(c));

                if(round >= 1000)
                {
                    break;
                }
            }
            if (winner != null)
            {
                Console.Clear();
                Console.WriteLine($"{winner.Name} is W I N S the game in {round} turns!!! ");
            }
            else
            {
                Console.WriteLine("DEUCE!!!   More than 1000 rouns");
            }
        }

        private static void givePlayersCardsFromDeck()
        {
            int playersCount = players.Count;
            int initialDeckCardsCount = deck.getCardsCount();
            for (int i = 0; i < initialDeckCardsCount; i++)
            {
                int playerIndex = i % playersCount;
                players[playerIndex].addCardToPlayerDeck(deck.getCard());
            }
            printPlayerInfo();
            Console.WriteLine("Let the GAME BEGIN!!!");
        }

        private static void createPlayers()
        {
            List<string> playerNames = new List<string>();
           
            while (players.Count < 2)
            {
                if (players.Count > 0)
                {
                    Console.WriteLine("(press Enter to start playing... or)");
                }
                Console.Write($"enter new Player({players.Count + 1}) name: ");
                String playerName = Console.ReadLine();
                Console.Clear();
                if (playerName != "")
                {
                    if(playerNames.Contains(playerName))
                    {
                        errorMessage("Error!Player with such name already exist!!!\r\nPlease choose another name:!!!");
                        
                        continue;
                    }
                    if(playerName.Length <=2)
                    {
                        errorMessage("Error!Player name must be atleast 3 symbols!!!\r\nPlease choose another name:!!!");

                        continue;
                    }
                    playerNames.Add(playerName);
                    players.Add(new Player(playerName));
                    continue;
                }
                else
                {
                    if(players.Count == 0)
                    {
                        playerNames.Add("PC");
                        players.Add(new Player("PC"));
                        continue;
                    }
                    else  if(players.Count==1)
                    {
                        String name = null;
                        if (playerNames.Contains("PC")) {
                            name="PC2";
                        }
                        else
                        {
                            name = "PC";
                        }

                        players.Add(new Player(name));
                        continue;
                    }
                    break;
                }
            }

        }

        private static void printPlayerInfo()
        {
            foreach (var player in players)
            {
                Console.WriteLine(player.getPlayerInfo());
            }
        }

        private static void errorMessage(string message)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(message);
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
        }

        private static void printWellcomeScreen()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("        #########################################");
            Console.WriteLine("        #---------------------------------------#");
            Console.WriteLine("        #----WAR card game (console version)----#");
            Console.WriteLine("        #---------( 2 players allowed )---------#");
            Console.WriteLine("        #---------------------------------------#");
            Console.WriteLine("        #########################################");


            for (int i = Constants.START_GAME_COUNTDOWN_INTERVAL; i >0; i--)
            {
                Console.SetCursorPosition(10, 10);
                Console.WriteLine($"Starting in ({i})sec");
                System.Threading.Thread.Sleep(Constants.DELAY_TIME_IN_MILISECONDS);
            }
            Console.Clear();
        }
    }
}
