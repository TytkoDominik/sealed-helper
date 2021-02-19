using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SealedHelperServer.Models;

namespace SealedHelperServer.DatabaseControllers
{
    public class DeckParser
    {
        public Deck GetDeckDataFromRawString(string rawData, List<Card> cards)
        {
            var regex = new Regex(',' + "(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))"); 
            var data = regex.Split(rawData);
            var result = new Deck();
            result.DeckId = data[0];
            result.Name = data[1];
            try
            {
                result.Expansion = (Expansion) int.Parse(data[2]);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to parse AoA deck: {data[2]}");
                throw;
            }
            
            var rawHouses = data[3].Split('|');

            for (var index = 0; index < rawHouses.Length; index++)
            {
                var rawHouse = rawHouses[index];
                var houseType = (HouseType) Enum.Parse(typeof(HouseType), rawHouse);

                switch (index)
                {
                    case 0:
                        result.House_0 = (int)houseType;
                        break;
                    case 1:
                        result.House_1 = (int)houseType;
                        break;
                    case 2:
                        result.House_2 = (int)houseType;
                        break;
                }
            }

            result.Sas = int.Parse(data[4]);
            result.Synergy = int.Parse(data[5]);
            result.Antisynergy = int.Parse(data[6]);
            result.Aerc = int.Parse(data[7]);
            result.AemberControl = float.Parse(data[8]);
            result.ExpectedAember = float.Parse(data[9]);
            result.ArtifactControl = float.Parse(data[10]);
            result.CreatureControl = float.Parse(data[11]);
            result.Efficiency = float.Parse(data[12]);
            result.Recursion = float.Parse(data[13]);
            result.Disruption = float.Parse(data[14]);
            result.CreatureProtection = float.Parse(data[15]);
            result.Other = float.Parse(data[16]);
            result.EffectivePower = int.Parse(data[17]);
            result.RawAember = int.Parse(data[18]);
            result.ActionCount = int.Parse(data[19]);
            result.UpgradeCount = int.Parse(data[20]);
            result.CreatureCount = int.Parse(data[21]);
            result.ArtifactCount = 36 - result.ActionCount - result.UpgradeCount - result.CreatureCount;
            result.PowerLevel = int.Parse(data[22]);
            result.Chains = int.Parse(data[23]);
            result.Wins = int.Parse(data[24]);
            result.Loses = int.Parse(data[25]);

            var rawCards = data[26].Split('~');

            result.Cards = new List<CardData>();

            foreach (var rawCard in rawCards)
            {
                if (String.IsNullOrEmpty(rawCard))
                {
                    continue;
                }

                if (!int.TryParse(rawCard.Last().ToString(), out var cardCount))
                {
                   continue; 
                }
                
                var cardName = rawCard.Remove(rawCard.Length - cardCount);

                if (cards.All(c => c.Name != cardName))
                {
                    cards.Add(new Card{ Name = cardName });
                }
                
                var card = cards.First(c => c.Name == cardName);
                
                result.Cards.Add(new CardData{Card = card, Quantity = cardCount});
            }

            return result;
        }
    }
}