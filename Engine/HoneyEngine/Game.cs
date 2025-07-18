using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ProjectLS.Engine.EntityComponentSystem;
using ProjectLS.Engine.EntityComponentSystem.Components;
using ProjectLS.LootNShoot;

namespace ProjectLS;

public class Game : GameWindow
{
    private static GameManager gameManager = new GameManager(10, 16, 100);
    private Engine.Engine engine = new Engine.Engine(gameManager.GameWorldScene);
    
    //fps calculations
    private double fpsCalcTimer;
    private int frames;
    private int fps;
    private string gameTitle;

    public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings()
    {
        Size = (width, height),
        Title = title,
        //WindowState = WindowState.Maximized,

    })
    {
        gameTitle = title;
    }

    protected override void OnLoad()
    {
        GL.ClearColor(0.53f, 0.81f, 0.92f, 1.0f);
        
        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.Blend);
        GL.Disable(EnableCap.CullFace);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        
        gameManager.InstantiateGame();
        
        engine.LoadEntities(gameManager.GameWorldScene.Entities);
        engine.PrintEntities();
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        
        //RENDERING LOGIC:
        engine.RenderUse();
        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        engine.EngineDeltaTime = (float)e.Time;
        engine.EngineElapsedTime += (float)e.Time;
        
        KeyboardState input = KeyboardState;
        MouseState mouse = MouseState;
        
        
        if(input.IsKeyPressed(Keys.Escape))
            Close();
        
        //if(input.IsKeyPressed(Keys.F))
            //engine.PhysicsSystem.AddForce(generateWorldTest.Scene.Entities[3], new Vector3(0f, 4f, 0f));
            
        engine.SetSystemState(input);//sets the system state to wireframe mode for debugging
        engine.PhysicsUse();
        
        gameManager.GameWorldScene.Camera.MoveAround(input, e);
        gameManager.GameWorldScene.Camera.RotateCamera(mouse);
        
        //calculates the FPS for and displays it in the title of the game
        fpsCalcTimer += engine.EngineDeltaTime;
        frames++;
        
        if (fpsCalcTimer >= 1.0)
        {
            fps = frames;
            frames = 0;
            fpsCalcTimer = 0;
            Title = $"{gameTitle} | FPS: {fps}";
        }
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        GL.Viewport(0, 0, e.Width, e.Height);
        
        //adjusts the scene camera to fix when resizing the game
        gameManager.GameWorldScene.Camera.AspectRatio = e.Width / (float)e.Height;
    }
}