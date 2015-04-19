using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace KioskDisplay.Controls
{
	public class TouchScreenKeyboard : Window
	{
		#region Property & Variable & Constructor

		protected static bool ShiftFlag { get; set; }

		protected static bool CapsLockFlag { get; set; }

		private static Window _instanceObject;

		private static Brush _previousTextBoxBackgroundBrush;
		private static Brush _previousTextBoxBorderBrush;
		private static Thickness _previousTextBoxBorderThickness;

		private static Control _currentControl;
		public static string TouchScreenText
		{
			get
			{
				if (_currentControl is TextBox)
				{
					return ((TextBox)_currentControl).Text;
				}
				if (_currentControl is PasswordBox)
				{
					return ((PasswordBox)_currentControl).Password;
				}
				return "";
			}
			set
			{
				if (_currentControl is TextBox)
				{
					((TextBox)_currentControl).Text = value;
				}
				else if (_currentControl is PasswordBox)
				{
					((PasswordBox)_currentControl).Password = value;
				}
			}
		}

		public static RoutedUICommand CmdTlide = new RoutedUICommand();
		public static RoutedUICommand Cmd1 = new RoutedUICommand();
		public static RoutedUICommand Cmd2 = new RoutedUICommand();
		public static RoutedUICommand Cmd3 = new RoutedUICommand();
		public static RoutedUICommand Cmd4 = new RoutedUICommand();
		public static RoutedUICommand Cmd5 = new RoutedUICommand();
		public static RoutedUICommand Cmd6 = new RoutedUICommand();
		public static RoutedUICommand Cmd7 = new RoutedUICommand();
		public static RoutedUICommand Cmd8 = new RoutedUICommand();
		public static RoutedUICommand Cmd9 = new RoutedUICommand();
		public static RoutedUICommand Cmd0 = new RoutedUICommand();
		public static RoutedUICommand CmdMinus = new RoutedUICommand();
		public static RoutedUICommand CmdPlus = new RoutedUICommand();
		public static RoutedUICommand CmdBackspace = new RoutedUICommand();

		public static RoutedUICommand CmdTab = new RoutedUICommand();
		public static RoutedUICommand CmdQ = new RoutedUICommand();
		public static RoutedUICommand Cmdw = new RoutedUICommand();
		public static RoutedUICommand CmdE = new RoutedUICommand();
		public static RoutedUICommand CmdR = new RoutedUICommand();
		public static RoutedUICommand CmdT = new RoutedUICommand();
		public static RoutedUICommand CmdY = new RoutedUICommand();
		public static RoutedUICommand CmdU = new RoutedUICommand();
		public static RoutedUICommand CmdI = new RoutedUICommand();
		public static RoutedUICommand CmdO = new RoutedUICommand();
		public static RoutedUICommand CmdP = new RoutedUICommand();
		public static RoutedUICommand CmdOpenCrulyBrace = new RoutedUICommand();
		public static RoutedUICommand CmdEndCrultBrace = new RoutedUICommand();
		public static RoutedUICommand CmdOr = new RoutedUICommand();

		public static RoutedUICommand CmdCapsLock = new RoutedUICommand();
		public static RoutedUICommand CmdA = new RoutedUICommand();
		public static RoutedUICommand CmdS = new RoutedUICommand();
		public static RoutedUICommand CmdD = new RoutedUICommand();
		public static RoutedUICommand CmdF = new RoutedUICommand();
		public static RoutedUICommand CmdG = new RoutedUICommand();
		public static RoutedUICommand CmdH = new RoutedUICommand();
		public static RoutedUICommand CmdJ = new RoutedUICommand();
		public static RoutedUICommand CmdK = new RoutedUICommand();
		public static RoutedUICommand CmdL = new RoutedUICommand();
		public static RoutedUICommand CmdColon = new RoutedUICommand();
		public static RoutedUICommand CmdDoubleInvertedComma = new RoutedUICommand();
		public static RoutedUICommand CmdEnter = new RoutedUICommand();

		public static RoutedUICommand CmdShift = new RoutedUICommand();
		public static RoutedUICommand CmdZ = new RoutedUICommand();
		public static RoutedUICommand CmdX = new RoutedUICommand();
		public static RoutedUICommand CmdC = new RoutedUICommand();
		public static RoutedUICommand CmdV = new RoutedUICommand();
		public static RoutedUICommand CmdB = new RoutedUICommand();
		public static RoutedUICommand CmdN = new RoutedUICommand();
		public static RoutedUICommand CmdM = new RoutedUICommand();
		public static RoutedUICommand CmdGreaterThan = new RoutedUICommand();
		public static RoutedUICommand CmdLessThan = new RoutedUICommand();
		public static RoutedUICommand CmdQuestion = new RoutedUICommand();

		public static RoutedUICommand CmdSpaceBar = new RoutedUICommand();
		public static RoutedUICommand CmdClear = new RoutedUICommand();

		static TouchScreenKeyboard()
		{
			DefaultStyleKeyProperty.OverrideMetadata(
				typeof(TouchScreenKeyboard),
				new FrameworkPropertyMetadata(typeof(TouchScreenKeyboard)));

			SetCommandBinding();
		}
		#endregion

		#region CommandRelatedCode

		private static void SetCommandBinding()
		{
			var cbTlide = new CommandBinding(CmdTlide, RunCommand);
			var cb1 = new CommandBinding(Cmd1, RunCommand);
			var cb2 = new CommandBinding(Cmd2, RunCommand);
			var cb3 = new CommandBinding(Cmd3, RunCommand);
			var cb4 = new CommandBinding(Cmd4, RunCommand);
			var cb5 = new CommandBinding(Cmd5, RunCommand);
			var cb6 = new CommandBinding(Cmd6, RunCommand);
			var cb7 = new CommandBinding(Cmd7, RunCommand);
			var cb8 = new CommandBinding(Cmd8, RunCommand);
			var cb9 = new CommandBinding(Cmd9, RunCommand);
			var cb0 = new CommandBinding(Cmd0, RunCommand);
			var cbMinus = new CommandBinding(CmdMinus, RunCommand);
			var cbPlus = new CommandBinding(CmdPlus, RunCommand);
			var cbBackspace = new CommandBinding(CmdBackspace, RunCommand);

			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbTlide);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cb1);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cb2);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cb3);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cb4);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cb5);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cb6);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cb7);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cb8);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cb9);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cb0);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbMinus);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbPlus);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbBackspace);

			var cbTab = new CommandBinding(CmdTab, RunCommand);
			var cbQ = new CommandBinding(CmdQ, RunCommand);
			var cbw = new CommandBinding(Cmdw, RunCommand);
			var cbE = new CommandBinding(CmdE, RunCommand);
			var cbR = new CommandBinding(CmdR, RunCommand);
			var cbT = new CommandBinding(CmdT, RunCommand);
			var cbY = new CommandBinding(CmdY, RunCommand);
			var cbU = new CommandBinding(CmdU, RunCommand);
			var cbI = new CommandBinding(CmdI, RunCommand);
			var cbo = new CommandBinding(CmdO, RunCommand);
			var cbP = new CommandBinding(CmdP, RunCommand);
			var cbOpenCrulyBrace = new CommandBinding(CmdOpenCrulyBrace, RunCommand);
			var cbEndCrultBrace = new CommandBinding(CmdEndCrultBrace, RunCommand);
			var cbOr = new CommandBinding(CmdOr, RunCommand);

			var cbCapsLock = new CommandBinding(CmdCapsLock, RunCommand);
			var cbA = new CommandBinding(CmdA, RunCommand);
			var cbS = new CommandBinding(CmdS, RunCommand);
			var cbD = new CommandBinding(CmdD, RunCommand);
			var cbF = new CommandBinding(CmdF, RunCommand);
			var cbG = new CommandBinding(CmdG, RunCommand);
			var cbH = new CommandBinding(CmdH, RunCommand);
			var cbJ = new CommandBinding(CmdJ, RunCommand);
			var cbK = new CommandBinding(CmdK, RunCommand);
			var cbL = new CommandBinding(CmdL, RunCommand);
			var cbColon = new CommandBinding(CmdColon, RunCommand);
			var cbDoubleInvertedComma = new CommandBinding(CmdDoubleInvertedComma, RunCommand);
			var cbEnter = new CommandBinding(CmdEnter, RunCommand);

			var cbShift = new CommandBinding(CmdShift, RunCommand);
			var cbZ = new CommandBinding(CmdZ, RunCommand);
			var cbX = new CommandBinding(CmdX, RunCommand);
			var cbC = new CommandBinding(CmdC, RunCommand);
			var cbV = new CommandBinding(CmdV, RunCommand);
			var cbB = new CommandBinding(CmdB, RunCommand);
			var cbN = new CommandBinding(CmdN, RunCommand);
			var cbM = new CommandBinding(CmdM, RunCommand);
			var cbGreaterThan = new CommandBinding(CmdGreaterThan, RunCommand);
			var cbLessThan = new CommandBinding(CmdLessThan, RunCommand);
			var cbQuestion = new CommandBinding(CmdQuestion, RunCommand);

			var cbSpaceBar = new CommandBinding(CmdSpaceBar, RunCommand);
			var cbClear = new CommandBinding(CmdClear, RunCommand);

			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbTab);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbQ);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbw);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbE);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbR);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbT);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbY);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbU);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbI);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbo);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbP);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbOpenCrulyBrace);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbEndCrultBrace);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbOr);

			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbCapsLock);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbA);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbS);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbD);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbF);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbG);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbH);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbJ);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbK);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbL);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbColon);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbDoubleInvertedComma);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbEnter);

			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbShift);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbZ);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbX);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbC);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbV);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbB);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbN);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbM);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbGreaterThan);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbLessThan);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbQuestion);

			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbSpaceBar);
			CommandManager.RegisterClassCommandBinding(typeof(TouchScreenKeyboard), cbClear);

		}
		static void RunCommand(object sender, ExecutedRoutedEventArgs e)
		{
			if (e.Command == CmdTlide)  //First Row
			{
				if (!ShiftFlag)
				{
					TouchScreenText += "`";
				}
				else
				{
					TouchScreenText += "~";
					ShiftFlag = false;
				}
			}
			else if (e.Command == Cmd1)
			{
				if (!ShiftFlag)
				{
					TouchScreenText += "1";
				}
				else
				{
					TouchScreenText += "!";
					ShiftFlag = false;
				}
			}
			else if (e.Command == Cmd2)
			{
				if (!ShiftFlag)
				{
					TouchScreenText += "2";
				}
				else
				{
					TouchScreenText += "@";
					ShiftFlag = false;
				}
			}
			else if (e.Command == Cmd3)
			{
				if (!ShiftFlag)
				{
					TouchScreenText += "3";
				}
				else
				{
					TouchScreenText += "#";
					ShiftFlag = false;
				}
			}
			else if (e.Command == Cmd4)
			{
				if (!ShiftFlag)
				{
					TouchScreenText += "4";
				}
				else
				{
					TouchScreenText += "$";
					ShiftFlag = false;
				}
			}
			else if (e.Command == Cmd5)
			{
				if (!ShiftFlag)
				{
					TouchScreenText += "5";
				}
				else
				{
					TouchScreenText += "%";
					ShiftFlag = false;
				}
			}
			else if (e.Command == Cmd6)
			{
				if (!ShiftFlag)
				{
					TouchScreenText += "6";
				}
				else
				{
					TouchScreenText += "^";
					ShiftFlag = false;
				}
			}
			else if (e.Command == Cmd7)
			{
				if (!ShiftFlag)
				{
					TouchScreenText += "7";
				}
				else
				{
					TouchScreenText += "&";
					ShiftFlag = false;
				}
			}
			else if (e.Command == Cmd8)
			{
				if (!ShiftFlag)
				{
					TouchScreenText += "8";
				}
				else
				{
					TouchScreenText += "*";
					ShiftFlag = false;
				}
			}
			else if (e.Command == Cmd9)
			{
				if (!ShiftFlag)
				{
					TouchScreenText += "9";
				}
				else
				{
					TouchScreenText += "(";
					ShiftFlag = false;
				}
			}
			else if (e.Command == Cmd0)
			{
				if (!ShiftFlag)
				{
					TouchScreenText += "0";
				}
				else
				{
					TouchScreenText += ")";
					ShiftFlag = false;
				}

			}
			else if (e.Command == CmdMinus)
			{
				if (!ShiftFlag)
				{
					TouchScreenText += "-";
				}
				else
				{
					TouchScreenText += "_";
					ShiftFlag = false;
				}
			}
			else if (e.Command == CmdPlus)
			{
				if (!ShiftFlag)
				{
					TouchScreenText += "=";
				}
				else
				{
					TouchScreenText += "+";
					ShiftFlag = false;
				}
			}
			else if (e.Command == CmdBackspace)
			{
				if (!string.IsNullOrEmpty(TouchScreenText))
				{
					TouchScreenText = TouchScreenText.Substring(0, TouchScreenText.Length - 1);
				}
			}
			else if (e.Command == CmdTab)  //Second Row
			{
				TouchScreenText += "     ";
			}
			else if (e.Command == CmdQ)
			{
				AddKeyBoardINput('Q');
			}
			else if (e.Command == Cmdw)
			{
				AddKeyBoardINput('w');
			}
			else if (e.Command == CmdE)
			{
				AddKeyBoardINput('E');
			}
			else if (e.Command == CmdR)
			{
				AddKeyBoardINput('R');
			}
			else if (e.Command == CmdT)
			{
				AddKeyBoardINput('T');
			}
			else if (e.Command == CmdY)
			{
				AddKeyBoardINput('Y');
			}
			else if (e.Command == CmdU)
			{
				AddKeyBoardINput('U');
			}
			else if (e.Command == CmdI)
			{
				AddKeyBoardINput('I');
			}
			else if (e.Command == CmdO)
			{
				AddKeyBoardINput('O');
			}
			else if (e.Command == CmdP)
			{
				AddKeyBoardINput('P');
			}
			else if (e.Command == CmdOpenCrulyBrace)
			{
				if (!ShiftFlag)
				{
					TouchScreenText += "[";
				}
				else
				{
					TouchScreenText += "{";
					ShiftFlag = false;
				}
			}
			else if (e.Command == CmdEndCrultBrace)
			{
				if (!ShiftFlag)
				{
					TouchScreenText += "]";
				}
				else
				{
					TouchScreenText += "}";
					ShiftFlag = false;
				}
			}
			else if (e.Command == CmdOr)
			{
				if (!ShiftFlag)
				{
					TouchScreenText += @"\";
				}
				else
				{
					TouchScreenText += "|";
					ShiftFlag = false;
				}
			}
			else if (e.Command == CmdCapsLock)  //Third ROw
			{

				if (!CapsLockFlag)
				{
					CapsLockFlag = true;
				}
				else
				{
					CapsLockFlag = false;
				}
			}
			else if (e.Command == CmdA)
			{
				AddKeyBoardINput('A');
			}
			else if (e.Command == CmdS)
			{
				AddKeyBoardINput('S');
			}
			else if (e.Command == CmdD)
			{
				AddKeyBoardINput('D');
			}
			else if (e.Command == CmdF)
			{
				AddKeyBoardINput('F');
			}
			else if (e.Command == CmdG)
			{
				AddKeyBoardINput('G');
			}
			else if (e.Command == CmdH)
			{
				AddKeyBoardINput('H');
			}
			else if (e.Command == CmdJ)
			{
				AddKeyBoardINput('J');
			}
			else if (e.Command == CmdK)
			{
				AddKeyBoardINput('K');
			}
			else if (e.Command == CmdL)
			{
				AddKeyBoardINput('L');

			}
			else if (e.Command == CmdColon)
			{
				if (!ShiftFlag)
				{
					TouchScreenText += ";";
				}
				else
				{
					TouchScreenText += ":";
					ShiftFlag = false;
				}

			}
			else if (e.Command == CmdDoubleInvertedComma)
			{
				if (!ShiftFlag)
				{
					TouchScreenText += "'";
				}
				else
				{
					TouchScreenText += Char.ConvertFromUtf32(34);
					ShiftFlag = false;
				}
			}
			else if (e.Command == CmdEnter)
			{
				if (_instanceObject != null)
				{
					_instanceObject.Close();
					_instanceObject = null;
				}
				_currentControl.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
			}
			else if (e.Command == CmdShift) //Fourth Row
			{
				ShiftFlag = true;
			}
			else if (e.Command == CmdZ)
			{
				AddKeyBoardINput('Z');
			}
			else if (e.Command == CmdX)
			{
				AddKeyBoardINput('X');
			}
			else if (e.Command == CmdC)
			{
				AddKeyBoardINput('C');
			}
			else if (e.Command == CmdV)
			{
				AddKeyBoardINput('V');
			}
			else if (e.Command == CmdB)
			{
				AddKeyBoardINput('B');
			}
			else if (e.Command == CmdN)
			{
				AddKeyBoardINput('N');
			}
			else if (e.Command == CmdM)
			{
				AddKeyBoardINput('M');
			}
			else if (e.Command == CmdLessThan)
			{
				if (!ShiftFlag)
				{
					TouchScreenText += ",";
				}
				else
				{
					TouchScreenText += "<";
					ShiftFlag = false;
				}
			}
			else if (e.Command == CmdGreaterThan)
			{
				if (!ShiftFlag)
				{
					TouchScreenText += ".";
				}
				else
				{
					TouchScreenText += ">";
					ShiftFlag = false;
				}
			}
			else if (e.Command == CmdQuestion)
			{
				if (!ShiftFlag)
				{
					TouchScreenText += "/";
				}
				else
				{
					TouchScreenText += "?";
					ShiftFlag = false;
				}
			}
			else if (e.Command == CmdSpaceBar)//Last row
			{
				TouchScreenText += " ";
			}
			else if (e.Command == CmdClear)//Last row
			{
				TouchScreenText = "";
			}
		}

		#endregion

		#region Main Functionality

		private static void AddKeyBoardINput(char input)
		{
			if (CapsLockFlag)
			{
				if (ShiftFlag)
				{
					TouchScreenText += char.ToLower(input).ToString();
					ShiftFlag = false;

				}
				else
				{
					TouchScreenText += char.ToUpper(input).ToString();
				}
			}
			else
			{
				if (!ShiftFlag)
				{
					TouchScreenText += char.ToLower(input).ToString();
				}
				else
				{
					TouchScreenText += char.ToUpper(input).ToString();
					ShiftFlag = false;
				}
			}
		}

		private static void SyncChild()
		{
			if (_currentControl == null || _instanceObject == null)
			{
				return;
			}

			var widthTouchKeyboard = _instanceObject.ActualWidth;

			var virtualpoint = new Point(0, _currentControl.ActualHeight + 3);
			var actualpoint = _currentControl.PointToScreen(virtualpoint);

			if (widthTouchKeyboard + actualpoint.X > SystemParameters.PrimaryScreenWidth)
			{
				var difference = widthTouchKeyboard + actualpoint.X - SystemParameters.PrimaryScreenWidth;
				actualpoint.X = actualpoint.X - difference;
			}
			else if (!(actualpoint.X > 1))
			{
				actualpoint.X = 1;
			}

			_instanceObject.Left = actualpoint.X;
			_instanceObject.Top = actualpoint.Y;
			_instanceObject.Show();
		}

		public static bool GetTouchScreenKeyboard(DependencyObject obj)
		{
			return (bool)obj.GetValue(TouchScreenKeyboardProperty);
		}

		public static void SetTouchScreenKeyboard(DependencyObject obj, bool value)
		{
			obj.SetValue(TouchScreenKeyboardProperty, value);
		}

		public static readonly DependencyProperty TouchScreenKeyboardProperty =
			DependencyProperty.RegisterAttached("TouchScreenKeyboard",
			typeof(bool), typeof(TouchScreenKeyboard),
			new UIPropertyMetadata(default(bool), TouchScreenKeyboardPropertyChanged));

		static void TouchScreenKeyboardPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var host = sender as FrameworkElement;
			if (host == null) return;

			host.GotFocus += OnGotFocus;
			host.LostFocus += OnLostFocus;
		}

		static void OnGotFocus(object sender, RoutedEventArgs e)
		{
			var host = sender as Control;

			if (host != null)
			{
				_previousTextBoxBackgroundBrush = host.Background;
				_previousTextBoxBorderBrush = host.BorderBrush;
				_previousTextBoxBorderThickness = host.BorderThickness;

				//host.Background = Brushes.Yellow;
				host.BorderBrush = (Brush)host.FindResource("HighlightForegroundBrush");
				host.BorderThickness = new Thickness(2);

				_currentControl = host;

				if (_instanceObject != null) return;

				var window = Application.Current.MainWindow;
				window.LocationChanged += TouchScreenKeyboard_LocationChanged;
				window.Activated += TouchScreenKeyboard_Activated;
				window.Deactivated += TouchScreenKeyboard_Deactivated;

				_instanceObject = new TouchScreenKeyboard
				{
					AllowsTransparency = true,
					WindowStyle = WindowStyle.None,
					ShowInTaskbar = false
				};

				_instanceObject.ShowInTaskbar = false;
				_instanceObject.Topmost = true;
				_instanceObject.SizeToContent = SizeToContent.WidthAndHeight;
				_instanceObject.HorizontalAlignment = HorizontalAlignment.Center;

				host.LayoutUpdated += tb_LayoutUpdated;
			}
		}

		static void RestartInactivityTimer()
		{
			var application = (App)Application.Current;
			application.StopInactivityTimer();
			application.StartInactivityTimer();
		}

		static void TouchScreenKeyboard_Deactivated(object sender, EventArgs e)
		{
			RestartInactivityTimer();

			if (_instanceObject != null)
			{
				_instanceObject.Topmost = false;
			}
		}

		static void TouchScreenKeyboard_Activated(object sender, EventArgs e)
		{
			RestartInactivityTimer();

			if (_instanceObject != null)
			{
				_instanceObject.Topmost = true;
			}
		}

		static void TouchScreenKeyboard_LocationChanged(object sender, EventArgs e)
		{
			RestartInactivityTimer();

			SyncChild();
		}

		static void tb_LayoutUpdated(object sender, EventArgs e)
		{
			RestartInactivityTimer();

			SyncChild();
		}

		static void OnLostFocus(object sender, RoutedEventArgs e)
		{
			RestartInactivityTimer();

			var host = sender as Control;
			if (host != null)
			{
				host.Background = _previousTextBoxBackgroundBrush;
				host.BorderBrush = _previousTextBoxBorderBrush;
				host.BorderThickness = _previousTextBoxBorderThickness;
			}

			if (_instanceObject != null)
			{
				_instanceObject.Close();
				_instanceObject = null;
			}
		}

		#endregion
	}
}
