﻿using System.Numerics;
using Raylib_cs;
using Simulation_CSharp.World;

namespace Simulation_CSharp.Core;

public static class SimulationCore
{
    private static ILevel _level = null!;
    public static Camera2D Camera2D;
    public static int Time = 1;
    public static bool Close = false;
    
    public static void Main(string[] args)
    {
        _level = new Level();
        Updater.Level = _level;

        Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
        Raylib.InitWindow(800, 480, "Ecosystem Simulation");
        Raylib.SetWindowIcon(Raylib.LoadImage("resources/logo.png"));

        ResourceLoader.LoadTextures();
        
        var screenWidth = Raylib.GetScreenWidth();
        var screenHeight = Raylib.GetScreenHeight();
        Camera2D = new Camera2D(new Vector2(screenWidth / 2f, screenHeight / 2f), Vector2.Zero, 0, 1);
        
        while (!Raylib.WindowShouldClose() && !Close)
        {
            Raylib.BeginDrawing();
            Updater.Update(ref Camera2D);
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}