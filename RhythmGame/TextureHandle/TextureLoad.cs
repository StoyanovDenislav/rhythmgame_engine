using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RhythmGame.HitObjectInterface;

namespace RhythmGame.TextureHandle
{


    public class TextureLoad
    {


        public string? exeFolderPath;
        public string? folderPath;
        public string[]? filePaths;
        public Dictionary<string, Texture2D>? textureCacheInGame = new Dictionary<string, Texture2D>();

        public void LoadTexturesInGame()
        {



            exeFolderPath = Environment.CurrentDirectory;
            folderPath = Path.Combine(exeFolderPath, "Resources/ingame");
            filePaths = Directory.GetFiles(folderPath);




            for (int i = 0; i < filePaths.Length; i++)
            {



                textureCacheInGame?.Add(Path.GetFileNameWithoutExtension(filePaths[i]), Raylib.LoadTexture(filePaths[i]));

                Console.WriteLine("Added successfully:" + Path.GetFileNameWithoutExtension(filePaths[i]));


            }





        }

        public void UnloadTexturesInGame()
        {



            foreach (KeyValuePair<string, Texture2D> kvp in textureCacheInGame)
            {

                
                Texture2D texture = kvp.Value;

                Raylib.UnloadTexture(texture);

               
            }

            textureCacheInGame.Clear();



        }





    }
}



