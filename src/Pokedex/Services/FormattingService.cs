namespace Pokedex.Services
{
    public class FormattingService
    {

        public string AsTwoDecimalValue(decimal x)
        {

            return x.ToString("00.00");
        }

    }
}
