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
        public DateTime partidaReal { get; set; }
        public DateTime chegadaPrevista { get; set; }
        public DateTime chegadaReal { get; set; }
        public string situacaoVoo { get; set; }
        public string codigoJustificativa { get; set; }
        public string atrasoPartida { get; set; } 
        public string atrasoChegada { get; set; }


        public static InformacoesVoos FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(csvLine[3]);
            InformacoesVoos informacoes = new InformacoesVoos();
            informacoes.ICAOEmpresaAerea = Convert.ToString(values[0]);
            informacoes.numeroVoo = Convert.ToString(values[1]);
            informacoes.codigoDI = Convert.ToString(values[2]);
            informacoes.codigoTipoLinha = Convert.ToString(values[3]);
            informacoes.ICAOAerodromoOrigem = Convert.ToString(values[4]);
            informacoes.ICAOAerodromoDestino = Convert.ToString(values[5]);
            if (!string.IsNullOrEmpty(values[6]))
            {
                informacoes.partidaPrevista = Convert.ToDateTime(values[6]);
            }
            if (!string.IsNullOrEmpty(values[7]))
            {
                informacoes.partidaReal = Convert.ToDateTime(values[7]);
            }
            else
            {
                informacoes.atrasoPartida = "Cancelado";
                informacoes.atrasoChegada = "Cancelado";
            }
            if (!string.IsNullOrEmpty(values[8]))
            {
                informacoes.chegadaPrevista = Convert.ToDateTime(values[8]);
            }
            if (!string.IsNullOrEmpty(values[9]))
            {
                informacoes.chegadaReal = Convert.ToDateTime(values[9]);
            }
            informacoes.situacaoVoo = Convert.ToString(values[10]);
            informacoes.codigoJustificativa = Convert.ToString(values[11]);
            if (informacoes.atrasoPartida != "Cancelado")
            {
                TimeSpan tempoDeAtrasoPartida = informacoes.partidaReal.Subtract(informacoes.partidaPrevista);
                VerificaAtrasoPartida(tempoDeAtrasoPartida, informacoes);
            }
            if (informacoes.atrasoChegada != "Cancelado")
            {
                TimeSpan tempoDeAtrasoChegada = informacoes.chegadaReal.Subtract(informacoes.chegadaPrevista);
                VerificaAtrasoChegada(tempoDeAtrasoChegada, informacoes);
            }
            return informacoes;
        }

        private static void VerificaAtrasoChegada(TimeSpan tempoDeAtrasoChegada, InformacoesVoos informacoes)
        {
            if (tempoDeAtrasoChegada < TimeSpan.Zero)
            {
                informacoes.atrasoChegada = "Adiantado";
            }
            else if (tempoDeAtrasoChegada == TimeSpan.Zero)
            {
                informacoes.atrasoChegada = "Pontual";
            }
            else if (tempoDeAtrasoChegada <= new TimeSpan(0, 10, 0) && tempoDeAtrasoChegada > TimeSpan.Zero)
            {
                informacoes.atrasoChegada = "0-10 min atrasado";
            }
            else if (tempoDeAtrasoChegada > new TimeSpan(0, 10, 0) && tempoDeAtrasoChegada <= new TimeSpan(1, 0, 0))
            {
                informacoes.atrasoChegada = "10-60 min atrasado";
            }
            else if (tempoDeAtrasoChegada > new TimeSpan(1, 0, 0))
            {
                informacoes.atrasoChegada = ">60 minutos atrasado";
            }
            else
            {
                informacoes.atrasoChegada = "Cancelado";
            }
        }

        private static void VerificaAtrasoPartida(TimeSpan tempoDeAtrasoPartida, InformacoesVoos informacoes)
        {
            if (tempoDeAtrasoPartida < TimeSpan.Zero)
            {
                informacoes.atrasoPartida = "Adiantado";
            }
            else if (tempoDeAtrasoPartida == TimeSpan.Zero)
            {
                informacoes.atrasoPartida = "Pontual";
            }
            else if (tempoDeAtrasoPartida <= new TimeSpan(0, 10, 0) && tempoDeAtrasoPartida > TimeSpan.Zero)
            {
                informacoes.atrasoPartida = "0-10 min atrasado";
            }
            else if (tempoDeAtrasoPartida > new TimeSpan(0, 10, 0) && tempoDeAtrasoPartida <= new TimeSpan(1, 0, 0))
            {
                informacoes.atrasoPartida = "10-60 min atrasado";
            }
            else if (tempoDeAtrasoPartida > new TimeSpan(1, 0, 0))
            {
                informacoes.atrasoPartida = ">60 minutos atrasado";
            }
            else
            {
                informacoes.atrasoPartida = "Cancelado";
            }
        }
    }
}
