namespace excel_feladaz;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

    public partial class Form1 : Form
    {
    Excel.Application xlApp; 
    Excel.Workbook xlWB;     
    Excel.Worksheet xlSheet; 
    public Form1()
        {
            InitializeComponent();
        try
        {
            
            xlApp = new Excel.Application();

            
            xlWB = xlApp.Workbooks.Add(Missing.Value);

            
            xlSheet = xlWB.ActiveSheet;


            CreateTable();


            xlApp.Visible = true;
            xlApp.UserControl = true;
        }
        catch (Exception ex)
        { 
            string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
            MessageBox.Show(errMsg, "Error");

            xlWB.Close(false, Type.Missing, Type.Missing);
            xlApp.Quit();
            xlWB = null;
            xlApp = null;
        }
        void CreateTable()
        {
            string[] fejlécek = new string[] {
        "Kérdés",
        "1. válasz",
        "2. válaszl",
        "3. válasz",
        "Helyes válasz",
        "kép"};
           Models.HajosContext hajosContext = new Models.HajosContext();
           var mindenKérdés = hajosContext.Questions.ToList();


            object[,] adatTömb = new object[mindenKérdés.Count(), fejlécek.Count()];

            for (int i = 0; i < mindenKérdés.Count(); i++)
            {
                adatTömb[i, 0] = mindenKérdés[i].Question1;
                adatTömb[i, 1] = mindenKérdés[i].Answer1;
                adatTömb[i, 2] = mindenKérdés[i].Answer2;
                adatTömb[i, 3] = mindenKérdés[i].Answer3;
                adatTömb[i, 4] = mindenKérdés[i].CorrectAnswer;
                adatTömb[i, 5] = mindenKérdés[i].Image;
            }

            int sorokSzáma = adatTömb.GetLength(0);
            int oszlopokSzáma = adatTömb.GetLength(1);

            Excel.Range adatRange = xlSheet.get_Range("A2", Type.Missing).get_Resize(sorokSzáma, oszlopokSzáma);
            adatRange.Value2 = adatTömb;

            Excel.Range fejllécRange = xlSheet.get_Range("A1", Type.Missing).get_Resize(1, 6);
            fejllécRange.Font.Bold = true;
            fejllécRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            fejllécRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            fejllécRange.EntireColumn.AutoFit();
            fejllécRange.RowHeight = 40;
            fejllécRange.Interior.Color = Color.Fuchsia;
            fejllécRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);
        }

    }
    }
