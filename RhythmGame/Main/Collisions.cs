using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RhythmGame.Main
{
    public class Collisions
    {
        

        public bool CheckCollision(TextureBindings.HitObject hitObject1, TextureBindings.HitObject hitObject2) { 

            bool collision = false;


            if (hitObject1.X + hitObject1.Texture.width >= hitObject2.X && 
                hitObject2.X + hitObject2.Texture.width >= hitObject1.X &&
                hitObject1.Y + hitObject1.Texture.height >= hitObject2.Y &&
                hitObject2.Y + hitObject2.Texture.height >= hitObject1.Y)
            {
                collision = true;
            }


            return collision;   
            
        }
    }
}
