namespace Blackjack
{
    internal interface IPerson
    {
        Hand Hand { get; set; }
        string Name { get; set; }

        bool DoesHit(Hand hand);
    }
}
