using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReflectionUtility;

namespace DivineAffinity
{
    class DivineAffinityGroup
    {
       
        public static void init()
        {
            ActorTraitGroupAsset DivineAffinityGroup = new ActorTraitGroupAsset();
            DivineAffinityGroup.id = "DivineAffinity";
            DivineAffinityGroup.name = "trait_group_DivineAffinity";
            DivineAffinityGroup.color = Toolbox.makeColor("##4fdbdb", -1f);
            AssetManager.trait_groups.add(DivineAffinityGroup);
            addTraitGroupToLocalizedLibrary(DivineAffinityGroup.id, "Divine Affinity");
        }
        private static void addTraitGroupToLocalizedLibrary(string id, string name)
        {
            string language = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "language") as string;
            Dictionary<string, string> localizedText = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "localizedText") as Dictionary<string, string>;
            localizedText.Add("trait_group_" + id, name);
        }
    }
}