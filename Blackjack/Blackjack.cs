using System;
using System.Linq;

namespace Blackjack
{
    internal class Blackjack
    {
        private Dealer Dealer { get; set; }
        private Player Player { get; set; }
        private readonly MessageService _msgService = new MessageService();
        public Blackjack()
        {
            Dealer = new Dealer();
            Player = new Player();
        }

        public void Restart()
        {
            Dealer = new Dealer();
            Player = new Player();
            StartGame();
        }
        public void StartGame()
        {
            Dealer.Draw(Player);
            if (Player.Hand.CheckForBlackjack())
            {
                EndGame();
                return;
            }
            PlayerService();
            if (Player.IsSplit)
            {
                Hand[] playerHands = {Player.Hand, Player.SplitHand, Player.SplitHand1, Player.SplitHand2};
                foreach (var hand in playerHands.Where(hand => hand != null))
                {
                    DealerService(hand);
                }
            }
            else
            {
                DealerService(Player.Hand);
            }

            EndGame();
            ResetGame();
        }

        private void PlayerService()
        {
            Player.Hand.DisplayHand(Player.Name);

            //foreach (var card in Player.Hand.CardList)
            //{
            //    card.Value = 5;
            //}

            if (Player.Hand.HasSameValue())
            {
                if (Player.Split(Player.Hand))
                {
                    HitTwoHands(Player.Hand, Player.SplitHand);
                    Display(Player.Hand, Player.SplitHand);

                    if (Player.Hand.HasSameValue())
                    {
                        if (Player.Split(Player.Hand))
                        {
                            HitTwoHands(Player.Hand, Player.SplitHand1);
                            Display(Player.Hand, Player.SplitHand1);
                        }
                    }

                    if (Player.SplitHand.HasSameValue())
                    {
                        if (Player.Split(Player.SplitHand))
                        {
                            HitTwoHands(Player.SplitHand, Player.SplitNumber == 1 ? Player.SplitHand1 : Player.SplitHand2);
                            Display(null, Player.SplitHand, Player.SplitNumber == 1 ? Player.SplitHand1 : null, Player.SplitNumber == 1 ? null : Player.SplitHand2);
                        }
                    }
                }
            }
            if(Player.SplitNumber == 0)
            {
                HitPlayer(Player.Hand);
                Display(Player.Hand);
            }
        }

        private void HitPlayer(Hand hand)
        {
            while (Player.DoesHit(hand))
            {
                Dealer.Hit(Player, hand);
                hand.DisplayHand(Player.Name);
                if (!hand.IsDead()) continue;
                _msgService.DisplayDead();
                break;
            }

        }

        private void HitTwoHands(Hand hand, Hand hand2)
        {
            HitPlayer(hand);
            _msgService.DisplayBreakLine();
            HitPlayer(hand2);
        }

        private void Display(Hand hand1 = null, Hand hand2 = null, Hand hand3 = null, Hand hand4 = null)
        {
            if (hand1 != null)
            {
                _msgService.DisplayHand(1);
                hand1.DisplayHand(Player.Name);
                _msgService.DisplayBreakLine();
            }

            if (hand2 != null)
            {
                _msgService.DisplayHand(2);
                hand2.DisplayHand(Player.Name);
                _msgService.DisplayBreakLine();
            }
            if (hand3 != null)
            {
                _msgService.DisplayHand(3);
                hand3.DisplayHand(Player.Name);
                _msgService.DisplayBreakLine();
            }
            if (hand4 != null)
            {
                _msgService.DisplayHand(4);
                hand4.DisplayHand(Player.Name);
                _msgService.DisplayBreakLine();
            }
        }

        private void DealerService(Hand playerHand)
        {
            Dealer.Hand.DisplayHand(Dealer.Name);
            while (Dealer.DoesHit(playerHand))
            {
                Dealer.Hit();
                Dealer.Hand.DisplayHand(Dealer.Name);
                if (Dealer.Hand.IsDead()) return;
            }
        }
        private void CheckWinner(Hand hand, int handId = 0)
        {
            if (hand.IsDead() || hand.CardTotal <= Dealer.Hand.CardTotal && Dealer.Hand.CardTotal <= 21)
            {
                _msgService.DisplayDealerWin(Dealer.Name, handId);
            }
            else
            {
                _msgService.DisplayPlayerWin(Player.Name, handId);
            }
        }

        private void EndGame()
        {
            if (Player.Hand.CheckForBlackjack())
            {
                _msgService.DisplayPlayerBlackjackWin(Player.Name);
                return;
            }
            if (Player.IsSplit)
            {
                Hand[] playerHands = {Player.Hand, Player.SplitHand, Player.SplitHand1, Player.SplitHand2};
                for (var i = 0; i < playerHands.Length; i++)
                {
                    if (playerHands[i] != null)
                    {
                        CheckWinner(playerHands[i], i + 1);
                    }
                }
            }
            else
            {
                CheckWinner(Player.Hand);
            }
        }
        public void ResetGame()
        {
            _msgService.DisplayResetGameOptions();
            var input = Console.ReadLine();
            while (CheckResetInput(input))
            {
                if (input == "yes" || input == "Yes")
                {
                    _msgService.DisplaySuccessfullReset();
                    _msgService.DisplayBreakLine();
                    Restart();
                }
                else
                {
                    return;
                }
            }
        }

        public bool CheckResetInput(string input)
        {
            switch (input)
            {
                case "Yes":
                case "yes":
                case "No":
                case "no":
                    return true;
            }
            return false;
        }
    }
}
