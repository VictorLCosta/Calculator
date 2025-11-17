using System.Windows;
using System.Windows.Controls;

namespace Calculator;

public partial class MainWindow : Window
{
    private string userInput = "0";
    private double operand = 0.0;
    private double operand2 = 0.0;
    private double result = 0.0;
    private SelectedOperator? selectedOperator = null;

    private readonly string[] digits = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"];
    private readonly Dictionary<string, SelectedOperator> operators = new()
    {
        {"+", SelectedOperator.Addition},
        {"-", SelectedOperator.Subtraction},
        {"/", SelectedOperator.Division},
        {"*", SelectedOperator.Multiplication}
    };

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var selectedValue = button.Content.ToString()!;

        if (digits.Contains(selectedValue))
        {
            if (selectedValue == "0" && userInput == "0")
                return;

            if (userInput == "0")
            {
                userInput = $"{selectedValue}";
            }
            else
            {
                userInput = $"{userInput}{selectedValue}";
            }

            ResultLabel.Content = userInput;

            return;
        }

        if (operators.TryGetValue(selectedValue, out SelectedOperator @operator)) 
        {
            operand = double.Parse(userInput);
            selectedOperator = @operator;

            userInput = "0";
            ResultLabel.Content = userInput;
            return;
        }

        if (selectedValue == ".")
        {
            if (!userInput.Contains('.'))
                userInput += ".";

            ResultLabel.Content = userInput;
            return;
        }

        if (button == EqualsButton)
        {
            operand2 = double.Parse(userInput);
            Calculate();
            userInput = result.ToString();
            ResultLabel.Content = userInput;
        }

        if (button == NegativeButton)
        {
            result = double.Parse(userInput) * -1;
            userInput = result.ToString();

            ResultLabel.Content = userInput;

            return;
        }

        if (button == ACButton)
            Reset();
    }

    private void PercentageButton_Click(object sender, RoutedEventArgs e)
    {
        double tempNumber;
        if (double.TryParse(userInput, out tempNumber))
        {
            tempNumber = (tempNumber / 100);
            if (operand != 0)
                tempNumber *= operand;
            ResultLabel.Content = tempNumber.ToString();
        }
    }

    private void Reset()
    {
        userInput = "0";
        result = 0.0;
        operand = 0.0;
        operand2 = 0.0;

        ResultLabel.Content = userInput;
    }

    private void Calculate()
    {
        switch (selectedOperator)
        {
            case SelectedOperator.Addition:
                result = operand + operand2;
                break;
            case SelectedOperator.Subtraction:
                result = operand - operand2;
                break;
            case SelectedOperator.Multiplication:
                result = operand * operand2;
                break;
            case SelectedOperator.Division:
                result = operand / operand2;
                break;
            case null:
                break;
        }
    }

}

public enum SelectedOperator
{
    Addition, 
    Subtraction, 
    Multiplication, 
    Division,
}