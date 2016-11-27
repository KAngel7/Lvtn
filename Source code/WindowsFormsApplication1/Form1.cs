using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuronNetwork
{
    public partial class Form1 : Form
    {
        StringBuilder listBP;
        StringBuilder listSA;
        StringBuilder listSA2;
        StringBuilder listSA3;
        public Form1()
        {
            InitializeComponent();
        }
        public StringBuilder testing(int a, int b, int c, List<double> sample, double lr, double mm)
        {
            Network ann = new Network(a, b, c);
            StringBuilder result = new StringBuilder();
            List<double> sampleTrain = new List<double>();
            List<double> sampleTest = new List<double>();
            int cutIndex = sample.Count * 9 / 10;
            for (int i = 0; i < cutIndex; i++)
            {
                sampleTrain.Add(sample.ElementAt(i));
            }
            int tt = 0;
            for (int i = cutIndex - a; i < sample.Count; i++)
            {
              //  if (i >= cutIndex)
              //  {
              //      chart1.Series["Observations"].Points.AddXY(tt, sample.ElementAt(i));
              //      Console.WriteLine(sample.ElementAt(i));
               // }
                
                sampleTest.Add(sample.ElementAt(i));
                tt++;
            }

            List<double> sampleTrain01 = ann.to01List(sampleTrain, sample.Max(), sample.Min());
            for (int g = 0; g < 1; g++)
            {
                DateTime oldDate = new DateTime();
                DateTime newDate = new DateTime();
                StringBuilder t = new StringBuilder();
                TimeSpan ts = new TimeSpan();
                
                richTextBox1.AppendText("Training BP LR=" + lr + ", MM=" + mm + " R=1.0E-5.... ");
                richTextBox1.Update();
                oldDate = DateTime.Now;
                t = ann.Bp_run(sampleTrain01, lr, mm, 10000, 0.00001);
                newDate = DateTime.Now;
                string epoch = t.ToString();
                ts = newDate - oldDate;
                t = ann.calErr(sampleTest, sample.Max(), sample.Min());
                listBP.AppendLine(lr + ";" + mm + ";" + epoch + ";" + (ts.Seconds + 1) + ";" + t.ToString());

                richTextBox1.AppendText("Training SA");
                richTextBox1.Update();

                oldDate = DateTime.Now;
                t = ann.Sa_run(sampleTrain01, 9000, 2, 15, 100);
                newDate = DateTime.Now;
                ts = newDate - oldDate;

                result.AppendLine(t.ToString());
                t = ann.calErr(sampleTest, sample.Max(), sample.Min());
                listSA.AppendLine(";;;" + (ts.Seconds+1) + ";" + t.ToString());

                richTextBox1.AppendText("Training new SA");
                richTextBox1.Update();
                oldDate = DateTime.Now;
                t = ann.SA_new_Run(sampleTrain01, false);
                newDate = DateTime.Now;
                ts = newDate - oldDate;
                t = ann.calErr(sampleTest, sample.Max(), sample.Min());
                listSA2.AppendLine(";;;"+(ts.Seconds + 1) + ";" + t.ToString());

                //richtextbox1.appendtext("training new sa - reheat");
                //richtextbox1.update();
                //olddate = datetime.now;
                //t = ann.sa_new_run(sampletrain01, true);
                //newdate = datetime.now;
                //ts = newdate - olddate;
                //t = ann.calerr(sampletest, sample.max(), sample.min());
                //listsa3.appendline(";;;" + (ts.seconds + 1) + ";" + t.tostring());
            }
            
            //List<double> listResult = ann.listResult(sampleTest, sample.Max(), sample.Min());
            ///for (int i = 0; i < listResult.Count;i++ )
            //{
            //    chart1.Series["Forecasts"].Points.AddXY(i + a, listResult.ElementAt(i));
           // }
            return result;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            System.IO.StreamWriter result = new System.IO.StreamWriter("C:/Users/Windows 10 TIMT/Desktop/Result.txt");
            List<string> inputList = new List<string>();
            inputList.Add("C:/Users/Windows 10 TIMT/Desktop/Runoffs/runoff_216_TriAn.txt");
            inputList.Add("C:/Users/Windows 10 TIMT/Desktop/Runoffs/runoff_239_GhenhGa.txt");
            inputList.Add("C:/Users/Windows 10 TIMT/Desktop/Runoffs/Qmax_365_ChauDoc.txt");
            inputList.Add("C:/Users/Windows 10 TIMT/Desktop/Runoffs/runoff_204_PhuocHoa.txt");
            inputList.Add("C:/Users/Windows 10 TIMT/Desktop/Runoffs/runoff_204_PhuocLong.txt");
            inputList.Add("C:/Users/Windows 10 TIMT/Desktop/Runoffs/runoff_239_ChiemHoa.txt");
            inputList.Add("C:/Users/Windows 10 TIMT/Desktop/Runoffs/runoff_4000_BuonHo.txt");
            inputList.Add("C:/Users/Windows 10 TIMT/Desktop/Runoffs/runoff_4000_Cau14.txt");
            inputList.Add("C:/Users/Windows 10 TIMT/Desktop/Runoffs/runoff_3000_DucXuyen.txt");
            Network a = new Network(3, 3, 1);
            int jj = 6;
            bool big = false;
            foreach (string inputPath in inputList)
            {
                System.IO.StreamReader input = new System.IO.StreamReader(inputPath);
                if (jj > 5) big = true;
                jj++;
                richTextBox1.AppendText("Input file: " + inputPath);
                richTextBox1.Update();
                string line = null;
                List<double> sample = new List<double>();
                chart1.Series["Observations"].Color = System.Drawing.Color.Blue;
                chart1.Series["Forecasts"].Color = System.Drawing.Color.Red;
                chart1.Series["Forecasts"].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
                int i = 0;
                while ((line = input.ReadLine()) != null)
                {

                    double num = Double.Parse(line, System.Globalization.CultureInfo.InvariantCulture);
                    sample.Add(num);
                    i++;
                }
                StringBuilder t = new StringBuilder();
                //t = testing(3, 3, 1, sample, 0.1, 0.1);
                //result.WriteLine(t);
                listBP = new StringBuilder();
                listSA = new StringBuilder();
                listSA2 = new StringBuilder();
                listSA3 = new StringBuilder();
                if (big)
                {
                    t = testing(10, 10, 1, sample, 0.1, 0.1);
                    t = testing(10, 10, 1, sample, 0.2, 0.2);
                    t = testing(10, 10, 1, sample, 0.01, 0.3);
                    t = testing(10, 10, 1, sample, 0.3, 0.25);
                    t = testing(10, 10, 1, sample, 0.05, 0.15);

                }
                else
                {
                    t = testing(6, 6, 1, sample, 0.1, 0.05);
                    t = testing(6, 6, 1, sample, 0.2, 0.1);
                    t = testing(6, 6, 1, sample, 0.01, 0.01);
                    t = testing(6, 6, 1, sample, 0.3, 0.15);
                    t = testing(6, 6, 1, sample, 0.05, 0.015);

                    //t = testing(10, 10, 1, sample, 0.1, 0.1);
                    //t = testing(10, 10, 1, sample, 0.2, 0.2);
                    //t = testing(10, 10, 1, sample, 0.01, 0.3);
                    //t = testing(10, 10, 1, sample, 0.3, 0.25);
                    //t = testing(10, 10, 1, sample, 0.05, 0.15);

                }
                result.WriteLine("LearnRate;Momen;Epochs;Time;MAE;MAPE;SSE;MSE");
                result.WriteLine(listBP);
                result.WriteLine(";;;Time;MAE;MAPE;SSE;MSE");
                result.WriteLine(listSA);
                result.WriteLine(";;;Time;MAE;MAPE;SSE;MSE");
                result.WriteLine(listSA2);
                result.WriteLine(";;;Time;MAE;MAPE;SSE;MSE");
                result.WriteLine(listSA3);
              //  t = testing(10, 10, 1, sample, 0.1, 0.1);
               // result.WriteLine(t);
                richTextBox1.Clear();
                input.Close();
            }
            textBox1.Text = "Done!";
            result.Close();            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
