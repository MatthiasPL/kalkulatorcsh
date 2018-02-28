using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Forms;

namespace kalkulatorcsh
{
    public partial class OknoKalkulatora : Form
    {
        Double resultValue=0;
        
        //the operation we want to execute
        string executedOperation = "";

        //if an operation was performed (+, -, *, /)
        bool isOperationPerformed = false;
        String operationPerformed = "";
        
        //gets the operator character
        String separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator; 

        public OknoKalkulatora()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            //adding the separator character
            if (button.Text == ".")
            {
                if (!result.Text.Contains(separator))
                    result.Text += separator;
            }

            //clearing all the data that are being kept in the memory
            else if (button.Text == "C")
            {
                resultValue = 0;
                executedOperation = "";
                result.Text = "0";
                label.Text = "";
                isOperationPerformed = false;
            }

            //removing the last character
            else if (button.Text == "Bc")
            {
                //checking if the length of the string is greater than 0
                if (!String.IsNullOrEmpty(result.Text))
                {
                    result.Text = result.Text.TrimEnd(result.Text[result.Text.Length - 1]);
                }
                //after removing everything there will be shown 0
                if (result.Text.Length == 0) result.Text = "0";
            }

            //changing the sign of the number
            else if (button.Text == "+/-")
            {
                if (isOperationPerformed == false)
                {
                    char character = result.Text.FirstOrDefault();
                    if (character == '-')
                    {
                        result.Text = result.Text.Remove(0, 1);
                    }
                    else if (!(result.Text == "0"))
                        result.Text = result.Text.Insert(0, "-");
                }
            }
            //if there was an operation performed (+, -, /, *) the "=" button will force to solve this to count 
            else if (button.Text == "=" && isOperationPerformed == true)
            {
                //adding 0 if there was a separator without any numbers later on
                String key = result.Text[result.Text.Length - 1].ToString();

                if (key == separator)
                    result.Text += "0";
                executedOperation += result.Text;

                //clearing the label
                label.Text = "";

                Double secondValue = Double.Parse(result.Text);

                //the final result
                Double final;

                if (operationPerformed == "+")
                {
                    final = resultValue + secondValue;
                    result.Text = final.ToString();
                }
                else if (operationPerformed == "-")
                {
                    final = resultValue - secondValue;
                    result.Text = final.ToString();
                }
                else if (operationPerformed == "*")
                {
                    final = resultValue * secondValue;
                    result.Text = final.ToString();
                }
                else if (operationPerformed == "/")
                {
                    //adding an exception if the user tries to divide by 0
                    if (secondValue != 0)
                    {
                        final = resultValue / secondValue;
                        result.Text = final.ToString();
                    }
                    else
                    {
                        label.Text = "Can't divide by 0";
                    }
                }
                isOperationPerformed = false;
            }
            else
            {
                //if there is a 0 and we want to write a new number, we clear the 0
                if (result.Text == "0")
                    result.Clear();

                if (button.Text != "=")
                {
                    if (!(button.Text == "+" || button.Text == "-" || button.Text == "*" || button.Text == "/") && (isOperationPerformed == true))
                    {
                        result.Text += button.Text;
                    }
                    else if (isOperationPerformed == false)
                    {
                        result.Text += button.Text;
                    }
                    if ((button.Text == "+" || button.Text == "-" || button.Text == "*" || button.Text == "/") && (isOperationPerformed == false))
                    {
                        //trimming the operator sign
                        result.Text = result.Text.TrimEnd(result.Text[result.Text.Length - 1]);

                        if (result.Text.Length >= 1)
                        {
                            String key = result.Text[result.Text.Length - 1].ToString();
                            if (key == separator)
                            {
                                result.Text += "0";
                                resultValue = Double.Parse(result.Text);
                                result.Text += button.Text;
                            }
                            else
                            {
                                resultValue = Double.Parse(result.Text);
                                result.Text += button.Text;
                            }
                        }

                        operationPerformed = button.Text;
                        isOperationPerformed = true;
                        executedOperation = result.Text;
                        label.Text = result.Text;
                        result.Text = "0";
                    }
                }
            }
        }
    }
}
