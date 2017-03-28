using System;

namespace Pokedex.Services
{
    public class FormattingService
    {

        public string AsTwoDecimalValue(decimal x)
        {

            return x.ToString("00.00");
        }

        public string AsDatePostedFormat(DateTime dt)
        {
            return dt.ToString("d");
        }

    }
}
