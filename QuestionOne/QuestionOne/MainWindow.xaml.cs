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
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace QuestionOne
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "C:\\Users\\austi\\OneDrive\\Documents\\UDM_TechnicalAssessment\\TechnicalAssesmentData";

            //connect to excel
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelBook = excelApp.Workbooks.Open(filePath, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Excel.Worksheet excelSheet = (Excel.Worksheet)excelBook.Worksheets.get_Item(2); ;
            Excel.Range excelRange = excelSheet.UsedRange;

            //variables
            string strCellData = "";
            double douCellData;
            int rowCnt = 0;
            int colCnt = 0;

            PasswordBox passwordBox = new PasswordBox();
            string name = FirstName.Text;
            string surname = Surname.Text;
            string password = passwordBox.Password.ToString();

            //create table to store data
            DataTable dt = new DataTable();
            for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
            {
                string strColumn = "";
                strColumn = (string)(excelRange.Cells[1, colCnt] as Excel.Range).Value2;
                dt.Columns.Add(strColumn, typeof(string));
            }

            for (rowCnt = 2; rowCnt <= excelRange.Rows.Count; rowCnt++)
            {
                string strData = "";
                for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
                {
                    try
                    {
                        strCellData = (string)(excelRange.Cells[rowCnt, colCnt] as Excel.Range).Value2;
                        strData += strCellData + "|";
                    }
                    catch (Exception ex)
                    {
                        douCellData = (excelRange.Cells[rowCnt, colCnt] as Excel.Range).Value2;
                        strData += douCellData.ToString() + "|";
                    }

                }
                strData = strData.Remove(strData.Length - 1, 1);
                dt.Rows.Add(strData.Split('|'));

                //compare data to user input
                DataColumn[] columns = dt.Columns.Cast<DataColumn>().ToArray();
                bool nm = dt.AsEnumerable().Any(row => columns.Any(col => row[col].ToString() == name));
                bool sm = dt.AsEnumerable().Any(row => columns.Any(col => row[col].ToString() == surname));
                //bool pw = dt.AsEnumerable().Any(row => columns.Any(col => row[col].ToString() == password));
                if (nm && sm)// && pw)
                {
                    Menu menu = new Menu();
                    menu.Owner = this;
                    this.Hide();
                    menu.Show();
                } else
                {
                    errorMessage.Opacity = 1;
                }
            }

            excelBook.Close(true, null, null);
            excelApp.Quit();
        }
    }

}

