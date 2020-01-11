using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Megaman.src.Effect
{
    public class CacheDataLoader
    {

        private static CacheDataLoader instance = null;

        private String framefile = "data/frame.txt";
        private String animationfile = "data/animation.txt";
        private String physmapfile = "data/phys_map.txt";
        private String backgroundmapfile = "data/background_map.txt";
        private String soundfile = "data/sounds.txt";

        private Dictionary<String, FrameImage> frameImages;
        private Dictionary<String, Animation> animations;
        //private Dictionary<String, AudioClip> sounds;

        private int[,] phys_map;
        private int[,] background_map;

        private CacheDataLoader() { }

        public static CacheDataLoader getInstance()
        {
            if (instance == null)
                instance = new CacheDataLoader();
            return instance;
        }

        //public AudioClip getSound(String name)
        //{
        //    return instance.sounds[name];
        //}

        public Animation getAnimation(String name)
        {

            Animation animation = new Animation(instance.animations[name]);
            return animation;

        }

        public FrameImage getFrameImage(String name)
        {

            FrameImage frameImage = new FrameImage(instance.frameImages[name]);
            return frameImage;

        }

        public int[,] getPhysicalMap()
        {
            return instance.phys_map;
        }

        public int[,] getBackgroundMap()
        {
            return instance.background_map;
        }

        public void LoadData()//throws IOException
        {

            LoadFrame();
            LoadAnimation();
            LoadPhysMap();
            LoadBackgroundMap();
            //LoadSounds();

        }

        //public void LoadSounds() //throws IOException
        //{
        //    sounds = new Dictionary<String, AudioClip>();

        //    FileReader fr = new FileReader(soundfile);
        //    BufferedReader br = new BufferedReader(fr);

        //    String line = null;

        //    if (br.ReadLine() == null)
        //    { // no line = "" or something like that
        //       MessageBox.Show("No data");
        //        throw new IOException();
        //    }
        //    else
        //    {

        //        fr = new FileReader(soundfile);
        //        br = new BufferedReader(fr);

        //        while ((line = br.ReadLine()).Equals("")) ;

        //        int n = Convert.ToInt32(line);

        //        for (int i = 0; i < n; i++)
        //        {

        //            AudioClip audioClip = null;
        //            while ((line = br.ReadLine()).Equals("")) ;

        //            String[] str = line.Split(new char[] { ' ' });
        //            String name = str[0];

        //            String path = str[1];

        //            try
        //            {
        //                audioClip = Applet.newAudioClip(new URL("file", "", str[1]));

        //            }
        //            catch (MalformedURLException ex) { }

        //            instance.sounds.put(name, audioClip);
        //        }

        //    }

        //    br.Close();

        //}

        public void LoadBackgroundMap() //throws IOException
        {

            //FileReader fr = new FileReader(backgroundmapfile);
            StreamReader br = new StreamReader(Properties.Resources.background_map);

            String line = null;

            line = br.ReadLine();
            int numberOfRows = Convert.ToInt32(line);
            line = br.ReadLine();
            int numberOfColumns = Convert.ToInt32(line);


            instance.background_map = new int[numberOfRows, numberOfColumns];

            for (int i = 0; i < numberOfRows; i++)
            {
                line = br.ReadLine();
                String[] str = line.Split(new char[] { ' ' });
                for (int j = 0; j < numberOfColumns; j++)
                    instance.background_map[i, j] = Convert.ToInt32(str[j]);
            }

            //for (int i = 0; i < numberOfRows; i++)
            //{

            //    for (int j = 0; j < numberOfColumns; j++)
            //        System.out.print(new char[] { ' ' } + instance.background_map[i][j]);
            //}

            br.Close();

        }

        public void LoadPhysMap() //throws IOException
        {

            //FileReader fr = new FileReader(physmapfile);
            StreamReader br = new StreamReader(Properties.Resources.phys_map);

            String line = null;

            line = br.ReadLine();
            int numberOfRows = Convert.ToInt32(line);
            line = br.ReadLine();
            int numberOfColumns = Convert.ToInt32(line);


            instance.phys_map = new int[numberOfRows, numberOfColumns];

            for (int i = 0; i < numberOfRows; i++)
            {
                line = br.ReadLine();
                String[] str = line.Split(new char[] { ' ' });
                for (int j = 0; j < numberOfColumns; j++)
                    instance.phys_map[i, j] = Convert.ToInt32(str[j]);
            }

            //for (int i = 0; i < numberOfRows; i++)
            //{

            //    for (int j = 0; j < numberOfColumns; j++)
            //        System.out.print(" " + instance.phys_map[i][j]);
            //}

            br.Close();

        }
        public void LoadAnimation() //throws IOException
        {

            animations = new Dictionary<String, Animation>();

            //FileReader fr = new FileReader(animationfile);
            //BufferedReader br = new BufferedReader(fr);
            StreamReader br = new StreamReader(Properties.Resources.animation);
            String line = null;

            if (br.ReadLine() == null)
            {
                MessageBox.Show("No data");
                throw new IOException();
            }
            else
            {

                //fr = new FileReader(animationfile);
                br = new StreamReader(Properties.Resources.animation);

                while ((line = br.ReadLine()).Equals("")) ;
                int n = Convert.ToInt32(line);

                for (int i = 0; i < n; i++)
                {

                    Animation animation = new Animation();

                    while ((line = br.ReadLine()).Equals("")) ;
                    animation.setName(line);

                    while ((line = br.ReadLine()).Equals("")) ;
                    String[] str = line.Split(new char[] { ' ' });

                    for (int j = 0; j < str.Length; j += 2)
                        animation.add(getFrameImage(str[j]), Convert.ToDouble(str[j + 1]));

                    instance.animations.Add(animation.getName(), animation);

                }

            }

            br.Close();
        }

        public void LoadFrame()// throws IOException
        {

            frameImages = new Dictionary<string, FrameImage>();
            StreamReader br = new StreamReader(Properties.Resources.frame);
            //FileReader fr = new FileReader(framefile);
            //BufferedReader br = new BufferedReader(fr);

            String line = null;

            if (br.ReadLine() == null)
            {
                MessageBox.Show("No data");
                throw new IOException();
            }
            else
            {

                //fr = new FileReader(framefile);
                br = new StreamReader(Properties.Resources.frame);

                while ((line = br.ReadLine()).Equals("")) ;
                int n = Convert.ToInt32(line);

                for (int i = 0; i < n; i++)
                {

                    FrameImage frame = new FrameImage();
                    while ((line = br.ReadLine()).Equals("")) ;
                    frame.setName(line);

                    while ((line = br.ReadLine()).Equals("")) ;
                    string[] str = line.Split(new char[] { ' ' });
                    string path = str[1];

                    while ((line = br.ReadLine()).Equals("")) ;
                    str = line.Split(new char[] { ' ' });
                    int x = Convert.ToInt32(str[1]);

                    while ((line = br.ReadLine()).Equals("")) ;
                    str = line.Split(new char[] { ' ' });
                    int y = Convert.ToInt32(str[1]);

                    while ((line = br.ReadLine()).Equals("")) ;
                    str = line.Split(new char[] { ' ' });
                    int w = Convert.ToInt32(str[1]);

                    while ((line = br.ReadLine()).Equals("")) ;
                    str = line.Split(new char[] { ' ' });
                    int h = Convert.ToInt32(str[1]);

                    //Image imageData = ImageIO.read(new File(path));
                    Image imageData = Image.FromFile(path);
                    //Image image = imageData.getSubimage(x, y, w, h);
                    Bitmap bitmap = new Bitmap(imageData);
                    Bitmap copy = bitmap.Clone(new Rectangle(x, y, w, h), imageData.PixelFormat);
                    Image image = (Image)bitmap;
                    frame.setImage(image);

                    instance.frameImages.Add(frame.getName(), frame);
                }

            }

            br.Close();

        }

    }
}
