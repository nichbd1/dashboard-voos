using DashboardVoos.ObjetosVoos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.WindowsAPICodePack.Dialogs;
using ExcelDataReader;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;

namespace DashboardVoos
{
    public partial class DashboardVoos : Form
    {
        bool firstClick;
        List<InformacoesVoos> voos = new List<InformacoesVoos>();
        System.Drawing.Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();
        string pastaSelecionada;

        public DashboardVoos()
        {
            firstClick = true;
            InitializeComponent();
            label2.Visible = false;
            labelFechar.Visible = false;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            importacao.Anchor = AnchorStyles.Bottom;
            importacao.Anchor = AnchorStyles.Bottom;
            buttonImportar.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            GerarGrafico.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            label1.Anchor = AnchorStyles.Top;
            dataFinal.Anchor = AnchorStyles.Top;
            dataInicial.Anchor = AnchorStyles.Top;
            labelDataInicial.Anchor = AnchorStyles.Top;
            labelDataFinal.Anchor = AnchorStyles.Top;
            chart1.Visible = false;
            chart2.Visible = false;
            chart3.Visible = false;
            chart4.Visible = false;
            chart5.Visible = false;
            chart6.Visible = false;
            chart1.Titles.Add("Quantidade de voos por empresa aérea");
            chart2.Titles.Add("Quantidade de voos por dia do mês");
            chart3.Titles.Add("Situação dos voos");
            chart4.Titles.Add("Atraso na decolagem");
            chart5.Titles.Add("Atraso no pouso");
            chart6.Titles.Add("Decolagens por aeroporto");
            backgroundWorker1.WorkerReportsProgress = true;
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
        private void PopularGrafico5(List<InformacoesVoos> voosDentroData)
        {
            foreach (var line in voosDentroData.GroupBy(info => info.atrasoChegada)
                        .Select(group => new
                        {
                            Metric = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Metric))
            {
                chart5.Series["Quantidade de voos"].Points.AddXY(line.Metric, line.Count);
            };
            chart5.ChartAreas["ChartArea1"].AxisX.Interval = 1;
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
            chart3.Series["Situação"]["PieLabelStyle"] = "Disabled";
        }

        private void GerarGrafico_Click(object sender, EventArgs e)
        {
            label2.Visible = true;
            List<InformacoesVoos> voosDentroData = voos.FindAll(x => x.partidaPrevista >= dataInicial.Value && x.partidaPrevista <= dataFinal.Value);
            chart1.Visible = true;
            chart2.Visible = true;
            chart3.Visible = true;
            chart4.Visible = true;
            chart5.Visible = true;
            chart6.Visible = true;
            chart1.Series["Empresa Aérea"].Points.Clear();
            chart2.Series["Dia do mês"].Points.Clear();
            chart3.Series["Situação"].Points.Clear();
            chart4.Series["Quantidade de voos"].Points.Clear();
            chart5.Series["Quantidade de voos"].Points.Clear();
            chart6.Series["Voos por Aeroporto"].Points.Clear();
            PopularGrafico1(voosDentroData);
            PopularGrafico2(voosDentroData);
            PopularGrafico3(voosDentroData);
            PopularGrafico4(voosDentroData);
            PopularGrafico5(voosDentroData);
            PopularGrafico6(voosDentroData);
        }

        private void PopularGrafico6(List<InformacoesVoos> voosDentroData)
        {
            foreach (var line in voosDentroData.GroupBy(info => info.ICAOAerodromoOrigem)
                        .Select(group => new
                        {
                            Metric = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Metric))
            {
                chart6.Series["Voos por Aeroporto"].Points.AddXY(line.Metric, line.Count);
            };
            chart6.Series["Voos por Aeroporto"].ChartType = SeriesChartType.Pie;
            chart6.Series["Voos por Aeroporto"]["PieLabelStyle"] = "Disabled";
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
                    
                    using (ExcelPackage xlPackage = new ExcelPackage(new System.IO.FileInfo(file)))
                    {
                        
                        // get the first worksheet in the workbook
                        ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[0];
                        voos.AddRange(InformacoesVoos.FromXlsx(worksheet));
                    }
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

        private void chart3_Click(object sender, EventArgs e)
        {
            if (firstClick)
            {
                chart3.Dock = DockStyle.Fill;
                chart3.BringToFront();
                firstClick = false;
                labelFechar.Visible = true;
                labelFechar.BringToFront();
            }
            else if (!firstClick)
            {
                chart3.Dock = DockStyle.None;
                chart3.SendToBack();
                firstClick = true;
                labelFechar.Visible = false;
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            if (firstClick)
            {
                chart1.Dock = DockStyle.Fill;
                chart1.BringToFront();
                firstClick = false;
                labelFechar.Visible = true;
                labelFechar.BringToFront();
            }
            else if (!firstClick)
            {
                chart1.Dock = DockStyle.None;
                chart1.SendToBack();
                firstClick = true;
                labelFechar.Visible = false;
            }
        }

        private void chart4_Click(object sender, EventArgs e)
        {
            if (firstClick)
            {
                chart4.Dock = DockStyle.Fill;
                chart4.BringToFront();
                firstClick = false;
                labelFechar.Visible = true;
                labelFechar.BringToFront();
            }
            else if (!firstClick)
            {
                chart4.Dock = DockStyle.None;
                chart4.SendToBack();
                firstClick = true;
                labelFechar.Visible = false;
            }
        }

        private void chart2_Click(object sender, EventArgs e)
        {
            if (firstClick)
            {
                chart2.Dock = DockStyle.Fill;
                chart2.BringToFront();
                firstClick = false;
                labelFechar.Visible = true;
                labelFechar.BringToFront();
            }
            else if (!firstClick)
            {
                chart2.Dock = DockStyle.None;
                chart2.SendToBack();
                firstClick = true;
                labelFechar.Visible = false;
            }
        }

        private void chart5_Click(object sender, EventArgs e)
        {
            if (firstClick)
            {
                chart5.Dock = DockStyle.Fill;
                chart5.BringToFront();
                firstClick = false;
                labelFechar.Visible = true;
                labelFechar.BringToFront();
            }
            else if (!firstClick)
            {
                chart5.Dock = DockStyle.None;
                chart5.SendToBack();
                firstClick = true;
                labelFechar.Visible = false;
            }
        }

        private void chart5_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = chart5.HitTest(pos.X, pos.Y, false,
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
                        tooltip.Show("Quantidade: " + prop.YValues[0], this.chart5, pos.X, pos.Y - 15);
                    }
                }
            }
        }

        private void chart6_Click(object sender, EventArgs e)
        {
            if (firstClick)
            {
                chart6.Dock = DockStyle.Fill;
                chart6.BringToFront();
                firstClick = false;
                labelFechar.Visible = true;
                labelFechar.BringToFront();
            }
            else if (!firstClick)
            {
                chart6.Dock = DockStyle.None;
                chart6.SendToBack();
                firstClick = true;
                labelFechar.Visible = false;
            }
        }

        private void chart6_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = chart6.HitTest(pos.X, pos.Y, false,
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
                        tooltip.Show(prop.AxisLabel + ": " + prop.YValues[0], this.chart6, pos.X, pos.Y - 15);
                    }
                }
            }
        }
    }
}
