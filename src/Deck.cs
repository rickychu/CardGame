using System.Collections.Generic;
using System;

namespace CardGame
{
    public class Deck {
        
        private const int ShuffleCount = 5;

        protected List<Card> cards = new List<Card>();
        protected int currentIndex = 0;

        public Deck() {
            // Assign suit and value to card to build the deck
            foreach (Suit suit in Enum.GetValues(typeof(Suit))) {
                for (int value=1; value<=13; value++) {
                    var card = new Card(suit, value);

                    cards.Add(card);
                }
            }
        }

        public int CardCount {
            get {
                return this.cards.Count;
            }
        }

        // swap the cards
        protected void swap(int card1, int card2) {
                var temp = this.cards[card1];
                cards[card1] = this.cards[card2];
                cards[card2] = temp;
        }

        // Shuffle all the cards
        // Iterate all the cards and swap each one with a random card
        // Go through this multiple times to increase the randomness
        public void shuffle() {

            int cardCount = this.cards.Count;

            // Swap the whole deck of card n times
            for (int n=0; n<ShuffleCount; n++) {
                for (int i=0; i<cardCount; i++) {
                    var rand = new Random();
                    int randIndex = rand.Next(cardCount);

                    this.swap(i, randIndex);
                }
            }

            // Reset the deck after it's shuffle
            this.currentIndex = 0;
        }

        // deal one card
        public Card dealOneCard() {
            // Make sure index is less than the max card
            if (this.currentIndex >= this.cards.Count) {
                throw new EndOfDeckException();
            }
                
            // Return the card of the current index            
            return cards[this.currentIndex++];
        }
    }

    // Invalid card value exception
    public class InvalidCardValueException: Exception {}

    // End of deck exception
    public class EndOfDeckException: Exception {}

    // Card class
    // Stores the suit, value, and determine the face value based on the value
    public class Card
    {
        public Suit suit {get; set;}
        public int value {get; set;}

        public string faceValue {get; set;}

        // Constructor
        public Card(Suit suit, int value)
        {
            this.suit = suit;
            this.value = value;

            // Assign the face value based on the value
            if (value >= 2 && value <= 10) {
                this.faceValue = value.ToString();
            } else if (1 == value) {
                this.faceValue = "Ace";
            } else if (11 == value) {
                this.faceValue = "Jack";
            } else if (12 == value) {
                this.faceValue = "Queen";
            } else if (13 == value) {
                this.faceValue = "King";
            } else {
                // Invalid value
                throw new InvalidCardValueException();
            }

        }

        public override string ToString()
        {
            return $"Suit:{this.suit} Face:{this.faceValue} Value:{this.value}";
        }
    }

    public enum Suit {
        Diamond,
        Club,
        Heart,
        Spade
    }

}
