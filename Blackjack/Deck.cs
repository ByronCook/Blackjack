using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    internal class Deck
    {
        public List<Card> Cards = new List<Card>();
        public int SectionSize = 14;
        public Deck()
        {
            CreateDeck();
        }

        private void CreateDeck()
        {
            foreach (var section in Enum.GetValues(typeof(Sections)))
            {
                for (int i = 2; i <= SectionSize; i++)
                {
                    Cards.Add(new Card
                    {
                        Section = (Sections) section,
                        Value = i,
                       // IsPicture = false
                    });
                }
            }

            Shuffle();
        }
        public Card DrawCard()
        {
            if (Cards == null)
            {
                return null;
            }

            var tempCard = Cards.First();
            Cards.Remove(tempCard);

            return tempCard;
        }

        private void Shuffle()
        {
            Cards = Cards.OrderBy(o => Guid.NewGuid().ToString()).ToList();
        }
    }
}
