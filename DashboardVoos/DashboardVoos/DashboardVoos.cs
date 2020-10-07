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
            comboBox1.Items.Add("Todas");
            comboBox1.SelectedItem = "Todas";
            comboBox1.SelectedText = "Todas";
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
            graficoVoosPorEmpresa.Visible = false;
            graficoDiaDoMes.Visible = false;
            graficoSituacao.Visible = false;
            graficoAtrasoPartida.Visible = false;
            graficoAtrasoChegada.Visible = false;
            graficoVoosPorAeroporto.Visible = false;
            graficoVoosPorEmpresa.Titles.Add("Quantidade de voos por empresa aérea");
            graficoDiaDoMes.Titles.Add("Quantidade de voos por dia do mês");
            graficoSituacao.Titles.Add("Situação dos voos");
            graficoAtrasoPartida.Titles.Add("Atraso na decolagem");
            graficoAtrasoChegada.Titles.Add("Atraso no pouso");
            graficoVoosPorAeroporto.Titles.Add("Decolagens por aeroporto");
            backgroundWorker1.WorkerReportsProgress = true;
        }


        private void buttonImportar_Click(object sender, EventArgs e)
        {
            importacao.Maximum = 100;
            importacao.Step = 1;
            importacao.Value = 0;
            MessageBox.Show(@"Para o arquivo ser aceito pelo dashboard, ele deve cumprir os seguintes critérios:
- Conter as colunas ""ICAO Empresa Aérea"", ""Número Voo"", ""Código Autorização (DI)"", ""Código Tipo Linha"", ""ICAO Aeródromo Origem"", ""ICAO Aeródromo Destino"", ""Partida Prevista"", ""Partida Real"", ""Chegada Prevista"", ""Chegada Real"",""Situação Voo"" e ""Código Justificativa"", nessa ordem. 
- Não é necessário o cabecalho da tabela ter estes nomes, mas é necessário que sejam essas informações.
- Pode conter a coluna opcional ""Data Prevista"", desde que entre ""Partida Prevista"" e ""Partida Real"".
- Deve estar nos formatos "".xlsx"" ou "".csv"".
- Qualquer arquivo que não atenda aos critérios na pasta selecionada cancelará o processo de importação.");

            CommonOpenFileDialog dialogPasta = new CommonOpenFileDialog();
            dialogPasta.InitialDirectory = "C:\\Users";
            dialogPasta.IsFolderPicker = true;
            if (dialogPasta.ShowDialog() == CommonFileDialogResult.Ok)
            {
                pastaSelecionada = dialogPasta.FileName;
            }
            backgroundWorker1.RunWorkerAsync();
            return;
        }

        private void PopularDropdown(List<InformacoesVoos> voos)
        {
            foreach (var line in voos.GroupBy(info => info.ICAOEmpresaAerea)
            .Select(group => new
            {
                Metric = group.Key,
                Count = group.Count()
            })
            .OrderBy(x => x.Metric))
            {
                if (!string.IsNullOrWhiteSpace(line.Metric)) comboBox1.Items.Add(line.Metric);
            };
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            importacao.Value = e.ProgressPercentage;
        }

        private void PopularGraficoVoosPorEmpresa(List<InformacoesVoos> voosDentroData)
        {
            foreach (var line in voosDentroData.GroupBy(info => info.ICAOEmpresaAerea)
                        .Select(group => new
                        {
                            Metric = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Metric))
            {
                graficoVoosPorEmpresa.Series["Empresa Aérea"].Points.AddXY(line.Metric, line.Count);
            };
            graficoVoosPorEmpresa.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        }

        private void PopularGraficoDiaDoMes(List<InformacoesVoos> voosDentroData)
        {
            foreach (var line in voosDentroData.GroupBy(info => info.partidaReal.Day)
                        .Select(group => new
                        {
                            Metric = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Metric))
            {
                graficoDiaDoMes.Series["Dia do mês"].Points.AddXY(line.Metric, line.Count);
            };
            graficoDiaDoMes.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        }
        private void PopularGraficoAtrasoChegada(List<InformacoesVoos> voosDentroData)
        {
            foreach (var line in voosDentroData.GroupBy(info => info.atrasoChegada)
                        .Select(group => new
                        {
                            Metric = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Metric))
            {
                graficoAtrasoChegada.Series["Quantidade de voos"].Points.AddXY(line.Metric, line.Count);
            };
            graficoAtrasoChegada.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        }

        private void PopularGraficoSituacao(List<InformacoesVoos> voosDentroData)
        {

            foreach (var line in voosDentroData.GroupBy(info => info.situacaoVoo)
                        .Select(group => new
                        {
                            Metric = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Metric))
            {
                graficoSituacao.Series["Situação"].Points.AddXY(line.Metric, line.Count);
            };
            graficoSituacao.Series["Situação"].ChartType = SeriesChartType.Pie;
            graficoSituacao.Series["Situação"]["PieLabelStyle"] = "Disabled";
        }

        private void GerarGrafico_Click(object sender, EventArgs e)
        {
            try
            {
                this.UseWaitCursor = true;
                List<InformacoesVoos> voosDentroData = new List<InformacoesVoos>();
                if (dataInicial.Value > dataFinal.Value.AddMinutes(1))
                {
                    throw new Exception("A data inicial selecionada é maior que a data final.");
                }
                else
                {
                    if (voos.Count == 0)
                    {
                        throw new Exception("É necessário importar os dados para gerar os gráficos.");
                    }
                    if (comboBox1.Text != "Todas")
                    {
                        voosDentroData = voos.FindAll(x => x.partidaPrevista >= dataInicial.Value && x.partidaPrevista <= dataFinal.Value && x.ICAOEmpresaAerea == comboBox1.Text);
                    }
                    else
                    {
                        voosDentroData = voos.FindAll(x => x.partidaPrevista >= dataInicial.Value && x.partidaPrevista <= dataFinal.Value);
                    }
                    if (voosDentroData.Count == 0)
                    {
                        throw new Exception("Não existiram voos no período selecionado.");
                    }
                }
                label2.Visible = true;
                graficoVoosPorEmpresa.Visible = true;
                graficoDiaDoMes.Visible = true;
                graficoSituacao.Visible = true;
                graficoAtrasoPartida.Visible = true;
                graficoAtrasoChegada.Visible = true;
                graficoVoosPorAeroporto.Visible = true;
                graficoVoosPorEmpresa.Series["Empresa Aérea"].Points.Clear();
                graficoDiaDoMes.Series["Dia do mês"].Points.Clear();
                graficoSituacao.Series["Situação"].Points.Clear();
                graficoAtrasoPartida.Series["Quantidade de voos"].Points.Clear();
                graficoAtrasoChegada.Series["Quantidade de voos"].Points.Clear();
                graficoVoosPorAeroporto.Series["Voos por Aeroporto"].Points.Clear();
                PopularGraficoVoosPorEmpresa(voosDentroData);
                PopularGraficoDiaDoMes(voosDentroData);
                PopularGraficoSituacao(voosDentroData);
                PopularGraficoAtrasoPartida(voosDentroData);
                PopularGraficoAtrasoChegada(voosDentroData);
                PopularGraficoVoosPorAeroporto(voosDentroData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.UseWaitCursor = false;
        }

        private void PopularGraficoVoosPorAeroporto(List<InformacoesVoos> voosDentroData)
        {
            foreach (var line in voosDentroData.GroupBy(info => info.ICAOAerodromoOrigem)
                        .Select(group => new
                        {
                            Metric = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Metric))
            {
                graficoVoosPorAeroporto.Series["Voos por Aeroporto"].Points.AddXY(line.Metric, line.Count);
            };
            graficoVoosPorAeroporto.Series["Voos por Aeroporto"].ChartType = SeriesChartType.Pie;
            graficoVoosPorAeroporto.Series["Voos por Aeroporto"]["PieLabelStyle"] = "Disabled";
        }

        private void PopularGraficoAtrasoPartida(List<InformacoesVoos> voosDentroData)
        {
            foreach (var line in voosDentroData.GroupBy(info => info.atrasoPartida)
            .Select(group => new
            {
                Metric = group.Key,
                Count = group.Count()
            })
            .OrderBy(x => x.Metric))
            {
                graficoAtrasoPartida.Series["Quantidade de voos"].Points.AddXY(line.Metric, line.Count);
            };
            graficoAtrasoPartida.ChartAreas["ChartArea1"].AxisX.Interval = 1;
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
                throw new Exception("A pasta não foi selecionada.");
            }
            var backgroundWorker = sender as BackgroundWorker;
            if (fileEntries.Length == 0)
            {
                throw new Exception("A pasta selecionada está vazia.");
            }
            foreach (string file in fileEntries)
            {
                try
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
                catch
                {
                    throw new Exception("O arquivo " + file + " não está dentro dos padrões necessários.");
                }
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
                PopularDropdown(voos);
            }
        }

        private void graficoSituacao_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = graficoSituacao.HitTest(pos.X, pos.Y, false,
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
                        tooltip.Show("Quantidade: " + prop.YValues[0], this.graficoSituacao, pos.X, pos.Y - 15);
                    }
                }
            }
        }

        private void graficoDiaDoMes_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = graficoDiaDoMes.HitTest(pos.X, pos.Y, false,
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
                        tooltip.Show("Quantidade: " + prop.YValues[0], this.graficoDiaDoMes, pos.X, pos.Y - 15);
                    }
                }
            }
        }

        private void graficoVoosPorEmpresa_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = graficoVoosPorEmpresa.HitTest(pos.X, pos.Y, false,
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
                        tooltip.Show("Quantidade: " + prop.YValues[0], this.graficoVoosPorEmpresa, pos.X, pos.Y - 15);
                    }
                }
            }
        }

        private void graficoAtrasoPartida_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = graficoAtrasoPartida.HitTest(pos.X, pos.Y, false,
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
                        tooltip.Show("Quantidade: " + prop.YValues[0], this.graficoAtrasoPartida, pos.X, pos.Y - 15);
                    }
                }
            }
        }

        private void graficoSituacao_Click(object sender, EventArgs e)
        {
            var results = graficoSituacao.HitTest(((System.Windows.Forms.MouseEventArgs)e).X, ((System.Windows.Forms.MouseEventArgs)e).Y, false,
                        ChartElementType.LegendItem);
            if (results[0].Object != null && results[0].ChartElementType == ChartElementType.LegendItem)
            {
                var points = graficoSituacao.Series[0].Points;
                foreach (var point in points)
                {
                    if (results[0].Object != null && point.AxisLabel == ((System.Windows.Forms.DataVisualization.Charting.LegendItem)results[0].Object).Name)
                    {
                        point.BorderWidth = 2;
                        point.BorderColor = Color.Black;
                    }
                    else
                    {
                        point.BorderWidth = 0;
                    }
                }

            }
            else
            {
                if (firstClick)
                {
                    graficoSituacao.Dock = DockStyle.Fill;
                    graficoSituacao.BringToFront();
                    firstClick = false;
                    labelFechar.Visible = true;
                    labelFechar.BringToFront();
                }
                else if (!firstClick)
                {
                    graficoSituacao.Dock = DockStyle.None;
                    graficoSituacao.SendToBack();
                    firstClick = true;
                    labelFechar.Visible = false;
                    foreach (var point in graficoSituacao.Series[0].Points)
                    {
                        point.BorderWidth = 0;
                    }
                }
            }
        }

        private void graficoVoosPorEmpresa_Click(object sender, EventArgs e)
        {
            if (firstClick)
            {
                graficoVoosPorEmpresa.Dock = DockStyle.Fill;
                graficoVoosPorEmpresa.BringToFront();
                firstClick = false;
                labelFechar.Visible = true;
                labelFechar.BringToFront();
            }
            else if (!firstClick)
            {
                graficoVoosPorEmpresa.Dock = DockStyle.None;
                graficoVoosPorEmpresa.SendToBack();
                firstClick = true;
                labelFechar.Visible = false;
            }
        }

        private void graficoAtrasoPartida_Click(object sender, EventArgs e)
        {
            if (firstClick)
            {
                graficoAtrasoPartida.Dock = DockStyle.Fill;
                graficoAtrasoPartida.BringToFront();
                firstClick = false;
                labelFechar.Visible = true;
                labelFechar.BringToFront();
            }
            else if (!firstClick)
            {
                graficoAtrasoPartida.Dock = DockStyle.None;
                graficoAtrasoPartida.SendToBack();
                firstClick = true;
                labelFechar.Visible = false;
            }
        }

        private void graficoDiaDoMes_Click(object sender, EventArgs e)
        {
            if (firstClick)
            {
                graficoDiaDoMes.Dock = DockStyle.Fill;
                graficoDiaDoMes.BringToFront();
                firstClick = false;
                labelFechar.Visible = true;
                labelFechar.BringToFront();
            }
            else if (!firstClick)
            {
                graficoDiaDoMes.Dock = DockStyle.None;
                graficoDiaDoMes.SendToBack();
                firstClick = true;
                labelFechar.Visible = false;
            }
        }

        private void graficoAtrasoChegada_Click(object sender, EventArgs e)
        {
            if (firstClick)
            {
                graficoAtrasoChegada.Dock = DockStyle.Fill;
                graficoAtrasoChegada.BringToFront();
                firstClick = false;
                labelFechar.Visible = true;
                labelFechar.BringToFront();
            }
            else if (!firstClick)
            {
                graficoAtrasoChegada.Dock = DockStyle.None;
                graficoAtrasoChegada.SendToBack();
                firstClick = true;
                labelFechar.Visible = false;
            }
        }

        private void graficoAtrasoChegada_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = graficoAtrasoChegada.HitTest(pos.X, pos.Y, false,
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
                        tooltip.Show("Quantidade: " + prop.YValues[0], this.graficoAtrasoChegada, pos.X, pos.Y - 15);
                    }
                }
            }
        }

        private void graficoVoosPorAeroporto_Click(object sender, EventArgs e)
        {
            var results = graficoVoosPorAeroporto.HitTest(((System.Windows.Forms.MouseEventArgs)e).X, ((System.Windows.Forms.MouseEventArgs)e).Y, false,
                        ChartElementType.LegendItem);
            if (results[0].Object != null && results[0].ChartElementType == ChartElementType.LegendItem)
            {
                var points = graficoVoosPorAeroporto.Series[0].Points;
                foreach (var point in points)
                {
                    if (results[0].Object != null && point.AxisLabel == ((System.Windows.Forms.DataVisualization.Charting.LegendItem)results[0].Object).Name)
                    {
                        point.BorderWidth = 2;
                        point.BorderColor = Color.Black;
                    }
                    else
                    {
                        point.BorderWidth = 0;
                    }
                }
            }
            else
            {
                if (firstClick)
                {
                    graficoVoosPorAeroporto.Dock = DockStyle.Fill;
                    graficoVoosPorAeroporto.BringToFront();
                    firstClick = false;
                    labelFechar.Visible = true;
                    labelFechar.BringToFront();
                }
                else if (!firstClick)
                {
                    graficoVoosPorAeroporto.Dock = DockStyle.None;
                    graficoVoosPorAeroporto.SendToBack();
                    firstClick = true;
                    labelFechar.Visible = false;
                }
            }
        }

        private void graficoVoosPorAeroporto_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = graficoVoosPorAeroporto.HitTest(pos.X, pos.Y, false,
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
                        tooltip.Show(prop.AxisLabel + ": " + prop.YValues[0], this.graficoVoosPorAeroporto, pos.X, pos.Y - 15);
                    }
                }
            }
        }
    }
}
