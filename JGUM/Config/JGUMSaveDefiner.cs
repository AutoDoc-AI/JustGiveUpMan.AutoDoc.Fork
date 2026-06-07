using System.Collections.Generic;
using JGUM.Data;
using TaleWorlds.CampaignSystem;
using TaleWorlds.SaveSystem;

namespace JGUM.Config
{
    public class JGUMSaveDefiner : SaveableTypeDefiner
    {
        // Unique base ID for Just Give Up Man to avoid conflicts with other mods.
        public JGUMSaveDefiner() : base(987650)
        {
        }

        protected override void DefineClassTypes()
        {
            AddClassDefinition(typeof(AiSurrenderLogEntry), 1);
        }

        protected override void DefineContainerDefinitions()
        {
            // Adding a CampaignTime dictionary definition just in case we need it for cooldowns later,
            // though SyncData handles it natively, explicitly defining containers is safer.
            ConstructContainerDefinition(typeof(Dictionary<string, CampaignTime>));
            ConstructContainerDefinition(typeof(Dictionary<string, int>));
        }
    }
}
