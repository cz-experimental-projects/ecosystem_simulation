﻿using Raylib_cs;
using Simulation_CSharp.Entities.AI;
using Simulation_CSharp.Entities.AI.Goals;
using Simulation_CSharp.Entities.Inheritance;
using Simulation_CSharp.PathFinding;

namespace Simulation_CSharp.Entities.Wolf;

public class Wolf : Entity
{
    public Wolf(Gene genetics) : base(genetics, "Wolf")
    {
        
    }

    public Wolf() : this(new Gene(20, 2, 100, 100, 26, 1, 10, false, true, false, 2, 1))
    {
        
    }
    
    protected override Brain CreateBrain()
    {
        var brain = new Brain(this, new AStarPathFinder(this));
        brain.RegisterGoal(new DrinkGoal(5, this, brain));
        brain.RegisterGoal(new EatSheepGoal(5, this, brain));
        brain.RegisterGoal(new ReproduceGoal(6, this, brain));
        brain.RegisterGoal(new RandomWalkGoal(0, this, brain, 30));
        return brain;
    }

    public override void CreateOffspring(Entity mate)
    {
        if (mate is not Wolf) return;
        for (var i = 0; i < Raylib.GetRandomValue(1, 4); i++)
        {
            Level.CreateEntity(() => BabyWolf.CreateBaby(this, (Wolf) mate), Position);
        }
    }
}