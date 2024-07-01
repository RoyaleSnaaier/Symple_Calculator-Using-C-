using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Rekenmachine
{
    public partial class MainWindow : Window
    {
        private double _currentValue = 0;
        private string _currentOperator = "";
        private bool _isOperatorClicked = false;
        private double _memoryValue = 0;

        private MediaPlayer _hoverSound;
        private MediaPlayer _clickSound;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize and configure hover sound
            _hoverSound = new MediaPlayer();
            _hoverSound.Volume = 0.1; // Adjust volume as needed
            _hoverSound.MediaEnded += (sender, e) => { _hoverSound.Stop(); };

            // Load sound file
            _hoverSound.Open(new Uri("sounds/hover.wav", UriKind.RelativeOrAbsolute));

            // Initialize and configure click sound
            _clickSound = new MediaPlayer();
            _clickSound.Volume = 0.1; // Adjust volume as needed
            _clickSound.MediaEnded += (sender, e) => { _clickSound.Stop(); };

            // Load sound file
            _clickSound.Open(new Uri("sounds/click.wav", UriKind.RelativeOrAbsolute));
        }

        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                _hoverSound.Position = TimeSpan.Zero; // Rewind sound to start
                _hoverSound.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing hover sound: " + ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Display == null) return;

            if (_isOperatorClicked)
            {
                Display.Text = "";
                _isOperatorClicked = false;
            }

            Button button = (Button)sender;
            Display.Text += button.Content.ToString();

            try
            {
                _clickSound.Position = TimeSpan.Zero; // Rewind sound to start
                _clickSound.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing click sound: " + ex.Message);
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            if (Display == null) return;

            try
            {
                double newValue = double.Parse(Display.Text);
                switch (_currentOperator)
                {
                    case "+":
                        _currentValue += newValue;
                        break;
                    case "-":
                        _currentValue -= newValue;
                        break;
                    case "*":
                        _currentValue *= newValue;
                        break;
                    case "/":
                        if (newValue == 0)
                            throw new DivideByZeroException("Kan niet delen door nul");
                        _currentValue /= newValue;
                        break;
                    case "^":
                        _currentValue = Math.Pow(_currentValue, newValue);
                        break;
                    default:
                        _currentValue = newValue;
                        break;
                }
            }
            catch (DivideByZeroException ex)
            {
                Display.Text = "Fout: " + ex.Message;
            }
            catch (FormatException)
            {
                Display.Text = "Fout: Ongeldige invoer";
            }
            catch (Exception ex)
            {
                Display.Text = "Fout: " + ex.Message;
            }
            finally
            {
                _currentOperator = ((Button)sender).Content.ToString();
                _isOperatorClicked = true;
            }

            try
            {
                _clickSound.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing click sound: " + ex.Message);
            }
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (Display == null) return;

            try
            {
                double newValue = double.Parse(Display.Text);
                switch (_currentOperator)
                {
                    case "+":
                        _currentValue += newValue;
                        break;
                    case "-":
                        _currentValue -= newValue;
                        break;
                    case "*":
                        _currentValue *= newValue;
                        break;
                    case "/":
                        if (newValue == 0)
                            throw new DivideByZeroException("Kan niet delen door nul");
                        _currentValue /= newValue;
                        break;
                    case "^":
                        _currentValue = Math.Pow(_currentValue, newValue);
                        break;
                }

                Display.Text = _currentValue.ToString();
            }
            catch (DivideByZeroException ex)
            {
                Display.Text = "Fout: " + ex.Message;
            }
            catch (FormatException)
            {
                Display.Text = "Fout: Ongeldige invoer";
            }
            catch (Exception ex)
            {
                Display.Text = "Fout: " + ex.Message;
            }
            finally
            {
                _currentOperator = "";
                _isOperatorClicked = false;
            }

            try
            {
                _clickSound.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing click sound: " + ex.Message);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            if (Display == null) return;
            Display.Text = "";
            _currentValue = 0;
            _currentOperator = "";
            _isOperatorClicked = false;

            try
            {
                _clickSound.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing click sound: " + ex.Message);
            }
        }

        private void SquareRoot_Click(object sender, RoutedEventArgs e)
        {
            if (Display == null) return;
            try
            {
                double value = double.Parse(Display.Text);
                if (value < 0)
                    throw new Exception("Negatieve wortel niet mogelijk");
                Display.Text = Math.Sqrt(value).ToString();
            }
            catch (FormatException)
            {
                Display.Text = "Fout: Ongeldige invoer";
            }
            catch (Exception ex)
            {
                Display.Text = "Fout: " + ex.Message;
            }

            try
            {
                _clickSound.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing click sound: " + ex.Message);
            }
        }

        private void Power_Click(object sender, RoutedEventArgs e)
        {
            if (Display == null) return;
            try
            {
                _currentValue = double.Parse(Display.Text);
                _currentOperator = "^";
                _isOperatorClicked = true;
            }
            catch (FormatException)
            {
                Display.Text = "Fout: Ongeldige invoer";
            }

            try
            {
                _clickSound.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing click sound: " + ex.Message);
            }
        }

        private void Sin_Click(object sender, RoutedEventArgs e)
        {
            if (Display == null) return;
            try
            {
                double value = double.Parse(Display.Text);
                Display.Text = Math.Sin(value).ToString();
            }
            catch (FormatException)
            {
                Display.Text = "Fout: Ongeldige invoer";
            }

            try
            {
                _clickSound.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing click sound: " + ex.Message);
            }
        }

        private void Cos_Click(object sender, RoutedEventArgs e)
        {
            if (Display == null) return;
            try
            {
                double value = double.Parse(Display.Text);
                Display.Text = Math.Cos(value).ToString();
            }
            catch (FormatException)
            {
                Display.Text = "Fout: Ongeldige invoer";
            }

            try
            {
                _clickSound.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing click sound: " + ex.Message);
            }
        }

        private void MemoryRecall_Click(object sender, RoutedEventArgs e)
        {
            Display.Text = _memoryValue.ToString();

            try
            {
                _clickSound.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing click sound: " + ex.Message);
            }
        }

        private void MemoryPlus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _memoryValue += double.Parse(Display.Text);
            }
            catch (FormatException)
            {
                Display.Text = "Fout: Ongeldige invoer";
            }

            try
            {
                _clickSound.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing click sound: " + ex.Message);
            }
        }

        private void MemoryMinus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _memoryValue -= double.Parse(Display.Text);
            }
            catch (FormatException)
            {
                Display.Text = "Fout: Ongeldige invoer";
            }

            try
            {
                _clickSound.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing click sound: " + ex.Message);
            }
        }
    }
}
