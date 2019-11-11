namespace XRL.World.Parts
{
    public class helado_RandomTile : IPart
    {
        public string Choices = "";

        public override void Register(GameObject Object)
        {
            Log("Randomizing tile...");
            var Tile = Choices.Split(',').GetRandomElement();
            Log("We picked " + Tile + "!");
            Object.pRender.Tile = Tile;
            Log("It's been set.");
            base.Register(Object);
            Log("Base registration is done.");
            Object.RemovePart(this);
            Log("We should be removed now.");
        }
    }
}
