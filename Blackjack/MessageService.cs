using System;

namespace Blackjack
{
    internal class MessageService
    {
        public void DisplayPictueCard(string name, string pictureValue, Sections section)
        {
            Console.WriteLine(name + ": " + pictureValue + " " + section);
        }

        public void DisplayValueCard(string name, int value, Sections section)
        {
            Console.WriteLine(name + ": " + value + " " + section);
        }
        public void DisplayBreakLine()
        {
            Console.WriteLine("\n");
        }

        public void DisplayHand(int handId)
        {
            Console.WriteLine("Hand " + handId +":");
        }

        public void DisplayPlayerWin(string name, int handId)
        {
            Console.WriteLine("Congratulations, " + name + " You have won with: hand " + handId + "!");
        }

        public void DisplayDealerWin(string name, int handId)
        {
            Console.WriteLine("The " + name + " has won against: hand " + handId + "!");  
        }

        public void DisplayDead()
        {
            Console.WriteLine("Oops you've died.");
        }

        public void DisplayTotalHandValue(int total)
        {
            Console.WriteLine("Total: " + total);
        }

        public void DisplayPlayerBlackjackWin(string name)
        {
            Console.WriteLine("Congratulations, you've won with Blackjack!");
        }

        public void DisplayHitChoice()
        {
            Console.WriteLine("Stay or hit?");
        }

        public void DisplaySplitChoice()
        {
            Console.WriteLine("Do you want to split your hand? Answer Yes/No");
        }

        public void DisplayResetGameOptions()
        {
            Console.WriteLine("Do you want to reset the game and start over?");
        }

        public void DisplaySuccessfullReset()
        {
            Console.WriteLine("The game has been reset successfully.");
        }
    }
}
