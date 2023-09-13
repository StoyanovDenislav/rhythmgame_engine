using Raylib_cs;
using RhythmGame.TextureBindings;
using System.Numerics;

public class CircleScrollControl
{
    public static int elements = 1000;
    float[] timing = new float[elements];
    float[] timingFinal = new float[elements];

    public static List<float> objectIndex = new List<float>();

    public List<HitObject> renderList = new List<HitObject>();

    int currentObjectIndex = 0;
    public bool gameHasBegun = false;
    public static float speed = 800;

    public void lineConstructor()
    {





        for (int i = 0; i < elements; i++)
        {


            objectIndex.Add(Raylib.GetRandomValue(0, 3));
            timing[i] = Raylib.GetRandomValue(0, 600) + MainGame.elapsedTime;
            timingFinal[i] = ((((float)MainGame.baseCircleStatic[0].Y - (Raylib.GetScreenHeight() - Raylib.GetScreenHeight())) / speed) + timing[i]);


            Console.WriteLine(timing[i] + " : " + timingFinal[i]);


        }
        Array.Sort(timing);
        Array.Sort(timingFinal);




        renderList = MainGame.Instantiate(objectIndex.Count, "hitCircle");
        SetDefault();
        gameHasBegun = true;





    }

    public void moveCircles()
    {


        if (gameHasBegun)
        {




            if (currentObjectIndex < timing.Length && MainGame.elapsedTime >= timing[currentObjectIndex] + Raylib.GetFrameTime())
            {
                float index = objectIndex[currentObjectIndex];

                Vector2 position = CalculatePosition(index);



                if (currentObjectIndex < objectIndex.Count)
                {

                    renderList[currentObjectIndex].X = position.X;
                    renderList[currentObjectIndex].Y = position.Y;
                    renderList[currentObjectIndex].isActive = true;

                }

                else
                {

                    renderList[currentObjectIndex - 1].X = position.X;
                    renderList[currentObjectIndex - 1].Y = position.Y;
                    renderList[currentObjectIndex].isActive = true;


                }



                currentObjectIndex++;




            }








        }



    }

    private Vector2 CalculatePosition(float index)
    {
        // Calculate the position based on the index
        float screenWidth = Raylib.GetScreenWidth();
        float screenHeight = Raylib.GetScreenHeight();

        float xPos = screenWidth / 3 + (150 * index);
        float yPos = screenHeight - screenHeight;

        return new Vector2(xPos, yPos);
    }

    public void SetDefault()
    {

        // MainGame.elapsedTime = 0;
        currentObjectIndex = 0;
        gameHasBegun = false;

        for (int i = 0; i < renderList.Count; i++)
        {

            renderList[i].X = 0;
            renderList[i].Y = 0;
            renderList[i].isActive = false;
        }
    }
}