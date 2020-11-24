using System;
using Xunit;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using CardGame;

namespace test
{
    public class UnitTest1
    {
        [Fact]
        public void TestInvalidCardValueException()
        {
            // Test invalid card value exception
            Card card;
            Assert.Throws<InvalidCardValueException>(
                () => card = new Card(Suit.Club, -1)
            );
        }

        [Fact]
        public void TestFaceValue()
        {
            var Input = new Dictionary<int, string>{
                {1, "Ace"},   // 1 = A
                {2, "2"},   // 2-10 = digit
                {10, "10"},
                {11, "Jack"},  // 11 = J
                {12, "Queen"},  // 12 = Q
                {13, "King"}   // 13 = K
            };

            // Test face value assignments
            foreach (var kv in Input.Keys) {
                var card = new Card(Suit.Club, kv);            
                var expectedFaceValue = Input[kv];

                Assert.Equal(card.faceValue, expectedFaceValue);
            }
        }        

        [Fact]
        public void TestShuffle()
        {
            // Test the shuffle function. Fail if all the cards
            // drawn have value in sequence
            Console.WriteLine("====================================================");

            var deck = new Deck();
            deck.shuffle();

            int n = 5;
            // Save all the card in array so we can compare later
            var cards = new List<Card>();
            for (int i=0; i<n; i++) {
                var card = deck.dealOneCard();
                cards.Add(card);
            }

            // Compare and see if they are sequential values. Starts with index 1 and use the first card for comparison.
            // Randomness is low if all cards are in sequence -> fail the test.
            for (var i=1; i<n; i++) {
                var prevCard = cards[i-1];
                var currentCard = cards[i];

                // The two cards should not have the same suit with the current card has a value of 1 greater than the previous (in sequence)
                Assert.NotEqual(prevCard.suit.ToString() + (prevCard.value + 1).ToString(), currentCard.suit.ToString() + currentCard.value);
            }

        }

        [Fact]
        public void TestDealtMoreCardsThanAvailable()
        {
            Console.WriteLine("====================================================");
            var deck = new Deck();

            // Deal all the cards
            for (int i=0; i<Deck.MaxCard; i++) {
                var card = deck.dealOneCard();
            }
            Console.WriteLine($"Dealt all cards");

            Card oneMoreCard;
            Assert.Throws<EndOfDeckException>(
                () => oneMoreCard = deck.dealOneCard()
            );
        }
    }

}
