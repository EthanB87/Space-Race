using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct2D1;
using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D9;
using SpaceRace.Managers;
using SpaceRace.Source.GameComponents;
using SpaceRace.Source.GameEntities;

namespace SpaceRace
{
    public class Astronaut : Sprites
    {
        private const float gravity = 3000f;
        private const float jumpVelocity = -700f;
        private const float doubleJumpVelocity = -1000f;
        private float speed = 225f;
        private Vector2 velocity;
        private bool isOnGround = true;
        private bool isJumping = false;
        private int jumpCount = 0;
        private const int maxJumpCount = 2;
        private bool isFacingRight = true;
        private Vector2 initialPosition;
        private AnimationManager animationManager;
        private SoundEffect jumpSound = Globals.content.Load<SoundEffect>("cartoon-jump-6462");

        public Astronaut(Dictionary<string, Texture2D> animationSpritesheets, Vector2 position) : base(animationSpritesheets["Idle"], position)
        {
            initialPosition = position;
            animationManager = new AnimationManager();

            foreach (var kvp in animationSpritesheets)
            {
                Animation animation = new Animation(kvp.Value, 24, 24, 6, 100);
                animationManager.AddAnimation(kvp.Key, animation);
            }

            animationManager.SetCurrentAnimation("Idle");
        }


        public Rectangle CalculateAstronautBounds(Vector2 pos)
        {
            Animation currentAnimation = animationManager.GetCurrentAnimation();

            // Ensure there's a current animation
            if (currentAnimation != null)
            {
                Rectangle currentFrame = currentAnimation.GetNextFrame();
                return new Rectangle((int)pos.X, (int)pos.Y, currentFrame.Width, currentFrame.Height);
            }

            // Default to using the whole texture if there's no current animation
            return new Rectangle((int)pos.X, (int)pos.Y, texture.Width, texture.Height);
        }

        private void HandleInput()
        {
            KeyboardState ks = Keyboard.GetState();

            // Horizontal movement
            if (ks.IsKeyDown(Keys.A))
            {
                velocity.X = -speed;
                isFacingRight = false;
            }
            else if (ks.IsKeyDown(Keys.D))
            {
                velocity.X = speed;
                isFacingRight = true;   
            }
            else
            {
                velocity.X = 0;
            }

            // Jumping
            if (ks.IsKeyDown(Keys.Space))
            {
                if (isOnGround)
                {
                    velocity.Y = jumpVelocity; // Initial jump
                    jumpSound.Play();
                    isOnGround = false;
                }
                else if (jumpCount == 0 && velocity.Y > 0)
                {
                    velocity.Y = doubleJumpVelocity; // Double jump
                    jumpSound.Play();
                    jumpCount++;
                }
            }
            else if (isOnGround)
            {
                // Reset jump count when on the ground
                jumpCount = 0;
            }
        }


        public void HandleCollisions()
        {
            Rectangle newRectangle = CalculateAstronautBounds(position);
            List<Rectangle> collisions = LevelManager.GetCurrentMap().GetCollisions(newRectangle);

            // Check collisions with the map
            foreach (var collision in collisions)
            {
                CollisionManager.CollisionDirection direction = CollisionManager.GetCollisionDirection(newRectangle, collision);

                // Adjust astronaut's position based on collision direction
                switch (direction)
                {
                    case CollisionManager.CollisionDirection.Top:
                        position.Y = collision.Bottom; // Move astronaut to the bottom of the tile
                        velocity.Y = Math.Max(0, velocity.Y); // Stop vertical movement if moving up
                        break;
                    case CollisionManager.CollisionDirection.Bottom:
                        position.Y = collision.Top - newRectangle.Height; // Move astronaut to the top of the tile
                        velocity.Y = Math.Min(0, velocity.Y); // Stop vertical movement if moving down
                        isOnGround = true; // Astronaut is considered on the ground
                        jumpCount = 0;
                        break;
                    case CollisionManager.CollisionDirection.Left:
                        position.X = collision.Right; // Move astronaut to the right of the tile
                        velocity.X = Math.Max(0, velocity.X); // Stop horizontal movement if moving left
                        break;
                    case CollisionManager.CollisionDirection.Right:
                        position.X = collision.Left - newRectangle.Width; // Move astronaut to the left of the tile
                        velocity.X = Math.Min(0, velocity.X); // Stop horizontal movement if moving right
                        break;
                }
            }

            if (isOnGround)
            {
                // If on the ground, reset and jump variables
                jumpCount = 0;
            }

            Debug.WriteLine($"Is on ground: {isOnGround}");
            Debug.WriteLine($"Astronaut Position after collisions: X={position.X}, Y={position.Y}");
        }

        private void UpdateAnimations()
        {
            if (isOnGround)
            {
                if (velocity.X != 0)
                {
                    // Set the "Running" animation when moving horizontally
                    animationManager.SetCurrentAnimation("Running");
                }
                else
                {
                    // Set the "Idle" animation when on the ground and not moving
                    animationManager.SetCurrentAnimation("Idle");
                }
            }
            if(isJumping)
            {
                // Set the "Jumping" animation when jumping
                animationManager.SetCurrentAnimation("Jumping");
            }
        }

        public void ResetPosition(Vector2 vector)
        {
            position = initialPosition; // Reset the position to the initial position
        }

        public void Update()
        {
            HandleInput();

            if (isOnGround)
            {
                // If on the ground, reset jump variables
                jumpCount = 0;
            }
            if (isJumping)
            {
                position += velocity * Globals.time;
                velocity.Y += gravity * Globals.time;

                if (velocity.Y >= 0)
                {
                    isJumping = false;
                }
            }
            else
            {
                velocity.Y += gravity * Globals.time;

                // Move the astronaut
                position += velocity * Globals.time;

                // Check collisions with the map
                HandleCollisions();

                // Update animations based on movement
                UpdateAnimations();

            }

            Debug.WriteLine($"Astronaut Position: X={position.X}, Y={position.Y}");
        }

        public void Draw()
        {
            // Get the current animation and draw it
            Animation currentAnimation = animationManager.GetCurrentAnimation();

            if (currentAnimation != null)
            {
                Rectangle sourceRect = currentAnimation.GetNextFrame();
                Rectangle destinationRect = new Rectangle((int)position.X, (int)position.Y, sourceRect.Width, sourceRect.Height);

                SpriteEffects spriteEffects = isFacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

                Globals.spriteBatch.Draw(currentAnimation.texture, destinationRect, sourceRect, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
            }
        }
    }
}

