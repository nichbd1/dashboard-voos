namespace DashboardVoos
{
    partial class DashboardVoos
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonImportar = new System.Windows.Forms.Button();
            this.LabelResult = new System.Windows.Forms.Label();
            this.graficoVoosPorEmpresa = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataInicial = new System.Windows.Forms.DateTimePicker();
            this.dataFinal = new System.Windows.Forms.DateTimePicker();
            this.labelDataInicial = new System.Windows.Forms.Label();
            this.labelDataFinal = new System.Windows.Forms.Label();
            this.GerarGrafico = new System.Windows.Forms.Button();
            this.importacao = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.graficoDiaDoMes = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.graficoSituacao = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.graficoAtrasoPartida = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label2 = new System.Windows.Forms.Label();
            this.labelFechar = new System.Windows.Forms.Label();
            this.graficoAtrasoChegada = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.graficoVoosPorAeroporto = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.graficoVoosPorEmpresa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.graficoDiaDoMes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.graficoSituacao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.graficoAtrasoPartida)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.graficoAtrasoChegada)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.graficoVoosPorAeroporto)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(327, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(829, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Para inicializar a criação do dashboard, clique no botão Importar e selecione a p" +
    "asta onde os arquivos se encontram.";
            // 
            // buttonImportar
            // 
            this.buttonImportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonImportar.Location = new System.Drawing.Point(16, 792);
            this.buttonImportar.Name = "buttonImportar";
            this.buttonImportar.Size = new System.Drawing.Size(100, 23);
            this.buttonImportar.TabIndex = 1;
            this.buttonImportar.Text = "Importar";
            this.buttonImportar.UseVisualStyleBackColor = true;
            this.buttonImportar.Click += new System.EventHandler(this.buttonImportar_Click);
            // 
            // LabelResult
            // 
            this.LabelResult.AutoSize = true;
            this.LabelResult.Location = new System.Drawing.Point(46, 533);
            this.LabelResult.Name = "LabelResult";
            this.LabelResult.Size = new System.Drawing.Size(0, 13);
            this.LabelResult.TabIndex = 2;
            // 
            // graficoVoosPorEmpresa
            // 
            chartArea1.Name = "ChartArea1";
            this.graficoVoosPorEmpresa.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.graficoVoosPorEmpresa.Legends.Add(legend1);
            this.graficoVoosPorEmpresa.Location = new System.Drawing.Point(1060, 465);
            this.graficoVoosPorEmpresa.Name = "graficoVoosPorEmpresa";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Empresa Aérea";
            this.graficoVoosPorEmpresa.Series.Add(series1);
            this.graficoVoosPorEmpresa.Size = new System.Drawing.Size(457, 286);
            this.graficoVoosPorEmpresa.TabIndex = 3;
            this.graficoVoosPorEmpresa.Text = "Gráfico de voos por empresa";
            this.graficoVoosPorEmpresa.Click += new System.EventHandler(this.graficoVoosPorEmpresa_Click);
            this.graficoVoosPorEmpresa.MouseMove += new System.Windows.Forms.MouseEventHandler(this.graficoVoosPorEmpresa_MouseMove);
            // 
            // dataInicial
            // 
            this.dataInicial.Location = new System.Drawing.Point(303, 52);
            this.dataInicial.Name = "dataInicial";
            this.dataInicial.Size = new System.Drawing.Size(231, 20);
            this.dataInicial.TabIndex = 4;
            // 
            // dataFinal
            // 
            this.dataFinal.Location = new System.Drawing.Point(640, 52);
            this.dataFinal.Name = "dataFinal";
            this.dataFinal.Size = new System.Drawing.Size(228, 20);
            this.dataFinal.TabIndex = 5;
            // 
            // labelDataInicial
            // 
            this.labelDataInicial.AutoSize = true;
            this.labelDataInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.labelDataInicial.Location = new System.Drawing.Point(216, 53);
            this.labelDataInicial.Name = "labelDataInicial";
            this.labelDataInicial.Size = new System.Drawing.Size(83, 18);
            this.labelDataInicial.TabIndex = 6;
            this.labelDataInicial.Text = "Data Inicial:";
            // 
            // labelDataFinal
            // 
            this.labelDataFinal.AutoSize = true;
            this.labelDataFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.labelDataFinal.Location = new System.Drawing.Point(558, 53);
            this.labelDataFinal.Name = "labelDataFinal";
            this.labelDataFinal.Size = new System.Drawing.Size(78, 18);
            this.labelDataFinal.TabIndex = 7;
            this.labelDataFinal.Text = "Data Final:";
            // 
            // GerarGrafico
            // 
            this.GerarGrafico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GerarGrafico.Location = new System.Drawing.Point(1195, 52);
            this.GerarGrafico.Name = "GerarGrafico";
            this.GerarGrafico.Size = new System.Drawing.Size(106, 21);
            this.GerarGrafico.TabIndex = 8;
            this.GerarGrafico.Text = "Gerar Gráficos";
            this.GerarGrafico.UseVisualStyleBackColor = true;
            this.GerarGrafico.Click += new System.EventHandler(this.GerarGrafico_Click);
            // 
            // importacao
            // 
            this.importacao.Location = new System.Drawing.Point(122, 792);
            this.importacao.Name = "importacao";
            this.importacao.Size = new System.Drawing.Size(1395, 23);
            this.importacao.TabIndex = 9;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // graficoDiaDoMes
            // 
            chartArea2.Name = "ChartArea1";
            this.graficoDiaDoMes.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.graficoDiaDoMes.Legends.Add(legend2);
            this.graficoDiaDoMes.Location = new System.Drawing.Point(63, 465);
            this.graficoDiaDoMes.Name = "graficoDiaDoMes";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Dia do mês";
            this.graficoDiaDoMes.Series.Add(series2);
            this.graficoDiaDoMes.Size = new System.Drawing.Size(511, 286);
            this.graficoDiaDoMes.TabIndex = 11;
            this.graficoDiaDoMes.Text = "chart2";
            this.graficoDiaDoMes.Click += new System.EventHandler(this.graficoDiaDoMes_Click);
            this.graficoDiaDoMes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.graficoDiaDoMes_MouseMove);
            // 
            // graficoSituacao
            // 
            chartArea3.Name = "ChartArea1";
            this.graficoSituacao.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.graficoSituacao.Legends.Add(legend3);
            this.graficoSituacao.Location = new System.Drawing.Point(1060, 118);
            this.graficoSituacao.Name = "graficoSituacao";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Situação";
            this.graficoSituacao.Series.Add(series3);
            this.graficoSituacao.Size = new System.Drawing.Size(457, 294);
            this.graficoSituacao.TabIndex = 12;
            this.graficoSituacao.Text = "chart3";
            this.graficoSituacao.Click += new System.EventHandler(this.graficoSituacao_Click);
            this.graficoSituacao.MouseMove += new System.Windows.Forms.MouseEventHandler(this.graficoSituacao_MouseMove);
            // 
            // graficoAtrasoPartida
            // 
            chartArea4.Name = "ChartArea1";
            this.graficoAtrasoPartida.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.graficoAtrasoPartida.Legends.Add(legend4);
            this.graficoAtrasoPartida.Location = new System.Drawing.Point(63, 118);
            this.graficoAtrasoPartida.Name = "graficoAtrasoPartida";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Quantidade de voos";
            this.graficoAtrasoPartida.Series.Add(series4);
            this.graficoAtrasoPartida.Size = new System.Drawing.Size(511, 294);
            this.graficoAtrasoPartida.TabIndex = 13;
            this.graficoAtrasoPartida.Text = "chart4";
            this.graficoAtrasoPartida.Click += new System.EventHandler(this.graficoAtrasoPartida_Click);
            this.graficoAtrasoPartida.MouseMove += new System.Windows.Forms.MouseEventHandler(this.graficoAtrasoPartida_MouseMove);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(544, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(477, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "Clique na parte externa dos gráficos para aumentar a visualização.";
            // 
            // labelFechar
            // 
            this.labelFechar.AutoSize = true;
            this.labelFechar.BackColor = System.Drawing.SystemColors.Window;
            this.labelFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFechar.Location = new System.Drawing.Point(586, 0);
            this.labelFechar.Name = "labelFechar";
            this.labelFechar.Size = new System.Drawing.Size(399, 18);
            this.labelFechar.TabIndex = 15;
            this.labelFechar.Text = "Para voltar ao dashboard, clique na área externa do gráfico.";
            // 
            // graficoAtrasoChegada
            // 
            chartArea5.Name = "ChartArea1";
            this.graficoAtrasoChegada.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.graficoAtrasoChegada.Legends.Add(legend5);
            this.graficoAtrasoChegada.Location = new System.Drawing.Point(589, 465);
            this.graficoAtrasoChegada.Name = "graficoAtrasoChegada";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Quantidade de voos";
            this.graficoAtrasoChegada.Series.Add(series5);
            this.graficoAtrasoChegada.Size = new System.Drawing.Size(448, 286);
            this.graficoAtrasoChegada.TabIndex = 16;
            this.graficoAtrasoChegada.Text = "chart5";
            this.graficoAtrasoChegada.Click += new System.EventHandler(this.graficoAtrasoChegada_Click);
            this.graficoAtrasoChegada.MouseMove += new System.Windows.Forms.MouseEventHandler(this.graficoAtrasoChegada_MouseMove);
            // 
            // graficoVoosPorAeroporto
            // 
            chartArea6.Name = "ChartArea1";
            this.graficoVoosPorAeroporto.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.graficoVoosPorAeroporto.Legends.Add(legend6);
            this.graficoVoosPorAeroporto.Location = new System.Drawing.Point(589, 118);
            this.graficoVoosPorAeroporto.Name = "graficoVoosPorAeroporto";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Voos por Aeroporto";
            this.graficoVoosPorAeroporto.Series.Add(series6);
            this.graficoVoosPorAeroporto.Size = new System.Drawing.Size(448, 294);
            this.graficoVoosPorAeroporto.TabIndex = 17;
            this.graficoVoosPorAeroporto.Text = "chart6";
            this.graficoVoosPorAeroporto.Click += new System.EventHandler(this.graficoVoosPorAeroporto_Click);
            this.graficoVoosPorAeroporto.MouseMove += new System.Windows.Forms.MouseEventHandler(this.graficoVoosPorAeroporto_MouseMove);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(1044, 52);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label3.Location = new System.Drawing.Point(885, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 18);
            this.label3.TabIndex = 19;
            this.label3.Text = "ICAO Empresa Aérea:";
            // 
            // DashboardVoos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1546, 827);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.graficoVoosPorAeroporto);
            this.Controls.Add(this.graficoAtrasoChegada);
            this.Controls.Add(this.labelFechar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.graficoAtrasoPartida);
            this.Controls.Add(this.graficoSituacao);
            this.Controls.Add(this.graficoDiaDoMes);
            this.Controls.Add(this.importacao);
            this.Controls.Add(this.GerarGrafico);
            this.Controls.Add(this.labelDataFinal);
            this.Controls.Add(this.labelDataInicial);
            this.Controls.Add(this.dataFinal);
            this.Controls.Add(this.dataInicial);
            this.Controls.Add(this.graficoVoosPorEmpresa);
            this.Controls.Add(this.LabelResult);
            this.Controls.Add(this.buttonImportar);
            this.Controls.Add(this.label1);
            this.Name = "DashboardVoos";
            this.Text = "Dashboard Voos";
            ((System.ComponentModel.ISupportInitialize)(this.graficoVoosPorEmpresa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.graficoDiaDoMes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.graficoSituacao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.graficoAtrasoPartida)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.graficoAtrasoChegada)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.graficoVoosPorAeroporto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonImportar;
        private System.Windows.Forms.Label LabelResult;
        private System.Windows.Forms.DataVisualization.Charting.Chart graficoVoosPorEmpresa;
        private System.Windows.Forms.DateTimePicker dataInicial;
        private System.Windows.Forms.DateTimePicker dataFinal;
        private System.Windows.Forms.Label labelDataInicial;
        private System.Windows.Forms.Label labelDataFinal;
        private System.Windows.Forms.Button GerarGrafico;
        private System.Windows.Forms.ProgressBar importacao;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataVisualization.Charting.Chart graficoDiaDoMes;
        private System.Windows.Forms.DataVisualization.Charting.Chart graficoSituacao;
        private System.Windows.Forms.DataVisualization.Charting.Chart graficoAtrasoPartida;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelFechar;
        private System.Windows.Forms.DataVisualization.Charting.Chart graficoAtrasoChegada;
        private System.Windows.Forms.DataVisualization.Charting.Chart graficoVoosPorAeroporto;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
    }
}

