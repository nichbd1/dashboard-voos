using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardVoos.ObjetosVoos
{
    class InformacoesVoos
    {
        public string ICAOEmpresaAerea { get; set; }
        public string numeroVoo { get; set; }
        public string codigoDI { get; set; }
        public string codigoTipoLinha { get; set; }
        public string ICAOAerodromoOrigem { get; set; }
        public string ICAOAerodromoDestino { get; set; }
        public DateTime partidaPrevista { get; set; }
        public string dataPrevista { get; set; }
        public DateTime partidaReal { get; set; }
        public DateTime chegadaPrevista { get; set; }
        public DateTime chegadaReal { get; set; }
        public string situacaoVoo { get; set; }
        public string codigoJustificativa { get; set; }
        public string atrasoPartida { get; set; }
        public string atrasoChegada { get; set; }



        public static List<InformacoesVoos> FromXlsx(ExcelWorksheet arquivo)
        {
            try
            {
                int i = 2;
                List<InformacoesVoos> listaInformacoes = new List<InformacoesVoos>();
                while (arquivo.GetValue(i, 1) != null)
                {
                    InformacoesVoos informacoes = new InformacoesVoos();
                    informacoes.ICAOEmpresaAerea = arquivo.GetValue(i, 1).ToString();
                    informacoes.numeroVoo = arquivo.GetValue(i, 2).ToString();
                    informacoes.codigoDI = arquivo.GetValue(i, 3).ToString();
                    informacoes.codigoTipoLinha = arquivo.GetValue(i, 4).ToString();
                    informacoes.ICAOAerodromoOrigem = arquivo.GetValue(i, 5).ToString();
                    informacoes.ICAOAerodromoDestino = arquivo.GetValue(i, 6).ToString();

                    if (!string.IsNullOrWhiteSpace(Convert.ToString(arquivo.GetValue(i, 7))))
                    {
                        informacoes.partidaPrevista = Convert.ToDateTime(arquivo.GetValue(i, 7));
                    }

                    if (!string.IsNullOrWhiteSpace(Convert.ToString(arquivo.GetValue(i, 8))))
                    {
                        informacoes.partidaReal = Convert.ToDateTime(arquivo.GetValue(i, 8));
                    }
                    else
                    {
                        informacoes.atrasoPartida = "Cancelado";
                        informacoes.atrasoChegada = "Cancelado";
                    }

                    if (!string.IsNullOrWhiteSpace(Convert.ToString(arquivo.GetValue(i, 9))))
                    {
                        informacoes.chegadaPrevista = Convert.ToDateTime(arquivo.GetValue(i, 9));
                    }

                    if (!string.IsNullOrWhiteSpace(Convert.ToString(arquivo.GetValue(i, 10))))
                    {
                        informacoes.chegadaReal = Convert.ToDateTime(arquivo.GetValue(i, 10));
                    }

                    informacoes.situacaoVoo = arquivo.GetValue(i, 11).ToString();
                    informacoes.codigoJustificativa = Convert.ToString(arquivo.GetValue(i, 12));

                    if (informacoes.atrasoPartida != "Cancelado")
                    {
                        TimeSpan tempoDeAtrasoPartida = informacoes.partidaReal.Subtract(informacoes.partidaPrevista);
                        informacoes.atrasoPartida = VerificaAtraso(tempoDeAtrasoPartida, informacoes);
                    }
                    if (informacoes.atrasoChegada != "Cancelado")
                    {
                        TimeSpan tempoDeAtrasoChegada = informacoes.chegadaReal.Subtract(informacoes.chegadaPrevista);
                        informacoes.atrasoChegada = VerificaAtraso(tempoDeAtrasoChegada, informacoes);
                    }

                    i++;
                    listaInformacoes.Add(informacoes);
                }
                return listaInformacoes;
            }
            catch
            {
                throw new Exception("O arquivo não está dentro dos padrões necessários.");
            }
        }
        public static InformacoesVoos FromCsv(string csvLine)
        {
            try
            {
                int colunaExtra = 0;
                string[] values = null;
                InformacoesVoos informacoes = new InformacoesVoos();
                if (csvLine[0] == '"')
                {
                    if (Char.IsLetterOrDigit(csvLine[5]))
                    {
                        return informacoes;
                    }
                    values = csvLine.Split(csvLine[5]);
                    informacoes.ICAOEmpresaAerea = Convert.ToString(values[0].Substring(1, values[0].Length - 2));
                    informacoes.numeroVoo = Convert.ToString(values[1].Substring(1, values[1].Length - 2));
                    informacoes.codigoDI = Convert.ToString(values[2].Substring(1, values[2].Length - 2));
                    informacoes.codigoTipoLinha = Convert.ToString(values[3].Substring(1, values[3].Length - 2));
                    informacoes.ICAOAerodromoOrigem = Convert.ToString(values[4].Substring(1, values[4].Length - 2));
                    informacoes.ICAOAerodromoDestino = Convert.ToString(values[5].Substring(1, values[5].Length - 2));
                    if (!string.IsNullOrWhiteSpace(values[6].Substring(1, values[6].Length - 2)))
                    {
                        informacoes.partidaPrevista = Convert.ToDateTime(values[6].Substring(1, values[6].Length - 2));
                    }
                    if (values.Length > 12)
                    {
                        colunaExtra++;
                        if (!string.IsNullOrWhiteSpace(values[7].Substring(1, values[7].Length - 2)))
                        {
                            informacoes.dataPrevista = values[7].Substring(1, values[7].Length - 2);
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(values[7 + colunaExtra].Substring(1, values[7 + colunaExtra].Length - 2)))
                    {
                        informacoes.partidaReal = Convert.ToDateTime(values[7 + colunaExtra].Substring(1, values[7 + colunaExtra].Length - 2));
                    }
                    else
                    {
                        informacoes.atrasoPartida = "Cancelado";
                        informacoes.atrasoChegada = "Cancelado";
                    }
                    if (!string.IsNullOrWhiteSpace(values[8 + colunaExtra].Substring(1, values[8 + colunaExtra].Length - 2)))
                    {
                        informacoes.chegadaPrevista = Convert.ToDateTime(values[8 + colunaExtra].Substring(1, values[8 + colunaExtra].Length - 2));
                    }
                    if (!string.IsNullOrWhiteSpace(values[9 + colunaExtra].Substring(1, values[9 + colunaExtra].Length - 2)))
                    {
                        informacoes.chegadaReal = Convert.ToDateTime(values[9 + colunaExtra].Substring(1, values[9 + colunaExtra].Length - 2));
                    }
                    informacoes.situacaoVoo = Convert.ToString(values[10 + colunaExtra].Substring(1, values[10 + colunaExtra].Length - 2)).ToUpper();
                    informacoes.codigoJustificativa = Convert.ToString(values[11 + colunaExtra].Substring(1, values[11 + colunaExtra].Length - 2));
                    if (informacoes.atrasoPartida != "Cancelado")
                    {
                        TimeSpan tempoDeAtrasoPartida = informacoes.partidaReal.Subtract(informacoes.partidaPrevista);
                        informacoes.atrasoPartida = VerificaAtraso(tempoDeAtrasoPartida, informacoes);
                    }
                    if (informacoes.atrasoChegada != "Cancelado")
                    {
                        TimeSpan tempoDeAtrasoChegada = informacoes.chegadaReal.Subtract(informacoes.chegadaPrevista);
                        informacoes.atrasoChegada = VerificaAtraso(tempoDeAtrasoChegada, informacoes);
                    }
                }
                else
                {
                    if (Char.IsLetterOrDigit(csvLine[3]))
                    {
                        return informacoes;
                    }
                    values = csvLine.Split(csvLine[3]);
                    informacoes.ICAOEmpresaAerea = Convert.ToString(values[0]);
                    informacoes.numeroVoo = Convert.ToString(values[1]);
                    informacoes.codigoDI = Convert.ToString(values[2]);
                    informacoes.codigoTipoLinha = Convert.ToString(values[3]);
                    informacoes.ICAOAerodromoOrigem = Convert.ToString(values[4]);
                    informacoes.ICAOAerodromoDestino = Convert.ToString(values[5]);
                    if (!string.IsNullOrWhiteSpace(values[6]))
                    {
                        informacoes.partidaPrevista = Convert.ToDateTime(values[6]);
                    }
                    if (values.Length > 12)
                    {
                        colunaExtra++;
                        informacoes.dataPrevista = values[7];
                    }
                    if (!string.IsNullOrWhiteSpace(values[7 + colunaExtra]))
                    {
                        informacoes.partidaReal = Convert.ToDateTime(values[7 + colunaExtra]);
                    }
                    else
                    {
                        informacoes.atrasoPartida = "Cancelado";
                        informacoes.atrasoChegada = "Cancelado";
                    }
                    if (!string.IsNullOrWhiteSpace(values[8 + colunaExtra]))
                    {
                        informacoes.chegadaPrevista = Convert.ToDateTime(values[8 + colunaExtra]);
                    }
                    if (!string.IsNullOrWhiteSpace(values[9 + colunaExtra]))
                    {
                        informacoes.chegadaReal = Convert.ToDateTime(values[9 + colunaExtra]);
                    }
                    informacoes.situacaoVoo = Convert.ToString(values[10 + colunaExtra]).ToUpper();
                    informacoes.codigoJustificativa = Convert.ToString(values[11 + colunaExtra]);
                    if (informacoes.atrasoPartida != "Cancelado")
                    {
                        TimeSpan tempoDeAtrasoPartida = informacoes.partidaReal.Subtract(informacoes.partidaPrevista);
                        informacoes.atrasoPartida = VerificaAtraso(tempoDeAtrasoPartida, informacoes);
                    }
                    if (informacoes.atrasoChegada != "Cancelado")
                    {
                        TimeSpan tempoDeAtrasoChegada = informacoes.chegadaReal.Subtract(informacoes.chegadaPrevista);
                        informacoes.atrasoChegada = VerificaAtraso(tempoDeAtrasoChegada, informacoes);
                    }
                }
                return informacoes;
            }
            catch
            {
                throw new Exception("O arquivo não está dentro dos padrões necessários.");
            }
        }

        private static string VerificaAtraso(TimeSpan tempoDeAtraso, InformacoesVoos informacoes)
        {
            string atraso = string.Empty;
            if (tempoDeAtraso < TimeSpan.Zero)
            {
                atraso = "Adiantado";
            }
            else if (tempoDeAtraso == TimeSpan.Zero)
            {
                atraso = "Pontual";
            }
            else if (tempoDeAtraso <= new TimeSpan(0, 10, 0) && tempoDeAtraso > TimeSpan.Zero)
            {
                atraso = "0-10 min atrasado";
            }
            else if (tempoDeAtraso > new TimeSpan(0, 10, 0) && tempoDeAtraso <= new TimeSpan(1, 0, 0))
            {
                atraso = "10-60 min atrasado";
            }
            else if (tempoDeAtraso > new TimeSpan(1, 0, 0))
            {
                atraso = ">60 minutos atrasado";
            }
            else
            {
                atraso = "Cancelado";
            }
            return atraso;
        }
    }
}
