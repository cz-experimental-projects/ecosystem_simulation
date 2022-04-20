﻿using System.Numerics;
using Raylib_cs;
using Simulation_CSharp.World.Entities;
using Simulation_CSharp.World.Tiles;

namespace Simulation_CSharp.Core;

public static class Updater
{
    public static void Update(ref Camera2D camera)
    {
        Input(ref camera);
        Raylib.BeginMode2D(camera);
        Raylib.ClearBackground(Color.DARKGRAY);
        CameraModification(ref camera);
        RenderMap();
        RenderEntities();
        Raylib.EndMode2D();
        RenderHud();
    }

    private static void RenderEntities()
    {
        SimulationCore.Level.Entities.ForEach(entity =>
        {
            entity.Render();
            entity.Update();
        });
        var removalQueue = SimulationCore.Level.RemovalQueue;
        for (var i = 0; i < removalQueue.Count; i++)
        {
            SimulationCore.Level.Entities.Remove(removalQueue.Dequeue());
            Raylib.TraceLog(TraceLogLevel.LOG_INFO, "Removed Entity");
        }
    }
        
    private static void RenderMap()
    {
        SimulationCore.Level.Map.Render();
    }

    private static void RenderHud()
    {
        Raylib.DrawFPS(20, 20);
    }

        
        
        
        
    private const float MinZoomValue = 0.1f;
    private static float _scrollMovement = 1;

    private static Vector2 _initialMousePos;
    private static Vector2 _secondMousePos;
        
    private static void CameraModification(ref Camera2D camera)
    {
        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {
            _initialMousePos = GetWorldSpaceMousePos(ref camera);
        }

        if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
        {
            var tempSecondPos = GetWorldSpaceMousePos(ref camera);
            if (_secondMousePos != tempSecondPos)
            {
                camera.target += -(tempSecondPos - _initialMousePos);
            }
            _secondMousePos = tempSecondPos;
        }
            
        if (Raylib.IsMouseButtonReleased(MouseButton.MOUSE_BUTTON_LEFT))
        {
            _initialMousePos = Vector2.Zero;
        }
            
        // Zooming the camera
        camera.zoom = Math.Clamp(_scrollMovement, MinZoomValue, float.MaxValue);
    }
        
    private static void Input(ref Camera2D camera)
    {
        // Middle click generates a new map. For debugging purposes 
        if (Raylib.IsKeyDown(KeyboardKey.KEY_F))
        {
            SimulationCore.Level.Map.GenerateNew();
        }

        if (Raylib.IsKeyDown(KeyboardKey.KEY_R))
        {
            camera.target = Vector2.Zero;
        }
            
        if (Raylib.IsMouseButtonReleased(MouseButton.MOUSE_BUTTON_RIGHT))
        {
            SimulationCore.Level.CreateEntity(() => new SheepEntity(), new TileCell(GetWorldSpaceMousePos(ref camera)));
        }

        var value = Raylib.GetMouseWheelMove();
        switch (value)
        {
            case 0:
                return;
            case < 0 when _scrollMovement >= MinZoomValue:
                _scrollMovement += value/2;
                break;
            case < 0:
                return;
            default:
                _scrollMovement += value/2;
                break;
        }
    }

    private static Vector2 GetWorldSpaceMousePos(ref Camera2D camera)
    {
        return Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), camera);
    }
}