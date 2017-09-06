using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jellison_Payton_Debug
{
    class Program
    {
        static void Main(string[] args)
        {
            int pot = 100;
            int bet;
            int dealerNumInput;
            int playerNumInput;
            int playerHandValue = 0;
            int dealerHandValue = 0;
            int wins = 0;
            int losses = 0;
            int ties = 0;
            string surr;

            Random rand = new Random();
            Deck deck = new Deck();
            Player player = new Player();
            Dealer dealer = new Dealer();

            List<Card> playerHand = new List<Card>();
            List<Card> dealerHand = new List<Card>();

            uxNewGame();
            void uxNewGame()
            {
                dealerNumInput = 2;
                playerNumInput = 2;
                playerHand.Clear();
                dealerHand.Clear();
                playerHandValue = 0;
                dealerHandValue = 0;
                wins = 0;
                losses = 0;
                ties = 0;
                surr = "n";
                deck.createDeck();

                if(pot <= 0)
                {
                    Console.WriteLine("You have no funds left.");
                    uxQuit();
                }


                Console.WriteLine("===== NEW GAME =====");
                Console.WriteLine(" You have: $" + pot);
                Console.WriteLine("How much do you bet: ");

                bet = Convert.ToInt32(Console.ReadLine());

                if(bet > pot)
                {
                    Console.WriteLine("You don't have that much money");
                    uxNewGame();
                }
                Console.WriteLine("You bet $" + bet);

                uxPlayerCardInput();
                uxDealerCardInput();
                uxPlayerHandDisplay();
            }

            void uxPlayerCardInput()
            {
                string userInput;
                Card card = new Card();
                for (int i = 0; i < playerNumInput; i++)
                {
                    Console.WriteLine("Input card for player (3H, AD, TC, etc. or XX to draw from deck): ");
                    userInput = Console.ReadLine();
                    char[] inputArray = userInput.ToCharArray();
                    if (userInput == "XX" || userInput == "xx")
                    {
                        int num = rand.Next(1, 53);
                        Card c = deck.RandomDeal(num);
                        player.Add(c);
                    }
                    else
                    {
                        char rank = inputArray[0];
                        char suit = inputArray[1];

                        Card c = (deck.Deal(rank, suit));
                        player.Add(c);
                    }
                }
                if(playerNumInput < 2)
                {
                    uxPlayerHandDisplay();
                }
                playerHand = player.GetHand;
                playerNumInput = 1;
            }
            void uxDealerCardInput()
            {
                for (int i = 0; i < dealerNumInput; i++)
                {

                    Console.WriteLine("Input card for dealer (3H, AD, TC, etc. or XX to draw from deck): ");
                    string userInput = Console.ReadLine();
                    char[] inputArray = userInput.ToCharArray();
                    if (userInput == "XX" || userInput == "xx")
                    {
                        int num = rand.Next(1, 53);
                        Card c = deck.RandomDeal(num);
                        dealer.Add(c);
                    }
                    else
                    {
                        char rank = inputArray[0];
                        char suit = inputArray[1];

                        Card c = deck.Deal(rank, suit);
                        dealer.Add(c);
                    }
                }
                if(dealerNumInput < 2)
                {
                    uxDealerHandDisplay();
                }
                dealerNumInput = 1;
                dealerHand = dealer.GetHand;
            }

            void uxPlayerHandDisplay()
            {
                Console.Write("Your hand = ");
                for(int i = 0; i <playerHand.Count; i++)
                {
                    Console.Write(playerHand[i].Rank + playerHand[i].Suit + " ");
                    playerHandValue += playerHand[i].Value;
                }
                Console.WriteLine(", Hand Value: " + playerHandValue);
                if(playerHandValue == 21)
                {
                    uxGameOver();
                }
                else if(playerHandValue > 21)
                {
                    uxGameOver();
                }
                Console.WriteLine("Do you want to surrender <Y or N>?: ");
                playerHandValue = 0;
                surr = Console.ReadLine();
                if (surr == "y" || surr == "Y")
                {
                    uxQuit();
                }
                else
                {
                    Console.Write("Will you HIT or STAND <H or S>? : ");
                    string ans = Console.ReadLine();

                    if (ans == "h" || ans == "H")
                    {
                        uxPlayerHit();
                    }
                    else
                    {
                        uxPlayerStand();
                    }
                }
            }

            void uxDealerHandDisplay()
            {
                Console.Write("Dealer's Hand: ");
                if (dealerHand.Count == 2)
                {
                    Console.Write(dealerHand[0].Rank + dealerHand[0].Suit + " ");
                    Console.Write(" XX");
                    Console.WriteLine(" ");
                    for (int i = 0; i < dealerHand.Count; i++)
                    {
                        dealerHandValue += dealerHand[i].Value;
                    }
                    if (dealerHandValue >= 17)
                    {
                        uxGameOver();
                    }
                    else
                    {
                        dealerHandValue = 0;
                        uxPlayerStand();
                    }
                }
                else
                {
                    for (int i = 0; i < dealerHand.Count; i++)
                    {
                        Console.Write(dealerHand[i].Rank + dealerHand[i].Suit + " ");
                        dealerHandValue += dealerHand[i].Value;
                    }
                    Console.WriteLine(",  Hand Value: " + dealerHandValue);
                    if (dealerHandValue >= 17)
                    {
                        uxGameOver();
                    }
                    else
                    {
                        dealerHandValue = 0;
                        uxPlayerStand();
                    }
                    dealerHandValue = 0;
                }
            }

            void uxPlayerHit()
            {
                if(playerHandValue < 21 && dealerHandValue < 21)
                {
                    uxPlayerCardInput();
                }
                else
                {
                    uxGameOver();
                }
            }

            void uxPlayerStand()
            {
                Console.WriteLine(" ");
                Console.WriteLine("Now, Dealer's turn");
                uxDealerCardInput();
            }

            void uxGameOver()
            {
                Console.WriteLine(" ");
                double award = bet;
                int tempBet = bet;
                if (playerHandValue == 21)
                {
                    award = (Convert.ToDouble(tempBet) * 2.5);
                }
                if (playerHandValue == 21 && dealerHandValue != 21)
                {
                    Console.WriteLine("You won and Got $" + award + " from Dealer");
                    wins++;
                    Console.WriteLine("You Won " + wins + " times, Lost " + losses + " times, and Tied " + ties + " times");
                    tempBet = Convert.ToInt32(award);
                    pot += tempBet;
                    bet = 0;
                }
                else if (playerHandValue > 21)
                {
                    Console.WriteLine("You Bust");
                    losses++;
                    Console.WriteLine("Dealer won and Got $" + bet + " from User");
                    Console.WriteLine("You Won " + wins + " times, Lost " + losses + " times, and Tied " + ties + " times");
                    pot -= bet;
                    bet = 0;
                }
                else if (dealerHandValue > 21)
                {
                    Console.WriteLine("Dealer Busted");
                    wins++;
                    Console.WriteLine("You won and Got $" + bet + " from Dealer");
                    Console.WriteLine("You Won " + wins + " times, Lost " + losses + " times, and Tied " + ties + " times");
                    pot += bet;
                    bet = 0;
                }
                else
                {

                    if (playerHandValue > dealerHandValue)
                    {
                        Console.WriteLine("You won and Got $" + bet + " from Dealer");
                        wins++;
                        Console.WriteLine("You Won " + wins + " times, Lost " + losses + " times, and Tied " + ties + " times");
                        pot += bet;
                        bet = 0;
                    }
                    else if (dealerHandValue > playerHandValue)
                    {
                        Console.WriteLine("Dealer won and Got $" + bet + " from User");
                        losses++;
                        Console.WriteLine("You Won " + wins + " times, Lost " + losses + " times, and Tied " + ties + " times");
                        pot -= bet;
                        bet = 0;
                    }
                    else
                    {
                        Console.WriteLine("Stand Off");
                        Console.WriteLine("You Won " + wins + " times, Lost " + losses + " times, and Tied " + ties + " times");
                    }
                }
                uxContinue();
            }

            void uxContinue()
            {
                Console.WriteLine("More Game <Y or N>? : ");
                string ans = Console.ReadLine();

                if (ans == "y" || ans == "Y")
                {
                    uxNewGame();
                }
                else
                {
                    uxQuit();
                }
            }
            void uxQuit()
            {
                Console.WriteLine("Thaks for Playing");
            }
        }
    }
}
