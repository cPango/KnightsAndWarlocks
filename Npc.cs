﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsAndWarlocks
{
    enum NpcClasses { Warrior, Rogue, Mage }
    enum NpcRaces { Skeleton, Goblin, Troll }

    class Npc
    {
        public NpcRaces NpcRace { get; private set; }
        public NpcClasses NpcClass { get; private set; }
        public int health = 100;
        private const double _accuracyN = 0.80;
        private double _turnChoice;

        //Creates random enemy race.
        public void NpcEnemyRace()
        {
            int rdmNpcRace = GameFunctions.RndNext(0, 3);

            switch (rdmNpcRace)
            {
                case 0:
                    NpcRace = NpcRaces.Skeleton;
                    break;
                case 1:
                    NpcRace = NpcRaces.Goblin;
                    break;
                case 2:
                    NpcRace = NpcRaces.Troll;
                    break;
            }
        }

        //Creates random enemy class.
        public void NpcEnemyClass()
        {
            int rdmNpcClass = GameFunctions.RndNext(0, 3);

            switch (rdmNpcClass)
            {
                case 0:
                    NpcClass = NpcClasses.Warrior;
                    break;
                case 1:
                    NpcClass = NpcClasses.Rogue;
                    break;
                case 2:
                    NpcClass = NpcClasses.Mage;
                    break;
            }
        }

        //Constructor calls 2 functions to create an enemy.
        public Npc()
        {
            NpcEnemyRace();
            NpcEnemyClass();
        }

        public bool HitOrHeal()
        {
            //Npc intelligence => heal chance thresholds
            if (health > 95) _turnChoice = 1.00;
            else if (health > 50) _turnChoice = 0.85;
            else _turnChoice = 0.70;

            return GameFunctions.RndNextDouble() < _turnChoice;
        }

        public bool IsAccuracySuccessful() => GameFunctions.RndNextDouble() < _accuracyN;

        public void NpcChoice(Player name)
        {
            if (HitOrHeal())
            {
                if (IsAccuracySuccessful())
                {
                    int dmg = GameFunctions.RndNext(11, 16);
                    name.health -= dmg;

                    //clamp health to not go below 0
                    if (name.health < 0)
                    {
                        name.health = 0;
                    }
                    else //Gives each foe a unique roleplay element.
                    {
                        if (NpcClass == NpcClasses.Warrior)
                        {
                            Console.WriteLine($"The {NpcRace} {NpcClass} slashed their sword at you for {dmg} damage!");
                        }
                        else if (NpcClass == NpcClasses.Rogue)
                        {
                            Console.WriteLine($"The {NpcRace} {NpcClass} threw a knife at you for {dmg} damage!");
                        }
                        else if (NpcClass == NpcClasses.Mage)
                        {
                            Console.WriteLine($"The {NpcRace} {NpcClass} hurled a fireball at you for {dmg} damage!");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"{NpcRace} {NpcClass} missed!");
                }
            }
            else
            {
                int heal = GameFunctions.RndNext(12, 16);
                int newHealth = health + heal;

                //Clamp health to not go above 100
                if (newHealth > 100)
                {
                    newHealth = 100;
                }

                //Swap placeholder health(newHealth) back into health
                health = newHealth;
                Console.WriteLine($"{NpcRace} {NpcClass} healed by {heal}.");
            }
        }
    }
}