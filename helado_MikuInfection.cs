using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.Parts
{
    public class helado_MikuInfection : IPart
    {
        public int AttackChance = 15;

        public GameObject GetTarget()
        {
            var possibleTargets = new List<GameObject>();

            ParentObject.GetCurrentCell().ForeachLocalAdjacentCell(delegate (Cell Cell)
            {
                var combatTarget = Cell.GetCombatTarget(ParentObject);

                if (combatTarget != null)
                {
                    possibleTargets.Add(combatTarget);
                }
            });

            var count = possibleTargets.Count;

            return count == 0 ?
                null :
                possibleTargets[Stat.Rnd.Next(0, count)];
        }

        public override bool FireEvent(Event E)
        {
            switch (E.ID)
            {
                case "EndTurn":
                    if (Stat.Chance(AttackChance))
                    {
                        var target = GetTarget();

                        if (target != null)
                        {
                            var equipee = ParentObject.Equipped;

                            target.FireEvent(Event.New("TakeDamage",
                                "Damage", new Damage(6),
                                "Owner", equipee,
                                "Attacker", equipee,
                                "Message", "from %o miku tails!"));
                        }
                    }

                    return true;

                default:
                    return base.FireEvent(E);
            }
        }

        public override void Register(GameObject Object)
        {
            Object.RegisterPartEvent(this, "EndTurn");
            base.Register(Object);
        }
    }
}
