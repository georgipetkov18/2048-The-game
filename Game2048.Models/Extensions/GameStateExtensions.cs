using Game2048.Models.Enums;
using System.ComponentModel;

namespace Game2048.Models.Extensions
{
    public static class GameStateExtensions
    {
        public static string GetDescription(this GameState state)
        {
            var fieldInfo = state.GetType().GetField(state.ToString());

            var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return state.ToString();
        }
    }
}
