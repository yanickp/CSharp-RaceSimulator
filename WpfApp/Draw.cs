using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using Controller;
using Model;


namespace WpfApp
{
    class Draw {

        #region graphics
        private const string _StartGrid = ".\\images\\StartGrid.png";
        private const string _Finish = ".\\images\\Finish.png";

        private const string _StraightVertical = ".\\images\\StraightVertical.png";
        private const string _StraightHorizontal = ".\\images\\StraightHorizontal.png";

        private const string _Turn0 = ".\\images\\Turn0.png";
        private const string _Turn1 = ".\\images\\Turn1.png";
        private const string _Turn2 = ".\\images\\Turn2.png";
        private const string _Turn3 = ".\\images\\Turn3.png";

        private const string _CarRed = ".\\images\\CarRed.png";
        private const string _CarBlue = ".\\images\\CarBlue.png";
        private const string _CarYellow = ".\\images\\CarYellow.png";
        private const string _CarGreen = ".\\images\\CarGreen.png";
        private const string _CarGrey = ".\\images\\CarGrey.png";
        private const string _CarPurple = ".\\images\\CarPurple.png";
        
        
        private const string _Broken = ".\\images\\Broken.png";


        #endregion graphics

        private static int Width, Height, CurrentDirection, CurrentX, CurrentY, MaxX, MaxY;
    private static readonly int SectionSize = 64;

    public static BitmapSource DrawTrack(Model.Track track)
    {
        createWidthAndHeight(track);

        CurrentX = 0;
        CurrentY = 0;
        CurrentDirection = 0;

        Bitmap bitmap = imageCache.GetEmptyBitmap(Width * SectionSize, Height * SectionSize);
        Graphics graphics = Graphics.FromImage(bitmap);

        if (Data.CurrentRace != null)
        {
            IParticipant left;
            IParticipant right;
            

            foreach (Section section in track.Sections)
            {
                left = Data.CurrentRace.GetSectionData(section).Left;
                right = Data.CurrentRace.GetSectionData(section).Right;

                switch (section.SectionType)
                {
                    case Section.SectionTypes.StartGrid:
                        graphics.DrawImage(imageCache.Get(_StartGrid), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                        DrawPlayer(graphics, left, right);
                        Move();
                        break;

                    case Section.SectionTypes.Finish:
                        graphics.DrawImage(imageCache.Get(_Finish), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                        DrawPlayer(graphics, left, right);
                        Move();
                        break;

                    case Section.SectionTypes.Straight:
                        if (CurrentDirection == 0)
                            graphics.DrawImage(imageCache.Get(_StraightVertical), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                        else if (CurrentDirection == 1)
                            graphics.DrawImage(imageCache.Get(_StraightHorizontal), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                        else if (CurrentDirection == 2)
                            graphics.DrawImage(imageCache.Get(_StraightVertical), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                        else if (CurrentDirection == 3)
                            graphics.DrawImage(imageCache.Get(_StraightHorizontal), new Point(CurrentX * SectionSize, CurrentY * SectionSize));

                        DrawPlayer(graphics, left, right);
                        Move();
                        break;

                    case Section.SectionTypes.RightCorner:
                        if (CurrentDirection == 0)
                            graphics.DrawImage(imageCache.Get(_Turn3), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                        else if (CurrentDirection == 1)
                            graphics.DrawImage(imageCache.Get(_Turn0), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                        else if (CurrentDirection == 2)
                            graphics.DrawImage(imageCache.Get(_Turn1), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                        else if (CurrentDirection == 3)
                            graphics.DrawImage(imageCache.Get(_Turn2), new Point(CurrentX * SectionSize, CurrentY * SectionSize));

                        DrawPlayer(graphics, left, right);
                        Right();
                        Move();
                        break;

                    case Section.SectionTypes.LeftCorner:
                        if (CurrentDirection == 0)
                            graphics.DrawImage(imageCache.Get(_Turn0), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                        else if (CurrentDirection == 1)
                            graphics.DrawImage(imageCache.Get(_Turn1), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                        else if (CurrentDirection == 2)
                            graphics.DrawImage(imageCache.Get(_Turn2), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                        else if (CurrentDirection == 3)
                            graphics.DrawImage(imageCache.Get(_Turn3), new Point(CurrentX * SectionSize, CurrentY * SectionSize));

                        DrawPlayer(graphics, left, right);
                        Left();
                        Move();
                        break;
                }
            }
        }

        return imageCache.CreateBitmapSourceFromGdiBitmap(bitmap);
    }

    private static void DrawPlayer(Graphics graphics, IParticipant left, IParticipant right)
    {
        if (left != null)
        {
            switch (left.TeamColor)
            {
                case IParticipant.TeamColors.Red:
                    graphics.DrawImage(new Bitmap(imageCache.Get(_CarRed), 25, 25), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                    break;
                case IParticipant.TeamColors.Blue:
                    graphics.DrawImage(new Bitmap(imageCache.Get(_CarBlue), 25, 25), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                    break;
                case IParticipant.TeamColors.Yellow:
                    graphics.DrawImage(new Bitmap(imageCache.Get(_CarYellow), 25, 25), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                    break;
                case IParticipant.TeamColors.Grey:
                    graphics.DrawImage(new Bitmap(imageCache.Get(_CarGrey), 25, 25), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                    break;
                case IParticipant.TeamColors.Green:
                    graphics.DrawImage(new Bitmap(imageCache.Get(_CarGreen), 25, 25), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                    break;
                case IParticipant.TeamColors.purple:
                    graphics.DrawImage(new Bitmap(imageCache.Get(_CarPurple), 25, 25), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
                    break;
            }
        
            if (left.Equipment.IsBroken)
                graphics.DrawImage(new Bitmap(imageCache.Get(_Broken), 25, 25), new Point(CurrentX * SectionSize, CurrentY * SectionSize));
        }
        
        if (right != null)
        {
            switch (right.TeamColor)
            {
                case IParticipant.TeamColors.Blue:
                    graphics.DrawImage(new Bitmap(imageCache.Get(_CarBlue), 25, 25), new Point(CurrentX * SectionSize + 32, CurrentY * SectionSize + 32));
                    break;
                case IParticipant.TeamColors.Red:
                    graphics.DrawImage(new Bitmap(imageCache.Get(_CarRed), 25, 25), new Point(CurrentX * SectionSize + 32, CurrentY * SectionSize + 32));
                    break;
                case IParticipant.TeamColors.Yellow:
                    graphics.DrawImage(new Bitmap(imageCache.Get(_CarYellow), 25, 25), new Point(CurrentX * SectionSize + 32, CurrentY * SectionSize + 32));
                    break;
                 case IParticipant.TeamColors.Grey:
                    graphics.DrawImage(new Bitmap(imageCache.Get(_CarGrey), 25, 25), new Point(CurrentX * SectionSize + 32, CurrentY * SectionSize + 32));
                    break;
                 case IParticipant.TeamColors.Green:
                    graphics.DrawImage(new Bitmap(imageCache.Get(_CarGreen), 25, 25), new Point(CurrentX * SectionSize + 32, CurrentY * SectionSize + 32));
                    break;
                 case IParticipant.TeamColors.purple:
                    graphics.DrawImage(new Bitmap(imageCache.Get(_CarPurple), 25, 25), new Point(CurrentX * SectionSize + 32, CurrentY * SectionSize + 32));
                    break;
            }
        
            if (right.Equipment.IsBroken)
                graphics.DrawImage(new Bitmap(imageCache.Get(_Broken), 25, 25), new Point(CurrentX * SectionSize + 32, CurrentY * SectionSize + 32));
        }
    }

    private static void createWidthAndHeight(Model.Track track)
    {
        CurrentX = 0;
        CurrentY = 0;
        CurrentDirection = 0;

        foreach (Section section in track.Sections)
        {
            switch (section.SectionType)
            {
                case Section.SectionTypes.StartGrid:
                case Section.SectionTypes.Finish:
                case Section.SectionTypes.Straight:
                    Move();
                    break;

                case Section.SectionTypes.RightCorner:
                    Right();
                    Move();
                    break;

                case Section.SectionTypes.LeftCorner:
                    Left();
                    Move();
                    break;
            }
        }

        Width = MaxX + 1;
        Height = MaxY + 2;
    }

    private static void Move()
    {
        if (CurrentDirection == 0)
            CurrentY--;
        else if (CurrentDirection == 1)
            CurrentX++;
        else if (CurrentDirection == 2)
            CurrentY++;
        else if (CurrentDirection == 3)
            CurrentX--;

        if (CurrentX > MaxX)
            MaxX = CurrentX;

        if (CurrentY > MaxY)
            MaxY = CurrentY;
    }

    public static void Right()
    {
        if (CurrentDirection == 0)
            CurrentDirection = 1;
        else if (CurrentDirection == 1)
            CurrentDirection = 2;
        else if (CurrentDirection == 2)
            CurrentDirection = 3;
        else if (CurrentDirection == 3)
            CurrentDirection = 0;
    }

    public static void Left()
    {
        if (CurrentDirection == 0)
            CurrentDirection = 3;
        else if (CurrentDirection == 1)
            CurrentDirection = 0;
        else if (CurrentDirection == 2)
            CurrentDirection = 1;
        else if (CurrentDirection == 3)
            CurrentDirection = 2;
    }
}
}
