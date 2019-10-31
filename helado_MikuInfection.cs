using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.Parts
{
    public class helado_MikuInfection : IPart
    {
        public static List<string> Sounds = new List<string>
        {
            "Miku1",
            "Miku2",
            "Miku3",
            "Miku4",
        };

        public int AttackChance = 5;        // chance each turn of trying to attack an adjacent creature, expressed as a percentage
        public string BaseDamage = "2d6";   // damage inflicted on the attacked creature, expressed as a dice string

        // Try to pick a random local, adjacent, grounded, cophased target. If there are none, return null.
        public GameObject GetTarget()
        {
            var possibleTargets = new List<GameObject>();

            // Accumulate a list of up to one valid melee target per adjacent local cell.
            ParentObject.GetCurrentCell().ForeachLocalAdjacentCell(delegate (Cell Cell)
            {
                var combatTarget = Cell.GetCombatTarget(ParentObject);

                if (combatTarget != null)
                {
                    possibleTargets.Add(combatTarget);
                }
            });

            return possibleTargets.GetRandomElement(null);
        }

        // Attack the given target, running an animation and inflicting damage.
        // This method doesn't check the target for validity as per GetTarget's specification. It attacks them regardless.
        // It *does* check whether the target is an object. If it isn't, it does nothing.
        public void AttackTarget(GameObject Target)
        {
            if (Target != null)
            {
                var equipee = ParentObject.Equipped;

                // Briefly show a cyan ~ at the target's cell, representing the tails whipping out.
                Target.ParticleBlip("&c~");

                PlayUISound(Sounds.GetRandomElement(null));

                // Deal direct damage without rolling an attack.
                Target.FireEvent(Event.New("TakeDamage",
                    "Damage", new Damage(new DieRoll(BaseDamage).Resolve()),
                    "Owner", equipee,
                    "Attacker", equipee,    // Blame our host for the attack.
                    "Message", "from %o miku tails!"));
            }
        }

        public override bool FireEvent(Event E)
        {
            switch (E.ID)
            {
                case "EndTurn":
                    // Each turn, there is a chance the tails will lash out at a selected target.
                    if (Stat.Chance(AttackChance))
                    {
                        AttackTarget(GetTarget());
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
