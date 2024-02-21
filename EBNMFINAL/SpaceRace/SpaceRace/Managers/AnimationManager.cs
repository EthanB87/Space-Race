using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpaceRace.Source.GameEntities;


namespace SpaceRace
{
    public class AnimationManager
    {
        private Dictionary<string, Animation> animations;
        private string currentAnimationName;

        public AnimationManager()
        {
            animations = new Dictionary<string, Animation>();
        }

        public void AddAnimation(string name, Animation animation)
        {
            animations[name] = animation;
        }

        public void SetCurrentAnimation(string name)
        {
            if (animations.ContainsKey(name))
            {
                currentAnimationName = name;
                animations[currentAnimationName].Reset();
            }
            else
            {
                throw new Exception($"Animation '{name}' does not exist.");
            }
        }

        public Animation GetCurrentAnimation()
        {
            return animations[currentAnimationName];
        }

        public Rectangle GetNextFrame()
        {
            return animations[currentAnimationName].GetNextFrame();
        }

        public void Update()
        {
            animations[currentAnimationName].Update();
        }
    }
}
