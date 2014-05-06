using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaamStudios.AnimationController
{
    /// <summary>
    /// Manages multiple animations and provides access to the bone transforms.
    /// </summary>
    public class AnimationController
    {
        private readonly Dictionary<string, Animation> _animations = new Dictionary<string, Animation>();

        private Animation _currentAnimation;

        public string CurrentAnimationName
        {
            get
            {
                return _currentAnimation != null ? _currentAnimation.Name : null;
            }
        }

        public void AddAnimation(string animationName, Stream animationFile)
        {
            _animations.Add(animationName, new Animation(animationName, animationFile));
        }

        public void PlayAnimation(string animationName, bool restartIfPlaying)
        {
            var animation = _animations[animationName];

            if (!restartIfPlaying && _currentAnimation == animation)
                return;

            _currentAnimation = animation;
            _currentAnimation.Start();
        }

        public void Update(GameTime gameTime, float speed)
        {
            if (_currentAnimation != null)
                _currentAnimation.AnimationPlayer.Update(TimeSpan.FromSeconds(speed * (float)gameTime.ElapsedGameTime.TotalSeconds), true, Matrix.Identity);
        }

        /// <summary>
        /// The relative transforms from bind pose to final bone position.
        /// These values are used by the shader to transform the bindpose vertices to their final location.
        /// </summary>
        /// <returns></returns>
        public Matrix[] GetSkinTransforms()
        {
            if (_currentAnimation == null)
                return null;
            return _currentAnimation.AnimationPlayer.GetSkinTransforms();
        }

        /// <summary>
        /// The bone transforms (including bone position) relative to the model origin.
        /// These values can be used to attach objects to bones, e.g. a gun to the hand.
        /// See Microsoft.Xna.Framework.Graphics.Model.Bones for the bone names.
        /// </summary>
        /// <returns></returns>
        public Matrix[] GetWorldTransforms()
        {
            if (_currentAnimation == null)
                return null;
            return _currentAnimation.AnimationPlayer.GetWorldTransforms();
        }
    }
}
