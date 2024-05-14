namespace Cribbage
{
    public class Card
    {
        public Card(CardRank rank, Suit suit) 
        {
            this.Rank = rank;
            this.Suit = suit;
        }
        public CardRank Rank { get; set; }
        public Suit Suit { get; set; }
    }
}
