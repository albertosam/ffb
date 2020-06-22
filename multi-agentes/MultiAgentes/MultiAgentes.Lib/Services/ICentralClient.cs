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
        Posicionamento GetPosicaoAtual();
        void LimpezaRealizada(Posicionamento posicao);
        Posicionamento ProximaPosicao();
        Posicionamento Movimentar(int direcao);
        Posicionamento Registrar(string nome);
    }


    public class CentralClient : ICentralClient
    {
        private readonly HttpClient httpClient;

        public CentralClient(System.Net.Http.HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri("https://localhost:5001/");
        }

        public Posicionamento GetPosicaoAtual()
        {
            var result = this.httpClient.GetAsync("central/posicao");
            var httpResponse = result.GetAwaiter().GetResult();

            var posicao = httpResponse.Read<Posicionamento>();
            return posicao;
        }

        public void LimpezaRealizada(Posicionamento posicao)
        {
            this.httpClient.PostAsync("central/limpar", new JsonContent(posicao)).Wait();
        }

        public Posicionamento Registrar(string nome)
        {
            var result = this.httpClient.PostAsync("central/registrar", new JsonContent(nome));
            var httpResponse = result.GetAwaiter().GetResult();

            var posicao = httpResponse.Read<Posicionamento>();
            return posicao;
        }

        public Posicionamento Movimentar(int direcao)
        {
            var result = this.httpClient.PostAsync("central/movimentar", new JsonContent(direcao));
            var httpResponse = result.GetAwaiter().GetResult();

            var posicao = httpResponse.Read<Posicionamento>();
            return posicao;
        }

        public Posicionamento GetPosicao()
        {
            var result = this.httpClient.GetAsync("central/posicao");
            var httpResponse = result.GetAwaiter().GetResult();

            var posicao = httpResponse.Read<Posicionamento>();
            return posicao;
        }

        public Posicionamento ProximaPosicao()
        {
            var result = this.httpClient.GetAsync("central/proximaPosicao");
            var httpResponse = result.GetAwaiter().GetResult();

            var posicao = httpResponse.Read<Posicionamento>();
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
