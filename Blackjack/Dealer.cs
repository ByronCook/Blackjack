using System;

namespace Blackjack
{
    internal class Dealer : IPerson
    {
        private Deck Deck { get; set; }
        public Hand Hand { get; set; }
        public string Name { get; set; }
        public Dealer()
        {
            Deck = new Deck();
            Hand = new Hand();
            Name = "Dealer";
        }

        public void Draw(Player player)
        {
            player.Hand.AddCardToHand(Deck.DrawCard());
            player.Hand.AddCardToHand(Deck.DrawCard());
            Hand.AddCardToHand(Deck.DrawCard());
            Hand.AddCardToHand(Deck.DrawCard());
        }
        public void Hit(Player player = null, Hand playerHand = null)
        {
            if (player != null)
            {
                if (playerHand != null && playerHand.CardTotal < 21 && playerHand.DoesStay == false)
                {
                    playerHand.AddCardToHand(Deck.DrawCard());
                }
            }
            else
            {
                Hand.AddCardToHand(Deck.DrawCard());
            }
        }

        public bool DoesHit(Hand playerHand)
        {
            if (Hand.CardTotal > playerHand.CardTotal - 1 || playerHand.IsDead())
            {
                return false;
            }
            return true;
        }
    }
}
