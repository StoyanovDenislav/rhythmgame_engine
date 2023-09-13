using Raylib_cs;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using RhythmGame.HitObjectInterface;
using RhythmGame.TextureHandle;
using RhythmGame.TextureBindings;
using RhythmGame.Main;
using System.Windows.Markup;

enum ProgramState
{

    Start,
    Update,
    Draw
}





class MainGame
{
    static CircleScrollControl circleScroll = new CircleScrollControl();

    static Dictionary<string, Texture2D> textureCache = new Dictionary<string, Texture2D>();

    public static List<HitObject> _hitObjects = new List<HitObject>();

    static TextureLoad texture = new TextureLoad();

    static Collisions collisions = new Collisions();

    public static List<HitObject> baseCircleStatic = new List<HitObject>();

    public static List<HitObject> markForRemoval = new List<HitObject>();

    static int spacing = 150;

    public static Texture2D emptyTexture = Raylib.LoadTexture(" ");

    public static float elapsedTime = 0;

    static void Main()
    {

        Raylib.InitWindow(1000, 1000, "raylib [core] example - basic screen manager");
        Raylib.SetTargetFPS(60);

        ProgramState currentState = ProgramState.Start;

        do
        {
            switch (currentState)
            {


                case ProgramState.Start:
                    Start();
                    currentState = ProgramState.Update;
                    break;

                case ProgramState.Update:
                    Update();
                    currentState = ProgramState.Draw;
                    break;

                case ProgramState.Draw:
                    Draw();
                    currentState = ProgramState.Update;
                    break;
            }


        } while (!Raylib.WindowShouldClose());
    }



    static void Start()
    {


        StartTextureBinding();

        InputHandler.SetBinds();

        baseCircleStatic = Instantiate(4, "baseCircle");





        for (int i = 0; i < baseCircleStatic.Count; i++)
        {

            baseCircleStatic[i].X = Raylib.GetScreenWidth() / 3 + i * spacing;

            baseCircleStatic[i].Y = Raylib.GetScreenHeight() / 2 + 300;


        }

        circleScroll.lineConstructor();


    }

    

    static void Update()
    {

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) { circleScroll.lineConstructor(); }

        circleScroll.moveCircles();

        bool checkCol = false;


        elapsedTime += Raylib.GetFrameTime();


        for (int i = 0; i < baseCircleStatic.Count; i++) {

            for (int x = 0; x < circleScroll.renderList.Count; x++) {

                checkCol = collisions.CheckCollision(baseCircleStatic[i], circleScroll.renderList[x]);

                if (checkCol)
                {

                    if (!markForRemoval.Contains(circleScroll.renderList[x])) markForRemoval.Add(circleScroll.renderList[x]);

                    circleScroll.renderList[x].Texture = emptyTexture;

                    Console.WriteLine("Removed an object at: " + elapsedTime);

                }
            }

            
        }

        foreach (HitObject go in circleScroll.renderList)
        {

            if (go.Y < Raylib.GetScreenHeight() && go.isActive) go.Y += CircleScrollControl.speed * Raylib.GetFrameTime();


        }


        checkForElementsOutsideBounds();

        if (CircleScrollControl.objectIndex.Count <= markForRemoval.Count)
        {

            CircleScrollControl.objectIndex.Clear();
            circleScroll.renderList.Clear();
            markForRemoval.Clear();
            circleScroll.SetDefault();
            
            
        }



    }


    static void Draw()
    {




        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.BLACK);


        for (int i = 0; i < circleScroll.renderList.Count; i++) {

            Raylib.DrawTextureV(circleScroll.renderList[i].Texture, new Vector2((float)circleScroll.renderList[i].X, (float)circleScroll.renderList[i].Y), Color.WHITE);
        }

        for (int i = 0; i < baseCircleStatic.Count; i++)
        {


            Raylib.DrawTextureV(baseCircleStatic[i].Texture, new Vector2((float)baseCircleStatic[i].X, (float)baseCircleStatic[i].Y), Color.WHITE);
        }


        if (!circleScroll.gameHasBegun) Raylib.DrawText("Game has finished. Press Enter to restart", 300, 300, 30, Color.WHITE);

       

        



        /*  baseCircle baseCircle = (baseCircle)_hitObjects[0];

          Raylib.DrawTextureV(baseCircle.Texture, new Vector2((float)baseCircle.X, (float)baseCircle.Y), Color.WHITE);*/


        Raylib.EndDrawing();

    }


    
    static void checkForElementsOutsideBounds()
    {
        try
        {
            foreach (HitObject ho in circleScroll.renderList)
            {



                if (ho.Y > Raylib.GetScreenHeight() && !markForRemoval.Contains(ho))
                {
                    markForRemoval.Add(ho);



                }

               



            }
        }
        catch (Exception e)
        {

            Console.WriteLine(e.ToString());

        }
    }

    static void StartTextureBinding() {

        MainGame mainGame = new MainGame();

        

        texture.LoadTexturesInGame();

        textureCache = texture.textureCacheInGame;


        var binder = new BindTextures(textureCache);


        ClassCollector classCollector = new ClassCollector();
        List<Type> derivedClasses = classCollector.GetDerivedClasses<HitObject>();



        foreach (Type derivedClassType in derivedClasses)
        {
            HitObject instance = (HitObject)Activator.CreateInstance(derivedClassType);
            _hitObjects.Add(instance);
        }





        // Bind the textures
        binder.TexturesBind(_hitObjects);
    }


    

    public static List<HitObject> Instantiate(int n, string name)
    {

        List<HitObject> objects = new List<HitObject>();

        

        for (int i = 0; i < n; i++)
        {
            HitObject originalObject = _hitObjects.FirstOrDefault(obj => obj.Name == name);

            if (originalObject != null)
            {
                HitObject clonedObject = (HitObject)originalObject.Clone();

                objects.Add(clonedObject);
            }
        }


        return objects;



    }

    public static HitObject InstantiateOnce(string name)
    {


        HitObject originalObject = _hitObjects.FirstOrDefault(obj => obj.Name == name);

        if (originalObject != null)
        {
            HitObject clonedObject = (HitObject)originalObject.Clone();

            return clonedObject;
        }

        return null;


    }

    
}





