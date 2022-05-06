﻿using Simulation_CSharp.Tiles;

namespace Simulation_CSharp.PathFinding;

public class Node
{
    public readonly TileCell Position;
    public float GCost; // distance from starting node
    public float HCost; // distance from ending node
    public float FCost; // G + H = F
    public Node? Parent;
    public bool IsObstructed;
    
    public Node(TileCell position)
    {
        Position = position;
        GCost = 0;
        HCost = 0;
        FCost = 0;
        Parent = null;
        IsObstructed = false;
    }

    public void Reset()
    {
        GCost = 0;
        HCost = 0;
        FCost = 0;
        Parent = null;
        IsObstructed = false;
    }

    public static bool operator ==(Node? a, Node? b)
    {
        return a is not null && b is not null && a.Position == b.Position;
    }
    
    public static bool operator !=(Node? a, Node? b)
    {
        return a is null || b is null || a.Position != b.Position;
    }
    
    protected bool Equals(Node other)
    {
        return Position.Equals(other.Position);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Node) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Position);
    }
}