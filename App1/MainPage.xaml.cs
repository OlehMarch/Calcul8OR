using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }


        private bool isCalculated = false;
        private bool isHistoryClicked = false;
        private bool isAboutClicked = false;


        #region Common Functions
        private void ButtonClickAction(string digit)
        {
            if (isCalculated)
            {
                TB_Result.Text = String.Empty;
                isCalculated = false;
            }
            if (TB_Result.Text == "0")
                TB_Result.Text = String.Empty;
            TB_Result.Text += digit;

            FontSizeAutoChange();
        }
        private void Calculation()
        {
            string[] components = TB_Result.Text.Split(' ');
            double leftPart = Convert.ToDouble(components[0]);
            double rightPart = Convert.ToDouble(components[2]);
            double result = 0.0;
            switch (components[1])
            {
                case "+":
                    result = leftPart + rightPart;
                    break;
                case "-":
                    result = leftPart - rightPart;
                    break;
                case "*":
                    result = leftPart * rightPart;
                    break;
                case "/":
                    result = leftPart / rightPart;
                    break;
                case "%":
                    result = leftPart % rightPart;
                    break;
                case "^":
                    result = Math.Pow(leftPart, rightPart);
                    break;
                case "√":
                    result = Math.Pow(leftPart, 1 / rightPart);
                    break;
                case "~":
                    try
                    {
                        result = Math.Round(leftPart, (int)rightPart, MidpointRounding.ToEven);
                    }
                    catch
                    {
                        result = leftPart;
                    }
                    break;
            }
            TB_Result.Text += " = " + result.ToString();
            FontSizeAutoChange();

            if (TB_History.Text == "There's no history yet!")
            {
                TB_History.Text = String.Empty;
            }
            TB_History.Text = TB_Result.Text + "\n" + TB_History.Text;

            leftPart = 0.0;
            rightPart = 0.0;
            result = 0.0;
            isCalculated = true;
        }
        private void Operation(string operationSign)
        {
            #region If last symbol is operation sign, need to change previous sign to new one
            if (TB_Result.Text.EndsWith(" + "))
            {
                TB_Result.Text = TB_Result.Text.Replace(" + ", operationSign);
                return;
            }
            else if (TB_Result.Text.EndsWith(" - "))
            {
                TB_Result.Text = TB_Result.Text.Replace(" - ", operationSign);
                return;
            }
            else if (TB_Result.Text.EndsWith(" * "))
            {
                TB_Result.Text = TB_Result.Text.Replace(" * ", operationSign);
                return;
            }
            else if (TB_Result.Text.EndsWith(" / "))
            {
                TB_Result.Text = TB_Result.Text.Replace(" / ", operationSign);
                return;
            }
            else if (TB_Result.Text.EndsWith(" % "))
            {
                TB_Result.Text = TB_Result.Text.Replace(" % ", operationSign);
                return;
            }
            else if (TB_Result.Text.EndsWith(" ^ "))
            {
                TB_Result.Text = TB_Result.Text.Replace(" ^ ", operationSign);
                return;
            }
            else if (TB_Result.Text.EndsWith(" √ "))
            {
                TB_Result.Text = TB_Result.Text.Replace(" √ ", operationSign);
                return;
            }
            else if (TB_Result.Text.EndsWith(" ~ "))
            {
                TB_Result.Text = TB_Result.Text.Replace(" ~ ", operationSign);
                return;
            }
            #endregion

            #region If calculation was performed, need to set the result of previous calculation as first operand
            if (TB_Result.Text.Contains('='))
            {
                isCalculated = false;
                TB_Result.Text = TB_Result.Text.Remove(0, TB_Result.Text.LastIndexOf(" ") + 1) + operationSign;
                FontSizeAutoChange();
                return;
            }
            #endregion

            #region If there is an operation sign, need to complete calculation and actions from previous region
            if (TB_Result.Text.Contains(" + ")
                || TB_Result.Text.Contains(" - ")
                || TB_Result.Text.Contains(" * ")
                || TB_Result.Text.Contains(" / ")
                || TB_Result.Text.Contains(" % ")
                || TB_Result.Text.Contains(" ^ ")
                || TB_Result.Text.Contains(" √ ")
                || TB_Result.Text.Contains(" ~ ")
            )
            {
                Calculation();
                isCalculated = false;
                TB_Result.Text = TB_Result.Text.Remove(0, TB_Result.Text.LastIndexOf(" ") + 1) + operationSign;
                FontSizeAutoChange();
                return;
            }
            #endregion

            if (TB_Result.Text != "")
                TB_Result.Text += operationSign;
            FontSizeAutoChange();
        }
        private void FontSizeAutoChange()
        {
            // auto font size change for result text block
            if (TB_Result.Text.Length >= 25)
                TB_Result.FontSize = 20;
            else
                TB_Result.FontSize = 30;

            // need to find out how to get system architechture
        }
        #endregion

        #region Digit buttons
        private void B_1_Click(object sender, RoutedEventArgs e)
        {
            ButtonClickAction("1");
        }
        private void B_2_Click(object sender, RoutedEventArgs e)
        {
            ButtonClickAction("2");
        }
        private void B_3_Click(object sender, RoutedEventArgs e)
        {
            ButtonClickAction("3");
        }
        private void B_4_Click(object sender, RoutedEventArgs e)
        {
            ButtonClickAction("4");
        }
        private void B_5_Click(object sender, RoutedEventArgs e)
        {
            ButtonClickAction("5");
        }
        private void B_6_Click(object sender, RoutedEventArgs e)
        {
            ButtonClickAction("6");
        }
        private void B_7_Click(object sender, RoutedEventArgs e)
        {
            ButtonClickAction("7");
        }
        private void B_8_Click(object sender, RoutedEventArgs e)
        {
            ButtonClickAction("8");
        }
        private void B_9_Click(object sender, RoutedEventArgs e)
        {
            ButtonClickAction("9");
        }
        private void B_0_Click(object sender, RoutedEventArgs e)
        {
            ButtonClickAction("0");
        }
        #endregion

        #region Basic operations
        private void B_Plus_Click(object sender, RoutedEventArgs e)
        {
            Operation(" + ");
        }
        private void B_Minus_Click(object sender, RoutedEventArgs e)
        {
            Operation(" - ");
        }
        private void B_Multiply_Click(object sender, RoutedEventArgs e)
        {
            Operation(" * ");
        }
        private void B_Divide_Click(object sender, RoutedEventArgs e)
        {
            Operation(" / ");
        }
        private void B_Percent_Click(object sender, RoutedEventArgs e)
        {
            Operation(" % ");
        }
        private void B_Pow_Click(object sender, RoutedEventArgs e)
        {
            Operation(" ^ ");
        }
        private void B_Root_Click(object sender, RoutedEventArgs e)
        {
            Operation(" √ ");
        }
        private void B_Approximation_Click(object sender, RoutedEventArgs e)
        {
            Operation(" ~ ");
        }
    //--------------------------------------------------------------------------\\
        private void B_Sign_Click(object sender, RoutedEventArgs e)
        {
            if (!TB_Result.Text.Contains(' '))
            {
                TB_Result.Text = (.0 - Convert.ToDouble(TB_Result.Text)).ToString();
            }
            else if (!TB_Result.Text.EndsWith(" "))
            {
                TB_Result.Text = TB_Result.Text.Replace(TB_Result.Text.Remove(0, TB_Result.Text.LastIndexOf(" ")),
                    " " + (.0 - Convert.ToDouble(TB_Result.Text.Remove(0, TB_Result.Text.LastIndexOf(" ")))).ToString());
            }
            FontSizeAutoChange();
        }
        private void B_Dot_Click(object sender, RoutedEventArgs e)
        {
            if (!TB_Result.Text.EndsWith(" ") && TB_Result.Text != "" && !TB_Result.Text.EndsWith("."))
            {
                TB_Result.Text += ".";
                FontSizeAutoChange();
            }
        }
        private void B_Equation_Click(object sender, RoutedEventArgs e)
        {
            if (!isCalculated)
            {
                if (TB_Result.Text.Contains(' '))
                {
                    if (!TB_Result.Text.EndsWith(" "))
                    {
                        Calculation();
                    }
                }
            }
        }
    //--------------------------------------------------------------------------\\
        private void B_EraseLast_Click(object sender, RoutedEventArgs e)
        {
            if (TB_Result.Text != "")
                TB_Result.Text = TB_Result.Text.Remove(TB_Result.Text.Length - 1, 1);
            if (TB_Result.Text == "")
                TB_Result.Text = "0";
            FontSizeAutoChange();
        }
        private void B_Clear_Click(object sender, RoutedEventArgs e)
        {
            TB_Result.Text = "0";
            FontSizeAutoChange();
        }
    //--------------------------------------------------------------------------\\
        private void B_History_Click(object sender, RoutedEventArgs e)
        {
            if (!isHistoryClicked)
            {
                R_BG_History.Visibility = Visibility.Visible;
                SV_History.Visibility = Visibility.Visible;
                TB_History.Visibility = Visibility.Visible;
                isHistoryClicked = true;
            }
            else
            {
                R_BG_History.Visibility = Visibility.Collapsed;
                SV_History.Visibility = Visibility.Collapsed;
                TB_History.Visibility = Visibility.Collapsed;
                isHistoryClicked = false;
            }
        }
        private void B_About_Click(object sender, RoutedEventArgs e)
        {
            if (!isAboutClicked)
            {
                R_BG_About.Visibility = Visibility.Visible;
                TB_About.Visibility = Visibility.Visible;
                isAboutClicked = true;
            }
            else
            {
                R_BG_About.Visibility = Visibility.Collapsed;
                TB_About.Visibility = Visibility.Collapsed;
                isAboutClicked = false;
            }
        }
        #endregion

        private void Calcul8OR_MainPage_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            switch (e.Key)
            {
                #region Digits
                case Windows.System.VirtualKey.Number0:
                    ButtonClickAction("0");
                    break;
                case Windows.System.VirtualKey.Number1:
                    ButtonClickAction("1");
                    break;
                case Windows.System.VirtualKey.Number2:
                    ButtonClickAction("2");
                    break;
                case Windows.System.VirtualKey.Number3:
                    ButtonClickAction("3");
                    break;
                case Windows.System.VirtualKey.Number4:
                    ButtonClickAction("4");
                    break;
                case Windows.System.VirtualKey.Number5:
                    ButtonClickAction("5");
                    break;
                case Windows.System.VirtualKey.Number6:
                    ButtonClickAction("6");
                    break;
                case Windows.System.VirtualKey.Number7:
                    ButtonClickAction("7");
                    break;
                case Windows.System.VirtualKey.Number8:
                    ButtonClickAction("8");
                    break;
                case Windows.System.VirtualKey.Number9:
                    ButtonClickAction("9");
                    break;
                case Windows.System.VirtualKey.NumberPad0:
                    ButtonClickAction("0");
                    break;
                case Windows.System.VirtualKey.NumberPad1:
                    ButtonClickAction("1");
                    break;
                case Windows.System.VirtualKey.NumberPad2:
                    ButtonClickAction("2");
                    break;
                case Windows.System.VirtualKey.NumberPad3:
                    ButtonClickAction("3");
                    break;
                case Windows.System.VirtualKey.NumberPad4:
                    ButtonClickAction("4");
                    break;
                case Windows.System.VirtualKey.NumberPad5:
                    ButtonClickAction("5");
                    break;
                case Windows.System.VirtualKey.NumberPad6:
                    ButtonClickAction("6");
                    break;
                case Windows.System.VirtualKey.NumberPad7:
                    ButtonClickAction("7");
                    break;
                case Windows.System.VirtualKey.NumberPad8:
                    ButtonClickAction("8");
                    break;
                case Windows.System.VirtualKey.NumberPad9:
                    ButtonClickAction("9");
                    break;
                #endregion
                #region Operations
                case Windows.System.VirtualKey.Add:
                    Operation(" + ");
                    break;
                case Windows.System.VirtualKey.Subtract:
                    Operation(" - ");
                    break;
                case Windows.System.VirtualKey.Multiply:
                    Operation(" * ");
                    break;
                case Windows.System.VirtualKey.Divide:
                    Operation(" / ");
                    break;
                case Windows.System.VirtualKey.H:
                    Operation(" % ");
                    break;
                case Windows.System.VirtualKey.P:
                    Operation(" ^ ");
                    break;
                case Windows.System.VirtualKey.R:
                    Operation(" √ ");
                    break;
                case Windows.System.VirtualKey.A:
                    Operation(" ~ ");
                    break;
                #endregion
                #region Common
                case Windows.System.VirtualKey.Decimal:
                    if (!TB_Result.Text.EndsWith(" ") && TB_Result.Text != "" && !TB_Result.Text.EndsWith("."))
                    {
                        TB_Result.Text += ".";
                    }
                    break;
                case Windows.System.VirtualKey.E:
                    if (!isCalculated)
                    {
                        if (TB_Result.Text.Contains(' '))
                        {
                            if (!TB_Result.Text.EndsWith(" "))
                            {
                                Calculation();
                            }
                        }
                    }
                    break;
                case Windows.System.VirtualKey.Back:
                    if (TB_Result.Text != "")
                        TB_Result.Text = TB_Result.Text.Remove(TB_Result.Text.Length - 1, 1);
                    if (TB_Result.Text == "")
                        TB_Result.Text = "0";
                    break;
                case Windows.System.VirtualKey.C:
                    TB_Result.Text = "0";
                    break;
                case Windows.System.VirtualKey.O:
                    if (!isHistoryClicked)
                    {
                        R_BG_History.Visibility = Visibility.Visible;
                        SV_History.Visibility = Visibility.Visible;
                        TB_History.Visibility = Visibility.Visible;
                        isHistoryClicked = true;
                    }
                    else
                    {
                        R_BG_History.Visibility = Visibility.Collapsed;
                        SV_History.Visibility = Visibility.Collapsed;
                        TB_History.Visibility = Visibility.Collapsed;
                        isHistoryClicked = false;
                    }
                    break;
                case Windows.System.VirtualKey.F1:
                    if (!isAboutClicked)
                    {
                        R_BG_About.Visibility = Visibility.Visible;
                        TB_About.Visibility = Visibility.Visible;
                        isAboutClicked = true;
                    }
                    else
                    {
                        R_BG_About.Visibility = Visibility.Collapsed;
                        TB_About.Visibility = Visibility.Collapsed;
                        isAboutClicked = false;
                    }
                    break;
                #endregion
            }
        }

    }
}

// need to find out how to add a sound on click event