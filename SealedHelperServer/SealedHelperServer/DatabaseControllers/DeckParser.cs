using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SealedHelperServer.Models;

namespace SealedHelperServer.DatabaseControllers
{
    public class DeckParser
    {
        public Deck GetDeckDataFromRawString(string rawData)
        {
            var regex = new Regex(',' + "(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            var untrimmedData = regex.Split(rawData);

            var data = new List<string>();

            for (var index = 0; index < untrimmedData.Length; index++)
            {
                var record = untrimmedData[index];
                record = record.Replace("\"", String.Empty);
                data.Add(record); 
            }

            var result = new Deck();
            result.Name = data[0];
            result.DoKLink = data[45];

            return result;
        }
    }
}