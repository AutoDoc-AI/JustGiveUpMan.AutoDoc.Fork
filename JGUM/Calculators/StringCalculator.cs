using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace JGUM.Calculators
{
    public static class StringCalculator
    {
        private static Dictionary<string, string> _rolledVariants = new Dictionary<string, string>();

        private static int GetVariantCount(string baseId)
        {
            string countText = new TextObject("{=" + baseId + "_count}0").ToString();
            return int.TryParse(countText, out var count) && count > 0 ? count : 0;
        }

        public static string GetString(string baseId, string fallbackString)
        {
            int variantCount = GetVariantCount(baseId);
            string id = variantCount > 0
                ? "{=" + baseId + "_" + MBRandom.RandomInt(1, variantCount + 1) + "}"
                : "{=" + baseId + "}";

            return new TextObject(id + fallbackString).ToString();
        }

        public static void SetDialogVariable(string baseId, string fallbackString)
        {
            if (!_rolledVariants.TryGetValue(baseId, out string id))
            {
                int variantCount = GetVariantCount(baseId);
                id = variantCount > 0
                    ? "{=" + baseId + "_" + MBRandom.RandomInt(1, variantCount + 1) + "}"
                    : "{=" + baseId + "}";
                _rolledVariants[baseId] = id;
            }

            MBTextManager.SetTextVariable(baseId.ToUpper(), new TextObject(id + fallbackString));
        }

        public static void ClearDialogVariables()
        {
            _rolledVariants.Clear();
        }
    }
}