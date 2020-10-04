using DashboardVoos.ObjetosVoos;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace DashboardVoos
{
    public partial class DashboardVoos : Form
    {
        List<InformacoesVoos> voos = new List<InformacoesVoos>();
        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();
        string pastaSelecionada;
        public DashboardVoos()
        {
            InitializeComponent();
            chart1.Visible = false;
            chart2.Visible = false;
            chart3.Visible = false;
            PopularDropdownGraficos();
            backgroundWorker1.WorkerReportsProgress = true;
        }

        private void PopularDropdownGraficos()
        {
            comboBox1.DisplayMember = "nomeGrafico";
            comboBox1.ValueMember = "idGrafico";
            comboBox1.Items.Add("Quantidade de voos por empresa");
            comboBox1.Items.Add("Quantidade de voos por dia do mês");
            comboBox1.Items.Add("Quantidade de voos por situação");
            comboBox1.Items.Add("Quantidade de voos atrasados");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonImportar_Click(object sender, EventArgs e)
        {
            importacao.Maximum = 100;
            importacao.Step = 1;
            importacao.Value = 0;
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                pastaSelecionada = dialog.FileName;
            }
            backgroundWorker1.RunWorkerAsync();
            LabelResult.ForeColor = Color.Green;
            chart1.Titles.Add("Quantidade de voos por período");
            return;
        }


        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            importacao.Value = e.ProgressPercentage;
        }

        private void PopularGrafico1(List<InformacoesVoos> voosDentroData)
        {
            foreach (var line in voosDentroData.GroupBy(info => info.ICAOEmpresaAerea)
                        .Select(group => new
                        {
                            Metric = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Metric))
            {
                chart1.Series["Empresa Aérea"].Points.AddXY(line.Metric, line.Count);
            };
            chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        }

        private void PopularGrafico2(List<InformacoesVoos> voosDentroData)
        {
            foreach (var line in voosDentroData.GroupBy(info => info.partidaReal.Day)
                        .Select(group => new
                        {
                            Metric = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Metric))
            {
                chart2.Series["Dia do mês"].Points.AddXY(line.Metric, line.Count);
            };
            chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        }

        private void PopularGrafico3(List<InformacoesVoos> voosDentroData)
        {

            foreach (var line in voosDentroData.GroupBy(info => info.situacaoVoo)
                        .Select(group => new
                        {
                            Metric = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Metric))
            {
                chart3.Series["Situação"].Points.AddXY(line.Metric, line.Count);
            };
            chart3.Series["Situação"].ChartType = SeriesChartType.Pie;
        }

        private void GerarGrafico_Click(object sender, EventArgs e)
        {
            List<InformacoesVoos> voosDentroData = voos.FindAll(x => x.partidaPrevista >= dataInicial.Value && x.partidaPrevista <= dataFinal.Value);
            chart1.Series["Empresa Aérea"].Points.Clear();
            chart2.Series["Dia do mês"].Points.Clear();
            chart3.Series["Situação"].Points.Clear();
            chart4.Series["Quantidade de voos"].Points.Clear();
            PopularGrafico1(voosDentroData);
            PopularGrafico2(voosDentroData);
            PopularGrafico3(voosDentroData);
            PopularGrafico4(voosDentroData);
        }

        private void PopularGrafico4(List<InformacoesVoos> voosDentroData)
        {
            foreach (var line in voosDentroData.GroupBy(info => info.atrasoPartida)
            .Select(group => new
            {
                Metric = group.Key,
                Count = group.Count()
            })
            .OrderBy(x => x.Metric))
            {
                chart4.Series["Quantidade de voos"].Points.AddXY(line.Metric, line.Count);
            };
            chart4.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;

            string[] fileEntries = null;
            if (Directory.Exists(pastaSelecionada))
            {
                fileEntries = Directory.GetFiles(pastaSelecionada);
            }
            else
            {
                LabelResult.ForeColor = Color.Green;
                LabelResult.Text = "A pasta requisitada não foi criada.";
                return;
            }
            var backgroundWorker = sender as BackgroundWorker;
            foreach (string file in fileEntries)
            {
                i++;
                if (file.EndsWith(".csv"))
                {
                    voos.AddRange(File.ReadAllLines(file)
                                .Skip(1)
                                .Select(v => InformacoesVoos.FromCsv(v))
                                .ToList());
                }
                else if (file.EndsWith(".xlsx"))
                {

                }
                backgroundWorker.ReportProgress((i * 100) / fileEntries.Length);

            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                System.Windows.Forms.MessageBox.Show(e.Error.Message);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Importação executada com sucesso.");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Quantidade de voos por empresa":
                    chart1.Visible = true;
                    chart2.Visible = false;
                    chart3.Visible = false;
                    chart4.Visible = false;
                    break;
                case "Quantidade de voos por dia do mês":
                    chart1.Visible = false;
                    chart2.Visible = true;
                    chart3.Visible = false;
                    chart4.Visible = false;
                    break;
                case "Quantidade de voos por situação":
                    chart1.Visible = false;
                    chart2.Visible = false;
                    chart3.Visible = true;
                    chart4.Visible = false;
                    break;
                case "Quantidade de voos atrasados":
                    chart1.Visible = false;
                    chart2.Visible = false;
                    chart3.Visible = false;
                    chart4.Visible = true;
                    break;
            }
        }

        private void chart3_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = chart3.HitTest(pos.X, pos.Y, false,
                                            ChartElementType.DataPoint);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                        var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);
                        tooltip.Show("Quantidade: " + prop.YValues[0], this.chart3, pos.X, pos.Y - 15);
                    }
                }
            }
        }

        private void chart2_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = chart2.HitTest(pos.X, pos.Y, false,
                                            ChartElementType.DataPoint);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                        var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);
                        tooltip.Show("Quantidade: " + prop.YValues[0], this.chart2, pos.X, pos.Y - 15);
                    }
                }
            }
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = chart1.HitTest(pos.X, pos.Y, false,
                                            ChartElementType.DataPoint);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                        var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);
                        tooltip.Show("Quantidade: " + prop.YValues[0], this.chart1, pos.X, pos.Y - 15);
                    }
                }
            }
        }

        private void chart4_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = chart4.HitTest(pos.X, pos.Y, false,
                                            ChartElementType.DataPoint);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                        var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);
                        tooltip.Show("Quantidade: " + prop.YValues[0], this.chart4, pos.X, pos.Y - 15);
                    }
                }
            }
        }
    }
}
