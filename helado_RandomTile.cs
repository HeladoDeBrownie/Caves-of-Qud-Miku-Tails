namespace XRL.World.Parts
{
    public class helado_RandomTile : IPart
    {
        public string Choices = "";

        public override void Register(GameObject Object)
        {
            var Tile = Choices.Split(',').GetRandomElement();
            Object.pRender.Tile = Tile;
            base.Register(Object);
            Object.RemovePart(this);
        }
    }
}
