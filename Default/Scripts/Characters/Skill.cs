using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8.Scripts.Characters
{
    public enum SkillType
    {
        SingleTarget,
        RandomTarget,
        AllTarget,
        SelfTarget
    }
        public class Skills
    {
        public string Name { get; set; }

        public int MPCost { get; set; }

        public string Description { get; set; }

        public float DamageMultiplier { get; set; }

        public int NumberofTargets { get; set; }

        public SkillType Type { get; set; }

        public Skills(string name, int mpCost, string description, float damageMultiplier, SkillType type, int numberofTargets = 1)
        {
            Name = name;
            MPCost = mpCost;
            Description = description;

            DamageMultiplier = damageMultiplier;

            NumberofTargets = numberofTargets;

            Type = type;
        }










    }
}
