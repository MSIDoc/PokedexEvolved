namespace Pokedex.Services
{
    public static class Extensions
    {
        
            public static string ToYesNoString(this bool value)
            {
                return value ? "Yes" : "No";
            }
        
    }
}
