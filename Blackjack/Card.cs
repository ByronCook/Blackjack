using System;

namespace Blackjack
{
    internal class Card
    {
        public Sections Section { get; set; }
        public int Value { get; set; }
        public bool IsPicture { get; set; }
        public bool IsAce { get; set; }
        private string PictureValue { get; set; }
        private readonly MessageService _msgService = new MessageService();

        public void Display(string whoIs)
        {
            SetPicture();
            if (string.IsNullOrEmpty(PictureValue) || !IsAce)
            {
                _msgService.DisplayValueCard(whoIs, Value, Section);
            }
            else
            {
                _msgService.DisplayPictueCard(whoIs, PictureValue, Section);
            }
        }

        private void SetPicture()
        {
            if (IsAce && Value == 11)
            {
                return;
            }
            switch (Value)
            {
                case 11:
                    PictureValue = "J";
                    Value = 10;
                    break;
                case 12:
                    PictureValue = "Q";
                    Value = 10;
                    break;
                case 13:
                    PictureValue = "K";
                    Value = 10;
                    break;
                case 14:
                    IsAce = true;
                    PictureValue = "A";
                    break;
            }
        }
    }
}
