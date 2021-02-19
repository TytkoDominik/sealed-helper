using System.Collections.Generic;

namespace SealedHelperServer.Models
{
    public class Deck
    {
        public string DeckId { get; set; }
        public string Name { get; set; }
        public Expansion Expansion { get; set; }
        public int House_0 { get; set; }
        public int House_1 { get; set; }
        public int House_2 { get; set; }
        public int Sas { get; set; }
        public int Antisynergy { get; set; }
        public int Synergy { get; set; }
        public int Aerc { get; set; }
        public float AemberControl { get; set; }
        public float ExpectedAember { get; set; }
        public float ArtifactControl { get; set; }
        public float CreatureControl { get; set; }
        public float Efficiency { get; set; }
        public float Recursion { get; set; }
        public float Disruption { get; set; }
        public float CreatureProtection { get; set; }
        public float Other { get; set; }
        public int EffectivePower { get; set; }
        public int RawAember { get; set; }
        public int ActionCount { get; set; }
        public int UpgradeCount { get; set; }
        public int CreatureCount { get; set; }
        public int ArtifactCount { get; set; }
        public int PowerLevel { get; set; }
        public int Chains { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        public List<CardData> Cards { get; set; }

        public bool ContainsHouse(HouseType house)
        {
            int rawHouse = (int)house;
            return House_0 == rawHouse || House_1 == rawHouse || House_2 == rawHouse;
        }
    }

    public class CardData
    {
        public int Id { get; set; }
        public Card Card { get; set; }
        public int Quantity { get; set; }
    }
    
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public Card()
        {}
    }

    public enum HouseType
    {
        Brobnar = 0,
        Dis = 1,
        Logos = 2,
        Mars = 3,
        Sanctum = 4,
        Saurian = 5,
        Shadows = 6,
        StarAlliance =7,
        Unfanthomable = 8,
        Untamed = 9
    }
    
    public enum Expansion
    {
        CotA = 341,
        AoA = 420,
        WC = 452,
        MM = 479,
        DT = 423
    }
}