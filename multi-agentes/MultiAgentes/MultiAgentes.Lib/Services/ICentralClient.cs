using Microsoft.VisualBasic;
using MultiAgentes.Lib.Core;
using MultiAgentes.Lib.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MultiAgentes.Lib.Services
{
    public interface ICentralClient
    {
        Posicao_ GetPosicaoAtual();
        void LimpezaRealizada(Posicao_ posicao);
        Posicao_ ProximaPosicao();
        Posicao_ Movimentar(int direcao);
        Posicao_ Registrar(string nome);
    }


    public class CentralClient : ICentralClient
    {
        private readonly HttpClient httpClient;

        public CentralClient(System.Net.Http.HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri("https://localhost:5001/");
        }

        public Posicao_ GetPosicaoAtual()
        {
            var result = this.httpClient.GetAsync("central/posicao");
            var httpResponse = result.GetAwaiter().GetResult();

            var posicao = httpResponse.Read<Posicao_>();
            return posicao;
        }

        public void LimpezaRealizada(Posicao_ posicao)
        {
            this.httpClient.PostAsync("central/limpar", new JsonContent(posicao)).Wait();
        }

        public Posicao_ Registrar(string nome)
        {
            var result = this.httpClient.PostAsync("central/registrar", new JsonContent(nome));
            var httpResponse = result.GetAwaiter().GetResult();

            var posicao = httpResponse.Read<Posicao_>();
            return posicao;
        }

        public Posicao_ Movimentar(int direcao)
        {
            var result = this.httpClient.PostAsync("central/movimentar", new JsonContent(direcao));
            var httpResponse = result.GetAwaiter().GetResult();

            var posicao = httpResponse.Read<Posicao_>();
            return posicao;
        }

        public Posicao_ GetPosicao()
        {
            var result = this.httpClient.GetAsync("central/posicao");
            var httpResponse = result.GetAwaiter().GetResult();

            var posicao = httpResponse.Read<Posicao_>();
            return posicao;
        }

        public Posicao_ ProximaPosicao()
        {
            var result = this.httpClient.GetAsync("central/proximaPosicao");
            var httpResponse = result.GetAwaiter().GetResult();

            var posicao = httpResponse.Read<Posicao_>();
            return posicao;
        }
    }

    public static class Util
    {
        public static T Read<T>(this HttpResponseMessage response)
        {
            var body = response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<T>(body.Result);

            return obj;
        }
    }
}
