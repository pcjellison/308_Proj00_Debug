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
            int numInput = 2;

            Random rand = new Random();
            Deck deck = new Deck();
            Player player = new Player();
            Dealer dealer = new Dealer();
            uxNewGame();
            void uxNewGame()
            {
                deck.createDeck();
                Console.WriteLine("===== NEW GAME =====");
                Console.WriteLine(" You have: $" + pot);
                Console.WriteLine("How much do you bet: ");

                bet = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("You bet $" + bet);

                uxCardInput();
            }

            void uxCardInput()
            {
                string userInput;
                Card card = new Card();
                for (int i = 0; i < numInput; i++)
                {
                    Console.WriteLine("Input card for player (3H, AD, TC, etc. or XX to draw from deck): ");
                    userInput = Console.ReadLine();
                    if(userInput != "XX" || userInput != "xx")
                    {
                        char rank = userInput[0];
                        char suit = userInput[1];

                        player.Add(deck.Deal(rank, suit));
                    }
                    else
                    {
                        int num = rand.Next(1, 53);
                        player.Add(deck.RandomDeal(num));
                    }
                    Console.WriteLine("Input card for dealer (3H, AD, TC, etc. or XX to draw from deck): ");
                    userInput = Console.ReadLine();
                    if(userInput != "XX" || userInput != "xx")
                    {
                        char rank = userInput[0];
                        char suit = userInput[1];

                        dealer.Add(deck.Deal(rank, suit));
                    }
                    else
                    {
                        int num = rand.Next(1, 53);
                        dealer.Add(deck.RandomDeal(num));
                    }
                }
            }
        }
    }
}
