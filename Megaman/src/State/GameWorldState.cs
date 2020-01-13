
using Megaman.src.Effect;
using Megaman.src.GameObject;
using Megaman.src.UserInterface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Megaman.src.State
{
    public class GameWorldState : State
    {


        private Image bufferedImage;
        private GameState lastState;

        public ParticularObjectManager particularObjectManager;
        public BulletManager bulletManager;

        public MegaMan megaMan;

        public PhysicalMap physicalMap;
        public BackgroundMap backgroundMap;
        public Camera camera;

        public static readonly int readonlyBossX = 3600;
        public enum GameState { INIT_GAME, TUTORIAL, GAMEPLAY, GAMEOVER, GAMEWIN, PAUSEGAME }
        //public static readonly int INIT_GAME = 0;
        //public static readonly int TUTORIAL = 1;
        //public static readonly int GAMEPLAY = 2;
        //public static readonly int GAMEOVER = 3;
        //public static readonly int GAMEWIN = 4;
        //public static readonly int PAUSEGAME = 5;
        public enum TutorialState { INTROGAME, MEETreadonlyBOSS }
        //public static readonly int INTROGAME = 0;
        //public static readonly int MEETreadonlyBOSS = 1;

        public int openIntroGameY = 0;
        public GameState state = GameState.INIT_GAME;
        public GameState previousState;
        public TutorialState tutorialState = TutorialState.INTROGAME;

        public int storyTutorial = 0;
        public String[] texts1 = new String[4];

        public String textTutorial;
        public int currentSize = 1;

        private bool readonlybossTrigger = true;
        ParticularObject boss;

        FrameImage avatar = CacheDataLoader.getInstance().getFrameImage("avatar");


        private int numberOfLife = 3;

        //public AudioClip bgMusic;

        public GameWorldState(GamePanel gamePanel, GameTime time) : base(gamePanel, time)
        {
            texts1[0] = "We are heros, and our mission is protecting our Home\nEarth....";
            texts1[1] = "There was a Monster from University on Earth in 10 years\n"
                    + "and we lived in the scare in that 10 years....";
            texts1[2] = "Now is the time for us, kill it and get freedom!....";
            texts1[3] = "      LET'S GO!.....";
            textTutorial = texts1[0];


            bufferedImage = new Bitmap(GameFrame.SCREEN_WIDTH, GameFrame.SCREEN_HEIGHT);
            megaMan = new MegaMan(400, 400, this);
            physicalMap = new PhysicalMap(0, 0, this);
            backgroundMap = new BackgroundMap(0, 0, this);
            camera = new Camera(0, 50, GameFrame.SCREEN_WIDTH, GameFrame.SCREEN_HEIGHT, this);
            bulletManager = new BulletManager(this);

            particularObjectManager = new ParticularObjectManager(this);
            particularObjectManager.addObject(megaMan);

            initEnemies();

            //bgMusic = CacheDataLoader.getInstance().getSound("bgmusic");
            previousState = state;
        }

        private void initEnemies()
        {
            ParticularObject redeye = new RedEyeDevil(1250, 410, this);
            redeye.setDirection(ParticularObject.MainDir.LEFT_DIR);
            redeye.setTeamType(ParticularObject.TeamType.ENEMY_TEAM);
            particularObjectManager.addObject(redeye);

            ParticularObject smallRedGun = new SmallRedGun(1600, 180, this);
            smallRedGun.setDirection(ParticularObject.MainDir.LEFT_DIR);
            smallRedGun.setTeamType(ParticularObject.TeamType.ENEMY_TEAM);
            particularObjectManager.addObject(smallRedGun);

            ParticularObject darkraise = new DarkRaise(2000, 200, this);
            darkraise.setTeamType(ParticularObject.TeamType.ENEMY_TEAM);
            particularObjectManager.addObject(darkraise);

            ParticularObject darkraise2 = new DarkRaise(2800, 350, this);
            darkraise2.setTeamType(ParticularObject.TeamType.ENEMY_TEAM);
            particularObjectManager.addObject(darkraise2);

            ParticularObject robotR = new RobotR(900, 400, this);
            robotR.setTeamType(ParticularObject.TeamType.ENEMY_TEAM);
            particularObjectManager.addObject(robotR);

            ParticularObject robotR2 = new RobotR(3400, 350, this);
            robotR2.setTeamType(ParticularObject.TeamType.ENEMY_TEAM);
            particularObjectManager.addObject(robotR2);


            ParticularObject redeye2 = new RedEyeDevil(2500, 500, this);
            redeye2.setDirection(ParticularObject.MainDir.LEFT_DIR);
            redeye2.setTeamType(ParticularObject.TeamType.ENEMY_TEAM);
            particularObjectManager.addObject(redeye2);

            ParticularObject redeye3 = new RedEyeDevil(3450, 500, this);
            redeye3.setDirection(ParticularObject.MainDir.LEFT_DIR);
            redeye3.setTeamType(ParticularObject.TeamType.ENEMY_TEAM);
            particularObjectManager.addObject(redeye3);

            ParticularObject redeye4 = new RedEyeDevil(500, 1190, this);
            redeye4.setDirection(ParticularObject.MainDir.RIGHT_DIR);
            redeye4.setTeamType(ParticularObject.TeamType.ENEMY_TEAM);
            particularObjectManager.addObject(redeye4);


            ParticularObject darkraise3 = new DarkRaise(750, 650, this);
            darkraise3.setTeamType(ParticularObject.TeamType.ENEMY_TEAM);
            particularObjectManager.addObject(darkraise3);

            ParticularObject robotR3 = new RobotR(1500, 1150, this);
            robotR3.setTeamType(ParticularObject.TeamType.ENEMY_TEAM);
            particularObjectManager.addObject(robotR3);



            ParticularObject smallRedGun2 = new SmallRedGun(1700, 980, this);
            smallRedGun2.setDirection(ParticularObject.MainDir.LEFT_DIR);
            smallRedGun2.setTeamType(ParticularObject.TeamType.ENEMY_TEAM);
            particularObjectManager.addObject(smallRedGun2);
        }

        public void switchState(GameState state)
        {
            previousState = this.state;
            this.state = state;
        }

        private void TutorialUpdate()
        {
            switch (tutorialState)
            {
                case TutorialState.INTROGAME:

                    if (storyTutorial == 0)
                    {
                        if (openIntroGameY < 450)
                        {
                            openIntroGameY += 4;
                        }
                        else storyTutorial++;

                    }
                    else
                    {

                        if (currentSize < textTutorial.Length) currentSize++;
                    }
                    break;
                case TutorialState.MEETreadonlyBOSS:
                    if (storyTutorial == 0)
                    {
                        if (openIntroGameY >= 450)
                        {
                            openIntroGameY -= 1;
                        }
                        if (camera.getPosX() < readonlyBossX)
                        {
                            camera.setPosX(camera.getPosX() + 2);
                        }

                        if (megaMan.getPosX() < readonlyBossX + 150)
                        {
                            megaMan.setDirection(ParticularObject.MainDir.RIGHT_DIR);
                            megaMan.run();
                            megaMan.Update(gameTime);
                        }
                        else
                        {
                            megaMan.stopRun();
                        }

                        if (openIntroGameY < 450 && camera.getPosX() >= readonlyBossX && megaMan.getPosX() >= readonlyBossX + 150)
                        {
                            camera.Lock();
                            storyTutorial++;
                            megaMan.stopRun();
                            physicalMap.phys_map[14, 120] = 1;
                            physicalMap.phys_map[15, 120] = 1;
                            physicalMap.phys_map[16, 120] = 1;
                            physicalMap.phys_map[17, 120] = 1;

                            backgroundMap.map[14, 120] = 17;
                            backgroundMap.map[15, 120] = 17;
                            backgroundMap.map[16, 120] = 17;
                            backgroundMap.map[17, 120] = 17;
                        }

                    }
                    else
                    {

                        if (currentSize < textTutorial.Length) currentSize++;
                    }
                    break;
            }
        }

        private void drawString(Graphics g2, String text, int x, int y)
        {
            foreach (String str in text.Split(new char[] { '\n' }))
            {

                Font font = new Font("Verdana", 15);
                g2.DrawString(str, font, new SolidBrush(Color.Black), x, y += font.Height);
            }
        }



        private void TutorialRender(Graphics g2)
        {
            SolidBrush brush = new SolidBrush(Color.White);
            switch (tutorialState)
            {
                case TutorialState.INTROGAME:
                    int yMid = GameFrame.SCREEN_HEIGHT / 2 - 15;
                    int y1 = yMid - GameFrame.SCREEN_HEIGHT / 2 - openIntroGameY / 2;
                    int y2 = yMid + openIntroGameY / 2;
                    brush.Color = Color.Black;
                    g2.FillRectangle(brush, 0, y1, GameFrame.SCREEN_WIDTH, GameFrame.SCREEN_HEIGHT / 2);
                    g2.FillRectangle(brush, 0, y2, GameFrame.SCREEN_WIDTH, GameFrame.SCREEN_HEIGHT / 2);

                    if (storyTutorial >= 1)
                    {
                        g2.DrawImage(avatar.getImage(), 600, 350);
                        brush.Color = Color.Blue;
                        g2.FillRectangle(brush, 280, 450, 350, 80);
                        brush.Color = Color.White;
                        String text = textTutorial.Substring(0, currentSize - 1);
                        drawString(g2, text, 290, 480);
                    }

                    break;
                case TutorialState.MEETreadonlyBOSS:
                    yMid = GameFrame.SCREEN_HEIGHT / 2 - 15;
                    y1 = yMid - GameFrame.SCREEN_HEIGHT / 2 - openIntroGameY / 2;
                    y2 = yMid + openIntroGameY / 2;

                    brush.Color = Color.Black;
                    g2.FillRectangle(brush, 0, y1, GameFrame.SCREEN_WIDTH, GameFrame.SCREEN_HEIGHT / 2);
                    g2.FillRectangle(brush, 0, y2, GameFrame.SCREEN_WIDTH, GameFrame.SCREEN_HEIGHT / 2);
                    break;
            }
        }

        public override void Update()
        {

            switch (state)
            {
                case GameState.INIT_GAME:

                    break;
                case GameState.TUTORIAL:
                    TutorialUpdate();

                    break;
                case GameState.GAMEPLAY:
                    particularObjectManager.UpdateObjects(gameTime);
                    bulletManager.UpdateObjects(gameTime);

                    physicalMap.Update(gameTime);
                    camera.Update(gameTime);


                    if (megaMan.getPosX() > readonlyBossX && readonlybossTrigger)
                    {
                        readonlybossTrigger = false;
                        switchState(GameState.TUTORIAL);
                        tutorialState = TutorialState.MEETreadonlyBOSS;
                        storyTutorial = 0;
                        openIntroGameY = 550;

                        boss = new FinalBoss(readonlyBossX + 700, 460, this);
                        boss.setTeamType(ParticularObject.TeamType.ENEMY_TEAM);
                        boss.setDirection(ParticularObject.MainDir.LEFT_DIR);
                        particularObjectManager.addObject(boss);

                    }

                    if (megaMan.getState() == ParticularObject.MainState.DEATH)
                    {
                        numberOfLife--;
                        if (numberOfLife >= 0)
                        {
                            megaMan.setBlood(100);
                            megaMan.setPosY(megaMan.getPosY() - 50);
                            megaMan.setState(ParticularObject.MainState.NOBEHURT);
                            particularObjectManager.addObject(megaMan);
                        }
                        else
                        {
                            switchState(GameState.GAMEOVER);
                            //bgMusic.stop();
                        }
                    }
                    if (!readonlybossTrigger && boss.getState() == ParticularObject.MainState.DEATH)
                        switchState(GameState.GAMEWIN);

                    break;
                case GameState.GAMEOVER:

                    break;
                case GameState.GAMEWIN:

                    break;
            }


        }

        public override void Render(Graphics g2)
        {

            //Graphics g2 = Graphics.FromImage(bufferedImage);
            SolidBrush brush = new SolidBrush(Color.White);
            Font font = new Font("Verdana", 14);
            if (g2 != null)
            {

                // NOTE: two lines below make the error splash white screen....
                // need to remove this line
                ////g2.setColor(Color.WHITE);
                //g2.FillRectangle(brush,0, 0, GameFrame.SCREEN_WIDTH, GameFrame.SCREEN_HEIGHT);


                //physicalMap.draw(g2);

                switch (state)
                {
                    case GameState.INIT_GAME:
                        brush.Color = Color.Black;
                        g2.FillRectangle(brush, 0, 0, GameFrame.SCREEN_WIDTH, GameFrame.SCREEN_HEIGHT);
                        brush.Color = Color.White;
                        g2.DrawString("PRESS ENTER TO CONTINUE", font, brush, 400, 300);
                        break;
                    case GameState.PAUSEGAME:
                        brush.Color = Color.Black;
                        g2.FillRectangle(brush, 300, 260, 500, 70);
                        brush.Color = Color.White;
                        g2.DrawString("PRESS ENTER TO CONTINUE", font, brush, 400, 300);
                        break;
                    case GameState.TUTORIAL:
                        backgroundMap.draw(g2);
                        if (tutorialState == TutorialState.MEETreadonlyBOSS)
                        {
                            particularObjectManager.draw(g2, gameTime);
                        }
                        TutorialRender(g2);

                        break;
                    case GameState.GAMEWIN:
                    case GameState.GAMEPLAY:
                        backgroundMap.draw(g2);
                        particularObjectManager.draw(g2, gameTime);
                        bulletManager.draw(g2, gameTime);

                        brush.Color = Color.Gray;
                        g2.FillRectangle(brush, 19, 59, 102, 22);
                        brush.Color = Color.Red;
                        g2.FillRectangle(brush, 20, 60, megaMan.getBlood(), 20);

                        for (int i = 0; i < numberOfLife; i++)
                        {
                            g2.DrawImage(CacheDataLoader.getInstance().getFrameImage("hearth").getImage(), 20 + i * 40, 18);
                        }


                        if (state == GameState.GAMEWIN)
                        {
                            g2.DrawImage(CacheDataLoader.getInstance().getFrameImage("gamewin").getImage(), 300, 300);
                        }

                        break;
                    case GameState.GAMEOVER:
                        brush.Color = Color.Black;
                        g2.FillRectangle(brush, 0, 0, GameFrame.SCREEN_WIDTH, GameFrame.SCREEN_HEIGHT);
                        brush.Color = Color.White;
                        g2.DrawString("GAME OVER!", font, brush, 450, 300);
                        break;

                }


            }

        }

        public override Image getBufferedImage()
        {
            return bufferedImage;
        }

        public override void setPressedButton(Keys code)
        {
            switch (code)
            {

                case Keys.Down:
                    megaMan.dick();
                    break;

                case Keys.Right:
                    megaMan.setDirection(ParticularObject.MainDir.RIGHT_DIR);
                    megaMan.run();
                    break;

                case Keys.Left:
                    megaMan.setDirection(ParticularObject.MainDir.LEFT_DIR);
                    megaMan.run();
                    break;

                case Keys.Enter:
                    if (state == GameState.INIT_GAME)
                    {
                        if (previousState == GameState.GAMEPLAY)
                            switchState(GameState.GAMEPLAY);
                        else switchState(GameState.TUTORIAL);

                        //bgMusic.loop();
                        //bgMusic.play();
                    }
                    if (state == GameState.TUTORIAL && storyTutorial >= 1)
                    {
                        if (storyTutorial <= 3)
                        {
                            storyTutorial++;
                            currentSize = 1;
                            textTutorial = texts1[storyTutorial - 1];
                        }
                        else
                        {
                            switchState(GameState.GAMEPLAY);
                        }

                        // for meeting boss tutorial
                        if (tutorialState == TutorialState.MEETreadonlyBOSS)
                        {
                            switchState(GameState.GAMEPLAY);
                        }
                    }
                    break;

                case Keys.Space:
                    megaMan.jump();
                    break;

                case Keys.A:
                    megaMan.attack(gameTime);
                    break;

            }
        }

        //@Override
        public override void setReleasedButton(Keys code)
        {
            switch (code)
            {

                case Keys.Up:

                    break;

                case Keys.Down:
                    megaMan.standUp();
                    break;

                case Keys.Right:
                    if (megaMan.getSpeedX() > 0)
                        megaMan.stopRun();
                    break;

                case Keys.Left:
                    if (megaMan.getSpeedX() < 0)
                        megaMan.stopRun();
                    break;

                case Keys.Enter:
                    if (state == GameState.GAMEOVER || state == GameState.GAMEWIN)
                    {
                        //gamePanel.setState(new MenuState(gamePanel, gameTime));
                    }
                    else if (state == GameState.PAUSEGAME)
                    {
                        state = lastState;
                    }
                    break;

                case Keys.Space:

                    break;

                case Keys.A:

                    break;
                case Keys.Escape:
                    lastState = state;
                    state = GameState.PAUSEGAME;
                    break;

            }
        }

    }
}
