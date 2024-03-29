﻿using Simulation_CSharp.Tiles;

namespace Simulation_CSharp.PathFinding;

public interface IPathFindingAgent<T> where T : Node
{ 
    void Init(T start, T end, Dictionary<TileCell, T> map);

    List<TileCell> FindPath();
}