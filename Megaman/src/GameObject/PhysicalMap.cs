﻿using Megaman.src.Effect;
using Megaman.src.State;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Megaman.src.GameObject
{
    public class PhysicalMap : GameObject
    {

        public int[,] phys_map;
        private int tileSize;

        public PhysicalMap(float x, float y, GameWorldState gameWorld) : base(x, y, gameWorld)
        {

            this.tileSize = 30;
            phys_map = CacheDataLoader.getInstance().getPhysicalMap();
        }

        public int getTileSize()
        {
            return tileSize;
        }

        //@Override
        public override void Update(GameTime gameTime) { }


        public Rectangle haveCollisionWithTop(Rectangle rect)
        {
            int posX1 = rect.X / tileSize;
            posX1 -= 2;
            int posX2 = (rect.X + rect.Width) / tileSize;
            posX2 += 2;

            //int posY = (rect.y + rect.height)/tileSize;
            int posY = rect.Y / tileSize;

            if (posX1 < 0) posX1 = 0;

            if (posX2 >= phys_map.GetLength(1)) posX2 = phys_map.GetLength(1) - 1;

            for (int y = posY; y >= 0; y--)
            {
                for (int x = posX1; x <= posX2; x++)
                {

                      if (phys_map[y, x] == 1)
                    {
                        Rectangle r = new Rectangle((int)getPosX() + x * tileSize, (int)getPosY() + y * tileSize, tileSize, tileSize);
                        if (rect.IntersectsWith(r))
                            return r;
                    }
                }
            }
            return Rectangle.Empty;

        }


        public Rectangle haveCollisionWithLand(Rectangle rect)
        {

            int posX1 = rect.X / tileSize;
            posX1 -= 2;
            int posX2 = (rect.X + rect.Width) / tileSize;
            posX2 += 2;

            int posY = (rect.Y + rect.Height) / tileSize;

            if (posX1 < 0) posX1 = 0;

            if (posX2 >= phys_map.GetLength(1)) 
                posX2 = phys_map.GetLength(1) - 1;
            for (int y = posY; y < phys_map.GetLength(0); y++)
            {
                for (int x = posX1; x <= posX2; x++)
                {

                    if (phys_map[y,x] == 1)
                    {
                        Rectangle r = new Rectangle((int)getPosX() + x * tileSize, (int)getPosY() + y * tileSize, tileSize, tileSize);
                        if (rect.IntersectsWith(r))
                            return r;
                    }
                }
            }
            return Rectangle.Empty;
        }

        public Rectangle haveCollisionWithRightWall(Rectangle rect)
        {


            int posY1 = rect.Y / tileSize;
            posY1 -= 2;
            int posY2 = (rect.Y + rect.Height) / tileSize;
            posY2 += 2;

            int posX1 = (rect.X + rect.Width) / tileSize;
            int posX2 = posX1 + 3;
            if (posX2 >= phys_map.GetLength(1)) posX2 = phys_map.GetLength(1) - 1;

            if (posY1 < 0) posY1 = 0;
            if (posY2 >= phys_map.GetLength(0)) posY2 = phys_map.GetLength(0) - 1;


            for (int x = posX1; x <= posX2; x++)
            {
                for (int y = posY1; y <= posY2; y++)
                {
                    if (phys_map[y, x] == 1)
                    {
                        Rectangle r = new Rectangle((int)getPosX() + x * tileSize, (int)getPosY() + y * tileSize, tileSize, tileSize);
                        if (r.Y < rect.Y + rect.Height - 1 && rect.IntersectsWith(r))
                            return r;
                    }
                }
            }
            return Rectangle.Empty;

        }

        public Rectangle haveCollisionWithLeftWall(Rectangle rect)
        {



            int posY1 = rect.Y / tileSize;
            posY1 -= 2;
            int posY2 = (rect.Y + rect.Height) / tileSize;
            posY2 += 2;

            int posX1 = (rect.X + rect.Width) / tileSize;
            int posX2 = posX1 - 3;
            if (posX2 < 0) posX2 = 0;

            if (posY1 < 0) posY1 = 0;
            if (posY2 >= phys_map.GetLength(0)) posY2 = phys_map.GetLength(0) - 1;


            for (int x = posX1; x >= posX2; x--)
            {
                for (int y = posY1; y <= posY2; y++)
                {
                    if (phys_map[y, x] == 1)
                    {
                        Rectangle r = new Rectangle((int)getPosX() + x * tileSize, (int)getPosY() + y * tileSize, tileSize, tileSize);
                        if (r.Y < rect.Y + rect.Height - 1 && rect.IntersectsWith(r))
                            return r;
                    }
                }
            }
            return Rectangle.Empty;

        }

        public void draw(Graphics g2)
        {

            Camera camera = getGameWorld().camera;
            SolidBrush brush = new SolidBrush(Color.Gray);
            //g2.setColor(Color.GRAY);
            for (int i = 0; i < phys_map.GetLength(0); i++)
                for (int j = 0; j < phys_map.GetLength(1); j++)
                    if (phys_map[i,j] != 0) g2.FillRectangle(brush,(int)getPosX() + j * tileSize - (int)camera.getPosX(),
                               (int)getPosY() + i * tileSize - (int)camera.getPosY(), tileSize, tileSize);

        }

    }
}
