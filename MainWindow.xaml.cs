using System.Windows;
using System.Windows.Controls;

namespace Calculator;

public partial class MainWindow : Window
{
    private string userInput = "0";
    private double operand = 0.0;
    private double operand2 = 0.0;
    private double result = 0.0;
    private SelectedOperator selectedOperator;

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

        if (selectedValue == "0" && userInput == "0")
            return;

        if (digits.Contains(selectedValue))
        {
            userInput += selectedValue;
            ResultText.Text = userInput;

            return;
        }

        if (operators.TryGetValue(selectedValue, out SelectedOperator @operator)) 
        {
            operand = double.Parse(userInput);
            selectedOperator = @operator;

            userInput = "0";
            ResultText.Text = userInput;
            return;
        }

        if (button == EqualsButton)
        {
            operand2 = double.Parse(userInput);
            Calculate();
            userInput = result.ToString();
            ResultText.Text = userInput;
        }

        if (button == ACButton)
            Reset();
    }

    private void Reset()
    {
        userInput = "0";
        result = 0.0;
        operand = 0.0;
        operand2 = 0.0;

        ResultText.Text = userInput;
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