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

namespace CoCaRo
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }
    Button[,] lsbuttons;
    //List<Button> lsbtbn = new List<Button>();
     int _rows =0, _columns = 0;
    const int SnakeSquareSize = 40;
    string StateClick = "O";
    private void Window_ContentRendered(object sender, EventArgs e)
    {
      Create();
    }
   
    private void Create()
    {
      int nextX = 0, nextY = 0;
      _rows = Convert.ToInt32(GameArea.ActualHeight / SnakeSquareSize);
      _columns = Convert.ToInt32(GameArea.ActualWidth / SnakeSquareSize);
      lsbuttons = new Button[_rows , _columns];


      for (int i=0; i< _rows; i++)
      {
        nextX = 0;
        for (int j =0; j< _columns; j++)
        {
          Button rect = new Button
          {
            Width = SnakeSquareSize,
            Height = SnakeSquareSize,
            Name="A"+i+"_"+j
          }; 
          rect.Click += Rect_Click;
          lsbuttons[i, j] = rect;
          
          GameArea.Children.Add(rect);
          Canvas.SetTop(rect, nextY); 
          Canvas.SetLeft(rect, nextX);
          nextX = (j + 1) * SnakeSquareSize;
        }
        nextY = (i + 1) * SnakeSquareSize;
      }  


    }

    private void Rect_Click(object sender, RoutedEventArgs e)
    {
      Button btn = (Button)sender;
      if(btn.Content ==null)
      {
        btn.Content = StateClick=="O"? "X" : "O";
        StateClick = StateClick== "O" ? "X" : "O";
        Check(btn);
      }
    }
    void Check(Button _btn)
    {
      int _row = 0;
      int _Column = 0;
      string btnName = _btn.Name.Replace("A", "");
      string[] words = btnName.Split('_');
      _row = int.Parse(words[0]);
      _Column = int.Parse(words[1]);

      if (CheckRow(_row, _Column, _btn.Content.ToString()))
      {
        MessageBox.Show("Win");
      }
      else if (checkcolumn(_row, _Column, _btn.Content.ToString()))
      {
        MessageBox.Show("Win");
      }
      else if (checkmaindiagonal(_row, _Column, _btn.Content.ToString()))
      {
        MessageBox.Show("Win");
      }
      else if (checkauxiliarydiagonal(_row, _Column, _btn.Content.ToString()))
      {
        MessageBox.Show("Win");
      }

    }
    bool CheckRow(int _RowSelect, int _ColumnSelect, string StateClick)
    {
      int Value = 1;
      bool CheckNext = true;
      int CotCheck = _ColumnSelect; // kiểm tra dòng thì column chạy
      #region Kiểm tra lùi
      while (CheckNext)
      {
        CotCheck--;
        //kiểm tra các cột có index >0, trong phạm vi 5 cột kể từ cột chọn về bên trái, và cột 
        if (CotCheck >= 0 && CotCheck> _ColumnSelect-5)
        {          
          CheckNext = CheckObject(_RowSelect, CotCheck, StateClick);
          if (CheckNext)
          {
            Value++;
          }
        }
        else
        {
          CheckNext = false;
        }
      }
      #endregion

      #region Kiểm tra tiến
      CotCheck = _ColumnSelect; // kiểm tra dòng thì column chạy
      CheckNext = true;
      while (CheckNext)
      {
        CotCheck++;
        if (CotCheck < _columns && CotCheck < _ColumnSelect+5)
        {
          CheckNext = CheckObject(_RowSelect, CotCheck, StateClick);
          if (CheckNext)
          {
            Value++;
          }
        }
        else
        {
          CheckNext = false;
        }
      }
      #endregion
      return Value >= 5;
    }

    /// <summary>
    /// Hàng trước, cột sau
    /// </summary>
    /// <param name="_R"></param>
    /// <param name="_C"></param>
    /// <param name="StateClick"></param>
    /// <returns></returns>
    bool CheckObject(int _R, int _C, string StateClick)
    {
      Button btn = lsbuttons[_R,_C];
      if (btn.Content != null)
      {
        if (btn.Content.ToString() == StateClick)
        {
          return true;
        }
      }
      return false;
    }

    bool checkcolumn(int _RowSelect, int _ColumnSelect, string StateClick)
    {
      int Value = 1;
      bool CheckNext = true;
      int HangCheck = _RowSelect; // kiểm tra cột thì _Row chạy
      #region Kiểm tra lên
      while (CheckNext)
      {
        HangCheck--;
        if (HangCheck >= 0 && HangCheck > _RowSelect - 5 )
        {
          CheckNext = CheckObject(HangCheck, _ColumnSelect, StateClick);
          if (CheckNext)
          {
            Value++;
          }
        }
        else
        {
          CheckNext = false;
        }
      }
      #endregion

      HangCheck = _RowSelect; // kiểm tra cột  thì _Row chạy
      CheckNext = true;
      #region Kiểm tra xuống
      while (CheckNext)
      {
        HangCheck++;
        if (HangCheck < _rows && HangCheck < _RowSelect + 5)
        {
          CheckNext = CheckObject(HangCheck, _ColumnSelect, StateClick);
          if (CheckNext)
          {
            Value++;
          }
        }
        else
        {
          CheckNext = false;
        }
      }
      #endregion
      return Value >= 5;
    }

    bool checkmaindiagonal(int _RowSelect, int _ColumnSelect, string StateClick)
    {
      //đường chéo chính  
      int Value = 1;
      bool CheckNext = true;
      int CotCheck = _ColumnSelect;
      int HangCheck = _RowSelect;
      #region Kiểm tra hướng lên bên trái
      while (CheckNext)
      {
        CotCheck--;
        HangCheck--;
        if (CotCheck >= 0 && HangCheck >= 0 && CotCheck > _ColumnSelect-5 && HangCheck> _RowSelect-5)
        {
          CheckNext = CheckObject(HangCheck, CotCheck, StateClick);
          if (CheckNext)
          {
            Value++;
          }
        }
        else
        {
          CheckNext = false;
        }
      }
      #endregion

      CotCheck = _ColumnSelect;
      HangCheck = _RowSelect;
      CheckNext = true;
      #region Kiểm tra xuống bên phải
      while (CheckNext)
      {
        CotCheck++;
        HangCheck++;
        if (CotCheck < _columns  && HangCheck < _rows && CotCheck < _ColumnSelect+5 && HangCheck < _RowSelect+5)
        {
          CheckNext = CheckObject(HangCheck, CotCheck, StateClick);
          if (CheckNext)
          {
            Value++;
          }
        }
        else
        {
          CheckNext = false;
        }
      }
      #endregion
      return Value >= 5;
    }



    bool checkauxiliarydiagonal(int _RowSelect, int _ColumnSelect, string StateClick)
    {
      //đường chéo phụ  
      int Value = 1;
      bool CheckNext = true;
      int CotCheck = _ColumnSelect;
      int HangCheck = _RowSelect;
      #region Kiểm tra hướng lên bên phải
      while (CheckNext)
      {
        CotCheck++;
        HangCheck--;
        if (CotCheck < _columns && HangCheck >= 0 && CotCheck < _ColumnSelect + 5 && HangCheck > _RowSelect - 5)
        {
          CheckNext = CheckObject(HangCheck, CotCheck, StateClick);
          if (CheckNext)
          {
            Value++;
          }
        }
        else
        {
          CheckNext = false;
        }
      }
      #endregion

      CotCheck = _ColumnSelect;
      HangCheck = _RowSelect;
      CheckNext = true;
      #region Kiểm tra xuống bên trái
      while (CheckNext)
      {
        CotCheck--;
        HangCheck++;
        if (CotCheck >=0 && HangCheck < _rows  && CotCheck > _ColumnSelect - 5 && HangCheck < _RowSelect + 5)
        {
          CheckNext = CheckObject(HangCheck, CotCheck, StateClick);
          if (CheckNext)
          {
            Value++;
          }
        }
        else
        {
          CheckNext = false;
        }
      }
      #endregion
      return Value >= 5;
    }


    #region Ver1
    //int indexInList(int _Row, int _Column)
    //{
    //  //_Column là số cột trên bàn cờ
    //  return _Row * _columns + _Column ; ;
    //}
    //bool CheckRow(int _Row, int _Column, string StateClick)
    //{
    //  int Value = 1;
    //  int index = -1;// = indexInList(_Row, _Column);
    //  bool CheckNext = true;
    //  int k = _Column; // kiểm tra dòng thì column chạy
    //    #region Kiểm tra lùi
    //  while (CheckNext)
    //  {
    //    k--;
    //    if (k >= 0 || k > _Column - 5 && _Column - 5 >= 0)
    //    {
    //      index = indexInList(_Row, k);
    //      CheckNext = CheckObject(index, StateClick);
    //      if(CheckNext)
    //      {
    //        Value++;
    //      }  
    //    }
    //    else
    //    {
    //      CheckNext = false;
    //    }
    //  }
    //    #endregion

    //  #region Kiểm tra tiến
    //  k = _Column; // kiểm tra dòng thì column chạy
    //  CheckNext = true;
    //  while (CheckNext)
    //  {
    //    k++;
    //    if (k >= 0 || k > _Column + 5 && _Column + 5 <= _columns)
    //    {
    //      index = indexInList(_Row, k);
    //      CheckNext = CheckObject(index, StateClick);

    //      if (CheckNext)
    //      {
    //        Value++;
    //      }
    //    }
    //    else
    //    {
    //      CheckNext = false;
    //    }
    //  }
    //  #endregion
    //  return Value >= 5;
    //}

    //bool CheckObject(int Index, string StateClick)
    //{
    //  Button btn = lsbtbn[Index];
    //  if (btn.Content != null)
    //  {
    //    if (btn.Content.ToString() == StateClick)
    //    {
    //      return true;
    //    }

    //  }
    //  return false;
    //}
    #endregion
















    private void DrawGameArea()
    {
      bool doneDrawingBackground = false;
      int nextX = 0, nextY = 0;
      int rowCounter = 0;
      bool nextIsOdd = false;

      while (doneDrawingBackground == false)
      {
        Rectangle rect = new Rectangle
        {
          Width = SnakeSquareSize,
          Height = SnakeSquareSize,
          Fill = nextIsOdd ? Brushes.White : Brushes.Black
        };
        GameArea.Children.Add(rect);
        Canvas.SetTop(rect, nextY);
        Canvas.SetLeft(rect, nextX);

        nextIsOdd = !nextIsOdd;
        nextX += SnakeSquareSize;
        if (nextX >= GameArea.ActualWidth)
        {
          nextX = 0;
          nextY += SnakeSquareSize;
          rowCounter++;
          nextIsOdd = (rowCounter % 2 != 0);
        }

        if (nextY >= GameArea.ActualHeight)
          doneDrawingBackground = true;
      }
    }
  }
}
