using System;
using System.Linq;

namespace Blackjack
{
    internal class Player : IPerson
    {
        public Hand Hand { get; set; }
        public string Name { get; set; }
        public Hand SplitHand { get; set; }
        public Hand SplitHand1 { get; set; }
        public Hand SplitHand2 { get; set; }
        public bool IsSplit { get; set; }
        public int SplitNumber;
        private readonly MessageService _msgService = new MessageService();

        public Player()
        {
            Hand = new Hand();
            Name = "Player";
            SplitNumber = 0;
        }

        public bool DoesHit(Hand hand)
        {
            if ((hand.DoesStay != true && !hand.IsDead()) || (hand.CardTotal == 21))
            {
                _msgService.DisplayHitChoice();
                var input = Console.ReadLine();

                while (CheckInput(input))
                {
                    switch (input)
                    {
                        case "stay":
                        case "Stay":
                            hand.Stays(true);
                            return false;
                        case "hit":
                        case "Hit":
                            return true;
                    }
                }
            }
            return false;
        }

        private static bool CheckInput(string input)
        {
            switch (input)
            {
                case "stay":
                case "Stay":
                case "hit":
                case "Hit":
                    return true;
                default:
                    return false;
            }
        }

        public bool Split(Hand hand)
        {
            switch (SplitNumber)
            {
                case 0:
                    if (SplitCards(hand, SplitHand = new Hand()))
                    {
                        SplitNumber++;
                        return true;
                    }
                    return false;
                case 1:
                    if (SplitCards(hand, SplitHand1 = new Hand()))
                    {
                        SplitNumber++;
                        return true;
                    }
                    return false;
                case 2:
                    if (SplitCards(hand, SplitHand2 = new Hand()))
                    {
                        SplitNumber++;
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
        }

        private bool SplitCards(Hand hand, Hand newHand1)
        {
            if (hand.CardList[0].Value == hand.CardList[1].Value && hand.CardList.Count == 2)
            {
                _msgService.DisplaySplitChoice();
                var split = Console.ReadLine();
                while (CheckSplitInput(split))
                {
                    if (split == "Yes" || split == "yes")
                    {
                        newHand1.AddCardToHand(Hand.CardList.Last());
                        IsSplit = true;
                        hand.RemoveCardFromHand(Hand.CardList.Last());
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }

        private static bool CheckSplitInput(string input)
        {
            switch (input)
            {
                case "yes":
                case "Yes":
                case "no":
                case "No":
                    return true;
                default:
                    return false;
            }
        }
    }
}
