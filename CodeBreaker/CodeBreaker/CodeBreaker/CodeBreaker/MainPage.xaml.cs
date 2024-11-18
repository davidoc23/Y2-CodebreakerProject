using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Xamarin.Forms;

namespace CodeBreaker
{
    public partial class MainPage : ContentPage
    {
        //Answer for the array to test
        readonly Color[] solution = { Color.Blue, Color.Red, Color.Yellow, Color.Green };

        //Declare Variables
        private int _counterGuess = 0;
        private int _currentplay = 0;
        private string filename = "GameState.txt";
        private GameState _gameState;
        private Random _random;
        private Button currentGuessPeg;

        //Two arrays for the buttons
        List<Button>[] ArraySolution = new List<Button>[10];
        List<Button>[] mainArray = new List<Button>[10];

        //Array for what colors the program can use in the random generator.
        Color[] ColorTransparent = { Color.Red, Color.Green, Color.Yellow, Color.Blue };

        public class GameState
        {
            public int[] playerRows;

            public GameState()
            {
                playerRows = new int[40];
            }
        }

        public MainPage()
        {
            InitializeComponent();
            _random = new Random();
            int a, k;

            //Array to allow the user to choose a colour
            mainArray[0] = new List<Button> { BtnGuess1, BtnGuess2, BtnGuess3, BtnGuess4 };
            mainArray[1] = new List<Button> { BtnGuess5, BtnGuess6, BtnGuess7, BtnGuess8 };
            mainArray[2] = new List<Button> { BtnGuess9, BtnGuess10, BtnGuess11, BtnGuess12 };
            mainArray[3] = new List<Button> { BtnGuess13, BtnGuess14, BtnGuess15, BtnGuess16 };
            mainArray[4] = new List<Button> { BtnGuess17, BtnGuess18, BtnGuess19, BtnGuess20 };
            mainArray[5] = new List<Button> { BtnGuess21, BtnGuess22, BtnGuess23, BtnGuess24 };
            mainArray[6] = new List<Button> { BtnGuess25, BtnGuess26, BtnGuess27, BtnGuess28 };
            mainArray[7] = new List<Button> { BtnGuess29, BtnGuess30, BtnGuess31, BtnGuess32 };
            mainArray[8] = new List<Button> { BtnGuess33, BtnGuess34, BtnGuess35, BtnGuess36 };
            mainArray[9] = new List<Button> { BtnGuess37, BtnGuess38, BtnGuess39, BtnGuess40 };

            //Random generated colors for the buttons before the user selects which are right
            for (k = 0; k < 10; k++)
            {
                foreach (Button i in mainArray[k])
                {
                    i.BackgroundColor = ColorTransparent[_random.Next(0, 4)];
                    i.IsVisible = false;
                }
            }

            //Array to show whether each button input from the user was correct
            ArraySolution[0] = new List<Button> { BtnCheck1, BtnCheck2, BtnCheck3, BtnCheck4 };
            ArraySolution[1] = new List<Button> { BtnCheck5, BtnCheck6, BtnCheck7, BtnCheck8 };
            ArraySolution[2] = new List<Button> { BtnCheck9, BtnCheck10, BtnCheck11, BtnCheck12 };
            ArraySolution[3] = new List<Button> { BtnCheck13, BtnCheck14, BtnCheck15, BtnCheck16 };
            ArraySolution[4] = new List<Button> { BtnCheck17, BtnCheck18, BtnCheck19, BtnCheck20 };
            ArraySolution[5] = new List<Button> { BtnCheck21, BtnCheck22, BtnCheck23, BtnCheck24 };
            ArraySolution[6] = new List<Button> { BtnCheck25, BtnCheck26, BtnCheck27, BtnCheck28 };
            ArraySolution[7] = new List<Button> { BtnCheck29, BtnCheck30, BtnCheck31, BtnCheck32 };
            ArraySolution[8] = new List<Button> { BtnCheck33, BtnCheck34, BtnCheck35, BtnCheck36 };
            ArraySolution[9] = new List<Button> { BtnCheck37, BtnCheck38, BtnCheck39, BtnCheck40 };

            for (a = 0; a < 10; a++)
            {
                foreach (Button i in ArraySolution[a])
                {
                    i.IsVisible = false;
                }
            }

            foreach (Button i in mainArray[0])
            {
                i.BackgroundColor = ColorTransparent[_random.Next(0, 4)];
                i.IsVisible = true;
            }

            Btn_Check_0.IsVisible = true;

        }

        private GameState ReadJsonFile()
        {
            GameState gs = null;
            string jsonText = "";

            try // read the local directory
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); // This is the location of the saved file - C:\Users\YOURNAME\AppData\Local\Packages *NOTE AppData folder is hidden so you need to view hidden folders in properties
                string fname = Path.Combine(path, filename);
                using (var reader = new StreamReader(fname))
                {
                    jsonText = reader.ReadToEnd();
                }
            }
            catch (Exception ex) // if it fails, read the embedded resource
            {
                string errorMsg = ex.Message;
            }
            if (jsonText != "")
            {
                gs = new GameState();
                gs = JsonConvert.DeserializeObject<GameState>(jsonText);
                return gs;
            }
            else
                return null;
        }

        public void SaveListOfData(GameState gs)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); // This is the location of the saved file - C:\Users\YOURNAME\AppData\Local\Packages *NOTE AppData folder is hidden so you need to view hidden folders in properties
            string fname = Path.Combine(path, filename);
            using (var writer = new StreamWriter(fname, false))
            {
                string jsonText = JsonConvert.SerializeObject(gs);
                writer.WriteLine(jsonText);
            }
        }

        private void BtnReadFile_Clicked(object sender, EventArgs e)
        {
            string fileContent = "";
            _gameState = ReadJsonFile();

            if (_gameState != null)
            {
                fileContent = "Row values: " + _gameState.playerRows[40];
            }
            else
            {
                fileContent = "no file found";
            }


        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (currentGuessPeg != null) currentGuessPeg.BorderColor = Color.Transparent;

            currentGuessPeg = (Button)sender;
            currentGuessPeg.BorderColor = Color.Magenta;
            currentGuessPeg.BorderWidth = 4;
            GridColourPicker.IsVisible = true;
        }

        private void ColorGuessBoxView_Tapped(object sender, EventArgs e)
        {
            BoxView b = (BoxView)sender;

            currentGuessPeg.BackgroundColor = b.Color;
            currentGuessPeg.BorderColor = Color.Transparent;
            GridColourPicker.IsVisible = false;
        }

        private void Save_Game_Clicked(object sender, EventArgs e)
        {
            GameState gs = new GameState();

            gs.playerRows[0] = 134;
            gs.playerRows[1] = 234;
            gs.playerRows[2] = 334;
            gs.playerRows[3] = 434;
            gs.playerRows[4] = 534;
            gs.playerRows[5] = 634;
            gs.playerRows[6] = 734;
            gs.playerRows[7] = 834;
            gs.playerRows[8] = 934;
            gs.playerRows[9] = 1034;
            gs.playerRows[10] = 1134;
            gs.playerRows[11] = 1234;
            gs.playerRows[12] = 1334;
            gs.playerRows[13] = 1434;
            gs.playerRows[14] = 1534;
            gs.playerRows[15] = 1634;
            gs.playerRows[16] = 1734;
            gs.playerRows[17] = 1834;
            gs.playerRows[18] = 1934;
            gs.playerRows[19] = 2034;
            gs.playerRows[20] = 2134;
            gs.playerRows[11] = 2234;
            gs.playerRows[22] = 2334;
            gs.playerRows[23] = 2434;
            gs.playerRows[24] = 2534;
            gs.playerRows[25] = 2634;
            gs.playerRows[26] = 2734;
            gs.playerRows[27] = 2834;
            gs.playerRows[28] = 2934;
            gs.playerRows[29] = 3034;
            gs.playerRows[30] = 3134;
            gs.playerRows[31] = 3234;
            gs.playerRows[32] = 3334;
            gs.playerRows[33] = 3434;
            gs.playerRows[34] = 3534;
            gs.playerRows[35] = 3634;
            gs.playerRows[36] = 3734;
            gs.playerRows[37] = 3834;
            gs.playerRows[38] = 3934;
            gs.playerRows[39] = 4034;

            SaveListOfData(gs);

            //Once the save game button has ben pressed the game exits.
            EndGame();
        }

        private void End_Game_Clicked(object sender, EventArgs e)
        {
            GridColourPicker.IsVisible = false;
            GridTest.IsVisible = false;
            End_Game.IsVisible = false;
            GridColourPicker.IsVisible = false;
            Start_Game.IsVisible = true;
            Save_Game.IsVisible = true;
            Heading_Name.IsVisible = false;
            Label1.IsVisible = true;
            Info.IsVisible = false;
            Exit_Game.IsVisible = true;
            EndGameImage.IsVisible = true;
            Exit_Message.IsVisible = true;
        }

        private void Start_Game_Clicked(object sender, EventArgs e)
        {
            GridColourPicker.IsVisible = true;
            GridTest.IsVisible = true;
            End_Game.IsVisible = true;
            GridColourPicker.IsVisible = false;
            Start_Game.IsVisible = false;
            Save_Game.IsVisible = false;
            Heading_Name.IsVisible = true;
            Label1.IsVisible = false;
            Info.IsVisible = true;
            EndGameImage.IsVisible = false;
        }

        private void Btn_Check_Clicked(object sender, EventArgs e)
        {
            if (_counterGuess < 9)
            {
                AnswerCheck(_currentplay);
                DisableCurrentRow(_currentplay);
                EnableNextRow(_currentplay + 1);
                _counterGuess++;
                _currentplay++;
            }
            else
            {
                //Gameover message to user once all boxes have been checked or once all the buttons have been checked
                GridColourPicker.IsVisible = false;
                GridTest.IsVisible = false;
                End_Game.IsVisible = false;
                GridColourPicker.IsVisible = false;
                Start_Game.IsVisible = true;
                Save_Game.IsVisible = true;
                Heading_Name.IsVisible = false;
                Label1.IsVisible = true;
                Info.IsVisible = false;
                EndGameImage.IsVisible = true;
                Exit_Message.IsVisible = true;
            }
        }

        private void EndGame()
        {
            //Used to end the game once the game has reached the end.
            System.Environment.Exit(0);
        }

        private void EnableNextRow(int k)
        {
            foreach (Button b in mainArray[k])
            {
                b.IsEnabled = true;
                b.IsVisible = true;
            }
            EnableConfirmBtn(k);
        }

        private void EnableConfirmBtn(object k)
        {
            switch (k)
            {
                case 0:
                    Btn_Check_0.IsVisible = true;
                    break;

                case 1:
                    Btn_Check_1.IsVisible = true;
                    break;

                case 2:
                    Btn_Check_2.IsVisible = true;
                    break;

                case 3:
                    Btn_Check_3.IsVisible = true;
                    break;

                case 4:
                    Btn_Check_4.IsVisible = true;
                    break;

                case 5:
                    Btn_Check_5.IsVisible = true;
                    break;

                case 6:
                    Btn_Check_6.IsVisible = true;
                    break;

                case 7:
                    Btn_Check_7.IsVisible = true;
                    break;

                case 8:
                    Btn_Check_8.IsVisible = true;
                    break;

                case 9:
                    Btn_Check_9.IsVisible = true;
                    break;
            }
        }

        private void DisableCurrentRow(int k)
        {
            switch (k)
            {
                case 0:
                    Btn_Check_0.IsVisible = false;
                    break;

                case 1:
                    Btn_Check_1.IsVisible = false;
                    break;

                case 2:
                    Btn_Check_2.IsVisible = false;
                    break;

                case 3:
                    Btn_Check_3.IsVisible = false;
                    break;

                case 4:
                    Btn_Check_4.IsVisible = false;
                    break;

                case 5:
                    Btn_Check_5.IsVisible = false;
                    break;

                case 6:
                    Btn_Check_6.IsVisible = false;
                    break;

                case 7:
                    Btn_Check_7.IsVisible = false;
                    break;

                case 8:
                    Btn_Check_8.IsVisible = false;
                    break;

                case 9:
                    Btn_Check_9.IsVisible = false;
                    break;

            }

        }

        private void AnswerCheck(int k)
        {
            //Declare Variabes
            int x;

            Color[] attempt = { mainArray[k][0].BackgroundColor, mainArray[k][1].BackgroundColor, mainArray[k][2].BackgroundColor, mainArray[k][3].BackgroundColor };

            //Cannot get this to work, when the guess is the same as the correct answer it just displays the 4 red buttons and continues on.
            if (attempt == solution)
            {
                GridColourPicker.IsVisible = false;
                GridTest.IsVisible = false;
                End_Game.IsVisible = false;
                GridColourPicker.IsVisible = false;
                Start_Game.IsVisible = true;
                Save_Game.IsVisible = true;
                Heading_Name.IsVisible = false;
                Label1.IsVisible = true;
                Info.IsVisible = false;
                EndGameImage.IsVisible = true;
                Exit_Message.IsVisible = true;
            }

            for (x = 0; x < 4; x++)
            {
                if (attempt[x] == solution[x])
                {
                    ArraySolution[k][x].IsVisible = true;
                    ArraySolution[k][x].BackgroundColor = Color.Red;
                }
                else if (Array.Exists<Color>(solution, element => element == attempt[x]))
                {
                    ArraySolution[k][x].IsVisible = true;
                    ArraySolution[k][x].BackgroundColor = Color.Gray;
                }
                else
                {
                    ArraySolution[k][x].IsVisible = true;
                    ArraySolution[k][x].BackgroundColor = Color.White;
                }
            }
        }

        private void Exit_Game_Clicked(object sender, EventArgs e)
        {
            EndGame();
        }
    }
}
