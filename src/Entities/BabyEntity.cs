﻿using Simulation_CSharp.Core;
using Simulation_CSharp.Entities.Inheritance;
using Simulation_CSharp.Utils.Widgets;

namespace Simulation_CSharp.Entities;

public abstract class BabyEntity : Entity
{
    protected int GrowthCountDown;
    
    public BabyEntity(Gene genetics, string entityName) : base(genetics, entityName, true)
    {
        GrowthCountDown = 0;
    }

    public override void CreateOffspring(Entity mate)
    {
        throw new NotSupportedException("Baby entity can not create offspring");
    }

    public override bool RequestMate(Entity other)
    {
        throw new NotSupportedException("Baby entity can not mate");
    }

    public override void Update()
    {
        base.Update();
        GrowthCountDown += 1 * SimulationCore.Time * Genetics.GrowthAcceleration;
        if (GrowthCountDown >= 5000)
        {
            Level.CreateEntity(() => new Sheep.Sheep(), Position);
            Destroy();
        }
    }

    protected override void RenderAdditionalTooltip(TooltipRenderer renderer)
    {
        renderer.DrawProgressBar("Growth", 5000, GrowthCountDown, true);
    }
}