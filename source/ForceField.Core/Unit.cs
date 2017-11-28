using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ForceField.Core
{
    public class UnitOfOneType
    {
        #region Properties

        public int NumberInArray
        {
            get { return numberInArray; }
            set { numberInArray = value; }
        }

        public MetaUnitType CurrentUnitType
        {
            get { return currentUnitType; }
            set { currentUnitType = value; }
        }

        #endregion

        public UnitOfOneType(MetaUnitType nfo)
        {
            CurrentUnitType = new MetaUnitType();
            CurrentUnitType = nfo;
            CurrentUnitType.DefineTextureNumber();
        }

        /// <summary>
        /// Создаёт экземпляр юнита
        /// </summary>
        public void AddUnitExemplar(MetaUnitEx ExemplarData)
        {
            MetaUnitEx Ex = new MetaUnitEx();
            Ex = ExemplarData;
            CurrentUnitType.Exemplars.Add(Ex);
        }

        /// <summary>
        /// Удаляет экземпляр юнита
        /// </summary>
        public void DeleteUnitEx(int exNumber)
        {
            CurrentUnitType.Exemplars.RemoveAt(exNumber);
        }

        /// <summary>
        /// Удаляет все экземпляры юнита
        /// </summary>
        public void DeleteAllUnitExes()
        {
            for (int i = 0; i < CurrentUnitType.Exemplars.Count; ++i)
            {
                CurrentUnitType.Exemplars.RemoveAt(i);
            }
        }

        /// <summary>
        /// выделение, обновление фрейма, перемещение
        /// </summary>
        public void Interaction()
        {
            for (int i = 0; i < CurrentUnitType.Exemplars.Count; ++i)
            {
                if (ForceField.Core.SpriteBatcher.IsOnScreen(TextureManager.Textures[CurrentUnitType.TextureNumberInArray].Texture.Width, 
                    TextureManager.Textures[CurrentUnitType.TextureNumberInArray].Texture.Height,
                    CurrentUnitType.Exemplars[i].UnitExPosition, CurrentUnitType.FrameCount, 
                    CurrentUnitType.AnimationCount, CurrentUnitType.Size)) 
                {
                    if (CurrentUnitType.Exemplars[i].TotalElapsed < 100000)
                    {
                        CurrentUnitType.Exemplars[i].TotalElapsed += Depository.Elapsed;
                    }

                    if (CurrentUnitType.Exemplars[i].TotalElapsed > CurrentUnitType.TimeFrame)
                    {
                        CurrentUnitType.Exemplars[i].FrameNow++;
                        CurrentUnitType.Exemplars[i].FrameNow = CurrentUnitType.Exemplars[i].FrameNow % 
                            (CurrentUnitType.FrameCount);
                        CurrentUnitType.Exemplars[i].TotalElapsed -= CurrentUnitType.TimeFrame;
                    }   
    
                    if (Depository.CanSelect || !Config.MapMakingMode)
                    {
                        Vector2 lpoint = new Vector2(TransformationController.CameraPosition.X - 
                            Depository.ScreenWidth / 2 / TransformationController.Zoom, 
                            TransformationController.CameraPosition.Y -
                            Depository.ScreenHeight / 2 / TransformationController.Zoom);
                            int from_x = (int)((CurrentUnitType.Exemplars[i].UnitExPosition.X - 
                            lpoint.X) * TransformationController.Zoom);

                        int from_y = (int)((CurrentUnitType.Exemplars[i].UnitExPosition.Y - lpoint.Y) * 
                            TransformationController.Zoom);

                        int to_x = (int)((CurrentUnitType.Exemplars[i].UnitExPosition.X + 
                            TextureManager.Textures[CurrentUnitType.TextureNumberInArray].Texture.Width * 
                            CurrentUnitType.Size / CurrentUnitType.FrameCount - lpoint.X) * 
                            TransformationController.Zoom);

                        int to_y = (int)((CurrentUnitType.Exemplars[i].UnitExPosition.Y + 
                            TextureManager.Textures[CurrentUnitType.TextureNumberInArray].Texture.Height *
                            CurrentUnitType.Size / CurrentUnitType.AnimationCount - lpoint.Y) *
                            TransformationController.Zoom);

                        if (Depository.MouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed 
                            && !UnitsArray.SmthSelectedAndDragging)
                        {
                            if ((Depository.MouseState.X > from_x & Depository.MouseState.X < to_x & 
                                Depository.MouseState.Y > from_y & Depository.MouseState.Y < to_y
                                & CurrentUnitType.Exemplars[i].Plan >= Depository.NowSelectedPlan) || 
                                CurrentUnitType.Exemplars[i].Dragging)
                            {
                                this.CurrentUnitType.Exemplars[i].Selected = true;
                                Depository.NowSelectedPlan = CurrentUnitType.Exemplars[i].Plan;
                                Depository.UnitSelectedNow = this.NumberInArray;
                                Depository.ExSelectedNow = i;
                            }
                            else
                            {
                                if (this.CurrentUnitType.Exemplars[i].Selected)
                                {
                                    Depository.NowSelectedPlan = 0.0f;
                                }

                                this.CurrentUnitType.Exemplars[i].Selected = false;
                            }
                        }
                
                        if (Config.MapMakingMode) 
                            Dragging(i);                    
                    }
                }   
            }
        }

        /// <summary>
        ///  Рисует все экземпляры юнита
        /// </summary>
        public void DrawAllUnitEx()
        {
            for (int i = 0; i < CurrentUnitType.Exemplars.Count; ++i) 
            {
                if (ForceField.Core.SpriteBatcher.IsOnScreen(TextureManager.Textures[CurrentUnitType.TextureNumberInArray].Texture.Width, 
                    TextureManager.Textures[CurrentUnitType.TextureNumberInArray].Texture.Height, CurrentUnitType.Exemplars[i].UnitExPosition, 
                    CurrentUnitType.FrameCount, CurrentUnitType.AnimationCount, CurrentUnitType.Size)) 
                {
                    int frameWidth = TextureManager.Textures[CurrentUnitType.TextureNumberInArray].Texture.Width 
                        / CurrentUnitType.FrameCount;

                    int frameHeight = TextureManager.Textures[CurrentUnitType.TextureNumberInArray].Texture.Height 
                        / CurrentUnitType.AnimationCount;

                    Rectangle rectangle = new Rectangle(frameWidth * CurrentUnitType.Exemplars[i].FrameNow, frameHeight * 
                        CurrentUnitType.Exemplars[i].AnimationNow, frameWidth, frameHeight);

                    Depository.SpriteBatch.Draw(TextureManager.Textures[CurrentUnitType.TextureNumberInArray].Texture, 
                        CurrentUnitType.Exemplars[i].UnitExPosition, rectangle, Color.White, 0, Vector2.Zero, 
                        CurrentUnitType.Size, SpriteEffects.None, 1 - CurrentUnitType.Exemplars[i].Plan);
                }

                if (CurrentUnitType.Exemplars[i].Selected)
                {
                    float size = 1;

                    if (!Config.SelectorsDependsOnZoom) size = 1 / TransformationController.Zoom;

                    Vector2 fromR = new Vector2(CurrentUnitType.Exemplars[i].UnitExPosition.X, CurrentUnitType.Exemplars[i].UnitExPosition.Y);
                    Vector2 toR = new Vector2((CurrentUnitType.Exemplars[i].UnitExPosition.X + 
                        TextureManager.Textures[CurrentUnitType.TextureNumberInArray].Texture.Width 
                        / CurrentUnitType.FrameCount * CurrentUnitType.Size), (CurrentUnitType.Exemplars[i].UnitExPosition.Y 
                        + TextureManager.Textures[CurrentUnitType.TextureNumberInArray].Texture.Height 
                        / CurrentUnitType.AnimationCount * CurrentUnitType.Size));

                    Depository.SpriteBatch.Draw(CurrentUnitType.SelectorTexture, new Vector2(fromR.X, fromR.Y), 
                        new Rectangle(0, 0, CurrentUnitType.SelectorTexture.Width, CurrentUnitType.SelectorTexture.Height), 
                        Color.White, 0, Vector2.Zero, size, SpriteEffects.None, 0);

                    Depository.SpriteBatch.Draw(CurrentUnitType.SelectorTexture, new Vector2(toR.X, fromR.Y), 
                        new Rectangle(0, 0, CurrentUnitType.SelectorTexture.Width, CurrentUnitType.SelectorTexture.Height), 
                        Color.White, 0, Vector2.Zero, size, SpriteEffects.FlipHorizontally, 0);

                    Depository.SpriteBatch.Draw(CurrentUnitType.SelectorTexture, new Vector2(fromR.X, toR.Y), 
                        new Rectangle(0, 0, CurrentUnitType.SelectorTexture.Width, CurrentUnitType.SelectorTexture.Height),
                        Color.White, 0, Vector2.Zero, size, SpriteEffects.FlipVertically, 0);

                    Depository.SpriteBatch.Draw(CurrentUnitType.SelectorTexture, new Vector2(toR.X, toR.Y), 
                        new Rectangle(0, 0, CurrentUnitType.SelectorTexture.Width, CurrentUnitType.SelectorTexture.Height), 
                        Color.White, 0, Vector2.Zero, size, SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically, 0);
                }
            }
        }

        #region private

        private int numberInArray;
        private MetaUnitType currentUnitType;

        /// <summary>
        ///  перемещение
        /// </summary>
        private void Dragging(int ex_now)
        {
            if (Depository.MouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed &
                CurrentUnitType.Exemplars[ex_now].Selected && (Depository.MouseWasX != Depository.MouseState.X
                | Depository.MouseWasY != Depository.MouseState.Y | TransformationController.IsCamMovingNow))
            {
                CurrentUnitType.Exemplars[ex_now].Dragging = true;

                Depository.MouseWasX = Depository.MouseState.X;
                Depository.MouseWasY = Depository.MouseState.Y;
            }

            if (Depository.MouseState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                CurrentUnitType.Exemplars[ex_now].Dragging = false;

            if (CurrentUnitType.Exemplars[ex_now].Dragging)
            {
                switch (Config.MeshLock)
                {
                    case 0:
                        CurrentUnitType.Exemplars[ex_now].UnitExPosition =
                            new Vector2((Depository.MouseGlobal.X -
                                (float)(ForceField.Core.TextureManager.Textures[CurrentUnitType.TextureNumberInArray].Texture.Width /
                                CurrentUnitType.FrameCount * CurrentUnitType.Size / 2)), (Depository.MouseGlobal.Y -
                                (float)(ForceField.Core.TextureManager.Textures[CurrentUnitType.TextureNumberInArray].Texture.Height /
                                CurrentUnitType.AnimationCount * CurrentUnitType.Size / 2)));
                        break;

                    case 1:
                        int tmp_x = Math.Abs((int)Depository.MouseGlobal.X);
                        tmp_x -= tmp_x % Config.MeshRegionSize;
                        if ((Math.Abs(Depository.MouseGlobal.X) - tmp_x) > Config.MeshRegionSize / 2)
                            tmp_x += Config.MeshRegionSize;
                        if (Depository.MouseGlobal.X < 0)
                            tmp_x *= -1;

                        int tmp_y = Math.Abs((int)Depository.MouseGlobal.Y);
                        tmp_y -= tmp_y % Config.MeshRegionSize;
                        if ((Math.Abs(Depository.MouseGlobal.Y) - tmp_y) > Config.MeshRegionSize / 2)
                            tmp_y += Config.MeshRegionSize;

                        if (Depository.MouseGlobal.Y < 0)
                            tmp_y *= -1;

                        CurrentUnitType.Exemplars[ex_now].UnitExPosition = new Vector2(tmp_x -
                            Config.MeshRegionSize, tmp_y - Config.MeshRegionSize);

                        break;
                }
            }

        }

        #endregion
    }  
}