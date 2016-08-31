using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace MatrixLibmanWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Image[] YesArr = new Image[] {flageYes11, minFlag2,minFlag3,MulFlage1, MulFlage2,MulFlage3,
                                          transFlage1,TransFlage2,TransFlage3, sumFlage1,sumFlage2,sumFlage3,
                                          ortogFlage1,ortogFlage2, ortogFlage3, chast1Flage1, chast1Flage2,chast1Flage3,
                                          chast2Flage1,chast2Flage2,chast2Flage3, mulNumFlage1,mulNumFlage2,mulNumFlage3,
                                          stepFlage1,stepFlage2,stepFlage3,rangFlage1,rangFlage2,rangFlage3,opredFlage1,
                                          opredFlage2,opredFlage3,obratFlage1,obratFlage2,obratFlage3};

            var UserName = new TextBlock[] { UserName1, UserName2, UserName3, UserName4, UserName5, UserName6, UserName7, UserName8,
            UserName9,UserName10,UserName11,UserName12,UserName13,UserName14,UserName15,UserName16,UserName17,UserName18,UserName19,UserName20,
            UserName21,UserName22,UserName23,UserName24,UserName25,UserName26,UserName27,UserName28,UserName29,UserName30,UserName31,UserName32,UserName33,UserName34,UserName35,UserName36};
            
            _yesArr = YesArr;
            USERNAME = UserName;
        }

        static Uri uri = new Uri("pack://application:,,,/MatrixLibmanWPF;component/Resources/Yes1.png");
        static BitmapImage yesImage = new BitmapImage(uri);
        static Image[] _yesArr = new Image[36];
        static TextBlock[] USERNAME = new TextBlock[36];

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 1;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Login1.Clear();
            Password1.Clear();
            Error1.Opacity = 0;
        }

        public void CloseExit()
        {
            var arr = File.ReadAllLines("dataBase.txt");
            for (int i = 0; i < arr.Length; i++)
            {
                string[] inform = arr[i].Split('|');
                if (inform[0] == UserName.Text)
                {
                    StringBuilder str = new StringBuilder();
                    for (int j = 0; j < _yesArr.Length; j++)
                    {
                        str.Append(  _yesArr[j].Source == yesImage ? "1" : "0");
                    }
                    inform[2] = str.ToString();

                    arr[i] = String.Format("{0}|{1}|{2}",inform[0], inform[1], inform[2]);

                    File.WriteAllLines("dataBase.txt", arr);

                    break;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Login1.Text) || string.IsNullOrEmpty(Password1.Password))
            {
                Error1.Opacity = 100; return;
            }

            var arr = File.ReadAllLines("dataBase.txt");
            bool flage = true;
            for (int i = 0; i < arr.Length; i++)
            {
                string[] inform = arr[i].Split('|');
                if (inform[0] == Login1.Text && inform[1] == Password1.Password)
                {
                    InitWorkDate(inform);
                    tabControl.SelectedIndex = 2;
                    break;
                }
            }
            if (flage)
            {
                Error1.Opacity = 100;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Error2.Content = String.Empty;
            Error2_Copy2.Content = String.Empty;

            if (string.IsNullOrEmpty(Login2.Text) || string.IsNullOrEmpty(Password21.Password))
            {
                Error2.Content = "                                 Заполните данные"; return;
            }

            if (Password21.Password != Password22.Password)
            {
                Error2.Content = "                               Пароли не совпадают"; return;
            }

            for (int i = 0; i < Password21.Password.Length; i++)
            {
                char ch = Password21.Password[i];
                if (!char.IsDigit(ch) && !char.IsLetter(ch) && ch != '_' && ch != ' ')
                {
                    Error2.Content = "Из спец. символов допустимо только нижнее подчеркивание"; return;
                }
            }
            for (int i = 0; i < Login2.Text.Length; i++)
            {
                char ch = Login2.Text[i];
                if (!char.IsDigit(ch) && !char.IsLetter(ch) && ch != '_' && ch != ' ')
                {
                    Error2_Copy2.Content = "Из спец. символов допустимо только нижнее подчеркивание"; return;
                }
            }

            var arr = File.ReadAllLines("dataBase.txt");
            for (int i = 0; i < arr.Length; i++)
            {
                string[] inform = arr[i].Split('|');
                if (inform[0] == Login2.Text)
                {
                    Error2.Content = " Пользователь уже существует"; return;
                }
            }
            string content = Login2.Text + "|" + Password21.Password + "|" + "000000000000000000000000000000000000";
            using (StreamWriter sw = File.AppendText("dataBase.txt")) 
            {
                sw.WriteLine(content);
            }
            InitWorkDate(content.Split('|'));
            tabControl.SelectedIndex = 2;
        }

        private void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {
            Login2.Clear();
            Password21.Clear();
            Password22.Clear();
            Error2.Content = "";
            Error2_Copy2.Content = "";

        }

        private void Output_Click(object sender, RoutedEventArgs e)
        {
            CloseExit();
            tabControl.SelectedIndex = 0;
        }
        private void ClearForm()
        {
            var foo = tabControl.Items.OfType<TabItem>();
            foreach(var item in foo)
            {
                var child = (item.Content as Grid).Children;
                foreach (var iter in child.OfType<TextBox>())
                {
                    iter.Text = String.Empty;
                    iter.BorderBrush = Brushes.Black;
                }
                foreach (var iter in child.OfType<Label>())
                {
                    if (iter.Content.ToString() == "Исправьте ошибки") iter.Visibility = Visibility.Hidden;
                }
                foreach (var iter in child.OfType<CheckBox>())
                {
                    iter.IsChecked = false;
                }
                foreach (var iter in child.OfType<RadioButton>())
                {
                    iter.IsChecked = false;
                }
            }
        }

        public void InitWorkDate(string[] input)
        {
            string _userName = input[0];
            string matrix = input[2];

            ClearForm();

            //Uri uri = new Uri("pack://application:,,,/MatrixLibmanWPF;component/Resources/Yes1.png");
            //var yesImage = new BitmapImage(uri);
            Uri uriNo = new Uri("pack://application:,,,/MatrixLibmanWPF;component/Resources/No.png");
            var noImage = new BitmapImage(uriNo);


            UserName.Text = _userName;
            Image[] butArr = new Image[12] { but4, but6, but10, but3, but12, but1, but2, but5, but7, but8, but9, but11 };
            for (int i = 0; i < matrix.Length; i += 3)
            {
                var buf = matrix.Substring(i, 3);
                if (buf == "111")
                {
                    butArr[i / 3].Source = yesImage;
                }
                else
                {
                    butArr[i / 3].Source = noImage;
                }
            }

            for (int i = 0; i < _yesArr.Length; i++)
            {
                if (matrix[i] == '1')
                {
                    _yesArr[i].Source = yesImage;
                }
                else
                {
                    _yesArr[i].Source = null;
                }

            }
            for (int i = 0; i < USERNAME.Length; i++)
            {
                USERNAME[i].Text = _userName;
            }

        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            //Window1 win = new Window1();
            //win.ShowDialog();
            tabControl.SelectedIndex = 2;
        }

        private void MinusBut_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 3;
        }

        private void butBack_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 0;
        }

        private void butMul_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 6;
        }

        private void TransBut_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 9;
        }

        private void SumBut_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 12;
        }

        private void OrtogonalBut_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 15;
        }

        private void MinusButTest1_Click(object sender, RoutedEventArgs e)
        {
            if (flageYes11.Source == yesImage)
            {
                tabControl.SelectedIndex = 4;
                return;
            }
            Er11.Visibility = Visibility.Hidden;
            TextBox[] arr = new TextBox[] { Minus1_11, Minus1_12, Minus1_21, Minus1_22 };
            NoSelect(arr);
            string[] ans = new string[] {"1","4","0","4" };
            bool flage = true;
            for (int i = 0; i < ans.Length; i++)
            {
                if (arr[i].Text != ans[i]) 
                {
                    YesSelect(arr[i]);
                    flage = false;
                    Er11.Visibility = Visibility.Visible;
                }
                
            }
            if (flage)
            {
                tabControl.SelectedIndex = 4;
                flageYes11.Source = yesImage;
            }
        }

        public void NoSelect(TextBox[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].BorderBrush = Brushes.Black;

            }
        }

        public void YesSelect(TextBox item)
        {
            item.BorderBrush = Brushes.Red;
        }

        private void Chast1But_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 18;
        }

        private void Chast11_Click(object sender, RoutedEventArgs e)
        {
            if (chast1Flage1.Source == yesImage)
            {
                tabControl.SelectedIndex = 19;
                return;
            }

            if (chast1True1.IsChecked != true)
            {
                chast1Er1.Visibility = Visibility.Visible;
                return;
            }
            tabControl.SelectedIndex = 19;
            chast1Flage1.Source = yesImage;
        }

        private void Chast12_Click(object sender, RoutedEventArgs e)
        {
            if (chast1Flage2.Source == yesImage)
            {
                tabControl.SelectedIndex = 20;
                return;
            }

            if (!(check2.IsChecked == true && check3.IsChecked == true && check1.IsChecked != true))
            {
                chast1Er2.Visibility = Visibility.Visible;
                return;
            }
            tabControl.SelectedIndex = 20;
            chast1Flage2.Source = yesImage;
        }

        private void Chast13_Click(object sender, RoutedEventArgs e)
        {
            if (chast1Flage3.Source == yesImage)
            {
                tabControl.SelectedIndex = 2;
                return;
            }

            if (!(check12.IsChecked != true && check13.IsChecked != true && check11.IsChecked == true))
            {
                chast1Er3.Visibility = Visibility.Visible;
                return;
            }
            tabControl.SelectedIndex = 2;
            chast1Flage3.Source = yesImage;
            but1.Source = yesImage;
        }

        private void Chast2But_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 21;
        }

        private void Chast21_Click(object sender, RoutedEventArgs e)
        {
            if (chast2Flage1.Source == yesImage)
            {
                tabControl.SelectedIndex = 22;
                return;
            }

            if (!(set11.IsChecked == true && set12.IsChecked == true && set14.IsChecked == true && set13.IsChecked != true))
            {
                chast2Er1.Visibility = Visibility.Visible;
                return;
            }
            tabControl.SelectedIndex = 22;
            chast2Flage1.Source = yesImage;
        }

        private void Chast22_Click(object sender, RoutedEventArgs e)
        {
            if (chast2Flage2.Source == yesImage)
            {
                tabControl.SelectedIndex = 23;
                return;
            }

            if (!(set23.IsChecked == true && set24.IsChecked == true && set22.IsChecked == true && set21.IsChecked != true))
            {
                chast2Er2.Visibility = Visibility.Visible;
                return;
            }
            tabControl.SelectedIndex = 23;
            chast2Flage2.Source = yesImage;
        }

        private void Chast23_Click(object sender, RoutedEventArgs e)
        {
            if (chast2Flage3.Source == yesImage)
            {
                tabControl.SelectedIndex = 2;
                return;
            }

            if (!(set31.IsChecked != true && set32.IsChecked == true && set34.IsChecked == true && set33.IsChecked != true))
            {
                chast2Er3.Visibility = Visibility.Visible;
                return;
            }
            tabControl.SelectedIndex = 2;
            chast2Flage3.Source = yesImage;
            but2.Source = yesImage;
        }

        private void MulBut_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 24;
        }

        private void MulBut1_Click(object sender, RoutedEventArgs e)
        {
            var arr = new TextBox[] { mulNum1_11, mulNum1_12, mulNum1_21, mulNum1_22 };
            var ans = new string[] { "15", "25", "10", "30" };
            Checking(mulNumFlage1, 25, mulnumEr1, arr, ans);
        }

        private void MulBut2_Click(object sender, RoutedEventArgs e)
        {
            var arr = new TextBox[] { mulNum2_11, mulNum2_12, mulNum2_21, mulNum2_22, mulNum2_31, mulNum2_32};
            var ans = new string[] { "6", "8", "8", "4", "0","10" };
            Checking(mulNumFlage2, 26, mulnumEr2, arr, ans);
        }

        private void StepenBut_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 27;
        }

        private void Stepen1_Click(object sender, RoutedEventArgs e)
        {
            var arr = new TextBox[] { step1_11, step1_12, step1_21, step1_22 };
            var ans = new string[] { "-1", "2", "-4", "7" };
            Checking(stepFlage1, 28, stepEr1, arr, ans);
        }

        private void Stepen2_Click(object sender, RoutedEventArgs e)
        {
            var arr = new TextBox[] { step2_11, step2_12, step2_13, step2_21, step2_22, step2_23, step2_31, step2_32, step2_33 };
            var ans = new string[] { "3", "6", "2", "0","1","0","4","6","3" };
            Checking(stepFlage2, 29, stepEr2, arr, ans);
        }

        private void Stepen3_Click(object sender, RoutedEventArgs e)
        {
            var arr = new TextBox[] { step3_11, step3_12, step3_13, step3_21, step3_22, step3_23, step3_31, step3_32, step3_33 };
            var ans = new string[] { "-7", "-36", "32", "16", "44", "-25", "-2", "-11", "19" };
            if (Checking(stepFlage3, 2, stepEr3, arr, ans)) but7.Source = yesImage;
        }

        private void RangBut_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 30;
        }

        private void Rang1_Click(object sender, RoutedEventArgs e)
        {
            if (rangFlage1.Source == yesImage)
            {
                tabControl.SelectedIndex = 31;
                return;
            }
            if (rangBox.Text!="1")
            {
                rangEr1.Visibility = Visibility.Visible;
                return;
            }
            tabControl.SelectedIndex = 31;
            rangFlage1.Source = yesImage;
        }

        private void Rang2_Click(object sender, RoutedEventArgs e)
        {
            if (rangFlage2.Source == yesImage)
            {
                tabControl.SelectedIndex = 32;
                return;
            }
            if (rangBox2.Text != "2")
            {
                rangEr2.Visibility = Visibility.Visible;
                return;
            }
            tabControl.SelectedIndex = 32;
            rangFlage2.Source = yesImage;
        }

        private void Rang3_Click(object sender, RoutedEventArgs e)
        {
            if (rangFlage3.Source == yesImage)
            {
                tabControl.SelectedIndex = 2;
                return;
            }
            if (rangBox3.Text != "2")
            {
                rangEr3.Visibility = Visibility.Visible;
                return;
            }
            tabControl.SelectedIndex = 2;
            rangFlage3.Source = yesImage;
            but8.Source = yesImage;
        }

        private void OpredBut_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 33;
        }

        private void Opred1_Click(object sender, RoutedEventArgs e)
        {
            if (opredFlage1.Source == yesImage)
            {
                tabControl.SelectedIndex = 34;
                return;
            }
            if (opredBox1.Text != "-29")
            {
                opredEr1.Visibility = Visibility.Visible;
                return;
            }
            tabControl.SelectedIndex = 34;
            opredFlage1.Source = yesImage;
        }

        private void Opred2_Click(object sender, RoutedEventArgs e)
        {
            if (opredFlage2.Source == yesImage)
            {
                tabControl.SelectedIndex = 35;
                return;
            }
            if (opredBox2.Text != "-75")
            {
                opredEr2.Visibility = Visibility.Visible;
                return;
            }
            tabControl.SelectedIndex = 35;
            opredFlage2.Source = yesImage;
        }

        private void Opred3_Click(object sender, RoutedEventArgs e)
        {
            if (opredFlage3.Source == yesImage)
            {
                tabControl.SelectedIndex = 2;
                return;
            }
            if (opredBox3.Text != "-355")
            {
                opredEr3.Visibility = Visibility.Visible;
                return;
            }
            tabControl.SelectedIndex = 2;
            opredFlage3.Source = yesImage;
            but9.Source = yesImage;
        }

        private void Minus2_Click(object sender, RoutedEventArgs e)
        {
            var arr = new TextBox[]  { Minus2_11, Minus2_12, Minus2_21, Minus2_22, Minus2_31, Minus2_32 };
            var ans = new string[] { "3", "3", "4", "-2", "3", "-2"};
            Checking(minFlag2, 5, Er12, arr, ans);
        }

        private void Minus3_Click(object sender, RoutedEventArgs e)
        {
            var arr = new TextBox[]{ Minus3_11, Minus3_12, Minus3_13, Minus3_21, Minus3_22, Minus3_23, Minus3_31, Minus3_32, Minus3_33 };
            var ans = new string[] { "-3", "2", "-4", "-2", "0", "-18","0","12","-37" };
            if (Checking(minFlag3, 2, Er13, arr, ans)) but4.Source = yesImage; ;
        }

        private void Mul1_Click(object sender, RoutedEventArgs e)
        {
            var arr = new TextBox[] { mul1_11, mul1_12, mul1_21, mul1_22};
            var ans = new string[] {"4","68","-14","22"};
            Checking(MulFlage1, 7, mulEr1, arr, ans);
        }

        public bool Checking(Image Flage, int PageNumber, Label ErrorName, TextBox[] arr, string[] ans)
        {
            if (Flage.Source == yesImage)
            {
                tabControl.SelectedIndex = PageNumber;
                return false;
            }
            ErrorName.Visibility = Visibility.Hidden;
            NoSelect(arr);
            bool flage = true;
            for (int i = 0; i < ans.Length; i++)
            {
                if (arr[i].Text != ans[i])
                {
                    YesSelect(arr[i]);
                    flage = false;
                    ErrorName.Visibility = Visibility.Visible;
                }

            }
            if (flage)
            {
                tabControl.SelectedIndex = PageNumber;
                Flage.Source = yesImage;
                return true;
            }
            return false;
        }

        private void Mul2_Click(object sender, RoutedEventArgs e)
        {
            var arr = new TextBox[] { mul2_11, mul2_12, mul2_21, mul2_22, mul2_31, mul2_32};
            var ans = new string[] { "-6", "88", "-30", "40", "-16", "68" };
            Checking(MulFlage2, 8, mulEr2, arr, ans);
        }

        private void Mul3_Click(object sender, RoutedEventArgs e)
        {
            var arr = new TextBox[] { mul3_11, mul3_12, mul3_13, mul3_21, mul3_22, mul3_23, mul3_31, mul3_32, mul3_33 };
            var ans = new string[] { "48", "21", "27", "43", "18", "20","47","24","12" };
            if (Checking(MulFlage3, 2, mulEr3, arr, ans)) but6.Source = yesImage;
        }

        private void Trans1_Click(object sender, RoutedEventArgs e)
        {
            if (transFlage1.Source == yesImage)
            {
                tabControl.SelectedIndex = 10;
                return;
            }

            if (transTrue1.IsChecked != true) 
            {
                transEr1.Visibility = Visibility.Visible;
                return;
            }
            tabControl.SelectedIndex = 10;
            transFlage1.Source = yesImage;
        }

        private void Trans2_Click(object sender, RoutedEventArgs e)
        {
            if (TransFlage2.Source == yesImage)
            {
                tabControl.SelectedIndex = 11;
                return;
            }

            if (transTrue2.IsChecked != true)
            {
                transEr2.Visibility = Visibility.Visible;
                return;
            }
            tabControl.SelectedIndex = 11;
            TransFlage2.Source = yesImage;
        }

        private void Trans3_Click(object sender, RoutedEventArgs e)
        {
            if (TransFlage3.Source == yesImage)
            {
                tabControl.SelectedIndex = 2;
                return;
            }

            if (transTrue3.IsChecked != true)
            {
                transEr3.Visibility = Visibility.Visible;
                return;
            }
            tabControl.SelectedIndex = 2;
            TransFlage3.Source = yesImage;
            but10.Source = yesImage;
        }

        private void Sum1_Click(object sender, RoutedEventArgs e)
        {
            var arr = new TextBox[] { sum1_11, sum1_12, sum1_21, sum1_22 };
            var ans = new string[] { "8", "10", "-4", "7"};
            Checking(sumFlage1, 13, sumEr1, arr, ans);
        }

        private void Sum2_Click(object sender, RoutedEventArgs e)
        {
            var arr = new TextBox[] { sum2_11, sum2_12, sum2_21, sum2_22, sum2_31, sum2_32 };
            var ans = new string[] { "1", "8", "3", "7","5", "10" };
            Checking(sumFlage2, 14, sumEr2, arr, ans);
        }

        private void Sum3_Click(object sender, RoutedEventArgs e)
        {
            var arr = new TextBox[] { sum3_11, sum3_12, sum3_13, sum3_21, sum3_22, sum3_23, sum3_31, sum3_32, sum3_33 };
            var ans = new string[] { "5", "18", "-6", "10", "4", "6" , "-4","-14","45"};
            if (Checking(sumFlage3, 2, sumEr3, arr, ans)) but3.Source = yesImage; ;
        }

        private void Ortog1_Click(object sender, RoutedEventArgs e)
        {
            if (ortogFlage1.Source == yesImage)
            {
                tabControl.SelectedIndex = 16;
                return;
            }

            if (ortogTrue1.IsChecked != true)
            {
                ortogEr1.Visibility = Visibility.Visible;
                return;
            }
            tabControl.SelectedIndex = 16;
            ortogFlage1.Source = yesImage;
        }

        private void Ortog2_Click(object sender, RoutedEventArgs e)
        {
            if (ortogFlage2.Source == yesImage)
            {
                tabControl.SelectedIndex = 17;
                return;
            }

            if (ortogTrue2.IsChecked != true)
            {
                ortogEr2.Visibility = Visibility.Visible;
                return;
            }
            tabControl.SelectedIndex = 17;
            ortogFlage2.Source = yesImage;
        }

        private void Ortog3_Click(object sender, RoutedEventArgs e)
        {
            if (ortogFlage3.Source == yesImage)
            {
                tabControl.SelectedIndex = 2;
                return;
            }

            if (ortogTrue3.IsChecked != true)
            {
                ortogEr3.Visibility = Visibility.Visible;
                return;
            }
            tabControl.SelectedIndex = 2;
            ortogFlage3.Source = yesImage;
            but12.Source = yesImage;
        }

        private void MulBut3_Click_1(object sender, RoutedEventArgs e)
        {
            var arr = new TextBox[] { mulNum3_11, mulNum3_12, mulNum3_13, mulNum3_21, mulNum3_22, mulNum3_23, mulNum3_31, mulNum3_32, mulNum3_33 };
            var ans = new string[] { "-12", "-24", "3", "-18", "-6", "-36", "6", "21", "-15" };
            if (Checking(mulNumFlage3, 2, mulnumEr3, arr, ans)) but5.Source = yesImage; ;
        }

        private void ObratBut_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 36;
        }

        private void Obrat1_Click(object sender, RoutedEventArgs e)
        {
            var arr = new TextBox[] { obrat1_11, obrat1_12, obrat1_21, obrat1_22 };
            var ans = new string[] { "7", "-5", "-4", "3" };
            Checking(obratFlage1, 37, obratEr1, arr, ans);
        }

        private void Obrat2_Click(object sender, RoutedEventArgs e)
        {
            var arr = new TextBox[] { obrat2_11, obrat2_12, obrat2_13, obrat2_21, obrat2_22, obrat2_23, obrat2_31, obrat2_32, obrat2_33 };
            var ans = new string[] { "2", "-1", "0", "-1", "2", "-1","0","-1","1" };
            Checking(obratFlage2, 38, obratEr2, arr, ans);
        }

        private void Obrat3_Click(object sender, RoutedEventArgs e)
        {
            var arr = new TextBox[] { obrat3_11, obrat3_12, obrat3_13, obrat3_21, obrat3_22, obrat3_23, obrat3_31, obrat3_32, obrat3_33 };
            var ans = new string[] { "1", "-38", "27", "-1","41","-29","1","-34","24" };
            if(Checking(obratFlage3, 2, obratEr3, arr, ans)) but11.Source = yesImage;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CloseExit();
        }
    }
}
