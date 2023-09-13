using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using RhythmGame.HitObjectInterface;

namespace RhythmGame.TextureBindings
{

    public abstract class HitObject : IHitObject, ITextureOverride, ICordsOverride, ICloneable
    {


        public abstract double X { get; set; }
        public abstract double Y { get; set; }
        public abstract Texture2D Texture { get; set; }
        public abstract string Name { get;  }

        public abstract bool isActive { get; set; }

        public abstract object Clone();



    }

    public class baseCircle : HitObject
    {



        public override double X { get; set; }
        public override double Y { get; set; }

        public override Texture2D Texture { get; set; }

        public override bool isActive { get; set; }
        public override string Name => "baseCircle";


        public override object Clone()
        {
            return new baseCircle
            {
                X = this.X,
                Y = this.Y,
                Texture = this.Texture,
                isActive = this.isActive
         
                // Clone additional properties specific to baseCircle
            };
        }


    }

    public class hitCircle : HitObject
    {



        public override double X { get; set; }
        public override double Y { get; set; }

        public override Texture2D Texture { get; set; }

        public override bool isActive { get; set; }

        public override string Name => "hitCircle";

        public override object Clone()
        {
            return new hitCircle
            {
                X = this.X,
                Y = this.Y,
                Texture = this.Texture,
                isActive = this.isActive

                // Clone additional properties specific to baseCircle
            };
        }
    }




    public class BindTextures
    {



        private Dictionary<string, Texture2D> textureCache;


        public BindTextures(Dictionary<string, Texture2D> textureCache)
        {
            this.textureCache = textureCache;
        }

        public void TexturesBind(IEnumerable<HitObject> hitObjects)
        {
            foreach (var hitObject in hitObjects)
            {
                if (hitObject is ITextureOverride textureOverride)
                {
                    if (textureCache.ContainsKey(textureOverride.Name))
                    {
                        textureOverride.Texture = textureCache[textureOverride.Name];

                        
                    }
                    else
                    {
                        textureOverride.Texture = Raylib.LoadTexture("");
                    }
                }
            }
        }

        

    }

    


}
