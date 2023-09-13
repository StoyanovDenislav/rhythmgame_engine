using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGame.HitObjectInterface
{
    public interface IHitObject
    {
        double X { get; }
        double Y { get; }
        Texture2D Texture { get; set; }
        string Name { get; }
    }

    public interface ITextureOverride
    {
        string Name { get; }
        Texture2D Texture { get; set; }
    }

    public interface ICordsOverride
    {
        double X { get; }
        double Y { get; }
    }


}
