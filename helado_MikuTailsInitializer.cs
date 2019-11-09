using XRL;

namespace XRL.World.Parts
{
    // Since population tables aren't moddable in general, we take advantage of the fact that all blueprints get initialized at load time in order to run some code that will add our object to the relevant tables.
    public class helado_MikuTailsInitializer : IPart
    {
        public static void AddToPopulation(string Table, string Blueprint, string Number, string Chance)
        {
            (PopulationManager.Populations[Table].Items[0] as PopulationGroup).Items.Add(new PopulationObject
            {
                Blueprint = Blueprint,
                Number = Number,
                Chance = Chance,
            });
        }

        public helado_MikuTailsInitializer()
        {
            AddToPopulation("FungalJunglePopulation", "helado_Miku Puffer", "1-3", "50");
            AddToPopulation("FungalBiome1", "helado_Miku Puffer", "1-3", "66");
            AddToPopulation("FungalBiome2", "helado_Miku Puffer", "2-3", "60");
            AddToPopulation("FungalBiome3", "helado_Miku Puffer", "2-4", "66");
        }
    }
}
