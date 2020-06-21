using Microsoft.VisualBasic;
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
        Task<Posicao> GetPosicaoAtual();
        Task LimpezaRealizada(Posicao posicao);
        Task<Posicao> ProximaPosicao();
        Task<Posicao> Movimentar(int direcao);
        Task<Posicao> Registrar(string nome);
    }


    public class CentralClient : ICentralClient
    {
        private readonly HttpClient httpClient;

        public CentralClient(System.Net.Http.HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri("https://localhost:5001/");
        }

        public async Task<Posicao> GetPosicaoAtual()
        {
            var result = await this.httpClient.GetAsync("central/posicao");
            var posicao = result.Read<Posicao>();
            return posicao;
        }

        public async Task LimpezaRealizada(Posicao posicao)
        {
            await this.httpClient.PostAsync("central/limpar", new JsonContent(posicao));
        }

        public async Task<Posicao> Registrar(string nome)
        {
            var result = await this.httpClient.PostAsync("central/registrar", new JsonContent(nome));
            var posicao = result.Read<Posicao>();
            return posicao;
        }

        public async Task<Posicao> Movimentar(int direcao)
        {
            var result = await this.httpClient.PostAsync("central/movimentar", new JsonContent(direcao));
            var posicao = result.Read<Posicao>();
            return posicao;
        }
        public async Task<Posicao> ProximaPosicao()
        {
            var result = await this.httpClient.GetAsync("central/proximaPosicao");
            var posicao = result.Read<Posicao>();
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
