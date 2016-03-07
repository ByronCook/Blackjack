using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    internal class Hand
    {
        public List<Card> CardList { get; set; }
        public int CardTotal { get; set; }
        public bool DoesStay { get; set; }

        public Hand()
        {
            CardList = new List<Card>();
        }
        public void CalculateTotal()
        {
            CardTotal = 0;
            foreach (var card in CardList)
            {
                if (card.IsPicture)
                {
                    card.Value = 10;
                }
                if (card.IsAce)
                {
                    DecideAceValue(card);
                }
                CardTotal += card.Value;
            }
        }

        private void DecideAceValue(Card card)
        {
            card.Value = CardTotal <= 10 ? 11 : 1;
        }

        public void AddCardToHand(Card card)
        {
            if (card != null)
            {
                CardList.Add(card);
            }
            else
            {
                throw new ArgumentNullException("card");
            }
        }

        public void RemoveCardFromHand(Card card)
        {
            if (card != null)
            {
                CardList.Remove(card);
            }
        }

        public void DisplayHand(string name)
        {
            foreach (var card in CardList)
            {
                card.Display(name);
            }
            CalculateTotal();
            new MessageService().DisplayTotalHandValue(CardTotal);
        }

        public bool IsDead()
        {
            CalculateTotal();
            return CardTotal > 21;
        }

        public bool CheckForBlackjack()
        {
            return CardList.Count == 2 && CardTotal == 21;
        }

        public bool HasSameValue()
        {
            if (CardList.Count == 2)
            {
                if (CardList.First().Value == CardList.Last().Value)
                {
                    return true;
                }
            }
            return false;
        }

        public void Stays(bool stay)
        {
            if (stay)
            {
                DoesStay = true;
            }
        }
    }
}
