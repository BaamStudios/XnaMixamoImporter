using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SkinnedModel;

namespace BaamStudios.AnimationController
{
    /// <summary>
    /// Manages multiple animations and provides access to the bone transforms.
    /// </summary>
    public class AnimationController
    {
        private readonly Dictionary<string, Animation> _animations = new Dictionary<string, Animation>();

        public Animation CurrentAnimation { get; private set; }

        protected virtual Animation CreateAnimation(string animationName, Stream animationFile)
        {
            return new Animation(animationName, animationFile);
        }

        protected virtual Animation CreateAnimation(string animationName, SkinningData skinningData)
        {
            return new Animation(animationName, skinningData);
        }

        public void AddAnimation(string animationName, Stream animationFile)
        {
            _animations.Add(animationName, CreateAnimation(animationName, animationFile));
        }

        public void AddAnimation(string animationName, SkinningData skinningData)
        {
            _animations.Add(animationName, CreateAnimation(animationName, skinningData));
        }

        public bool HasAnimation(string animationName)
        {
            return _animations.ContainsKey(animationName);
        }

        public virtual void PlayAnimation(string animationName, bool restartIfPlaying)
        {
            var animation = _animations[animationName];

            if (!restartIfPlaying && CurrentAnimation == animation)
                return;

            CurrentAnimation = animation;
            CurrentAnimation.Start();
        }

        public void Update(GameTime gameTime, float speed)
        {
            if (CurrentAnimation != null)
                CurrentAnimation.AnimationPlayer.Update(TimeSpan.FromSeconds(speed * (float)gameTime.ElapsedGameTime.TotalSeconds), true, Matrix.Identity);
        }
    }
}
