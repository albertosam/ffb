namespace MultiAgentes.Lib.Services
{
    using MultiAgentes.Lib.Requests;
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;

    /// <summary>
    /// Defines the <see cref="ICentralClient" />.
    /// </summary>
    public interface ICentralClient
    {
        /// <summary>
        /// The GetPosicaoAtual.
        /// </summary>
        /// <returns>The <see cref="Posicionamento"/>.</returns>
        Posicionamento GetPosicaoAtual();

        /// <summary>
        /// The LimpezaRealizada.
        /// </summary>
        /// <param name="posicao">The posicao<see cref="Posicionamento"/>.</param>
        void LimpezaRealizada(Posicionamento posicao);

        /// <summary>
        /// The ProximaPosicao.
        /// </summary>
        /// <returns>The <see cref="Posicionamento"/>.</returns>
        Posicionamento ProximaPosicao();

        /// <summary>
        /// The Movimentar.
        /// </summary>
        /// <param name="direcao">The direcao<see cref="int"/>.</param>
        /// <returns>The <see cref="Posicionamento"/>.</returns>
        Posicionamento Movimentar(int direcao);

        /// <summary>
        /// The Registrar.
        /// </summary>
        /// <param name="nome">The nome<see cref="string"/>.</param>
        /// <returns>The <see cref="Posicionamento"/>.</returns>
        Posicionamento Registrar(string nome);
    }

    /// <summary>
    /// Defines the <see cref="CentralClient" />.
    /// </summary>
    public class CentralClient : ICentralClient
    {
        /// <summary>
        /// Defines the httpClient.
        /// </summary>
        private readonly HttpClient httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="CentralClient"/> class.
        /// </summary>
        /// <param name="httpClient">The httpClient<see cref="System.Net.Http.HttpClient"/>.</param>
        public CentralClient(System.Net.Http.HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri("https://localhost:5001/");
        }

        /// <summary>
        /// The GetPosicaoAtual.
        /// </summary>
        /// <returns>The <see cref="Posicionamento"/>.</returns>
        public Posicionamento GetPosicaoAtual()
        {
            var result = this.httpClient.GetAsync("central/posicao");
            var httpResponse = result.GetAwaiter().GetResult();

            var posicao = httpResponse.Read<Posicionamento>();
            return posicao;
        }

        /// <summary>
        /// The LimpezaRealizada.
        /// </summary>
        /// <param name="posicao">The posicao<see cref="Posicionamento"/>.</param>
        public void LimpezaRealizada(Posicionamento posicao)
        {
            this.httpClient.PostAsync("central/limpar", new JsonContent(posicao)).Wait();
        }

        /// <summary>
        /// The Registrar.
        /// </summary>
        /// <param name="nome">The nome<see cref="string"/>.</param>
        /// <returns>The <see cref="Posicionamento"/>.</returns>
        public Posicionamento Registrar(string nome)
        {
            var result = this.httpClient.PostAsync("central/registrar", new JsonContent(nome));
            var httpResponse = result.GetAwaiter().GetResult();

            var posicao = httpResponse.Read<Posicionamento>();
            return posicao;
        }

        /// <summary>
        /// The Movimentar.
        /// </summary>
        /// <param name="direcao">The direcao<see cref="int"/>.</param>
        /// <returns>The <see cref="Posicionamento"/>.</returns>
        public Posicionamento Movimentar(int direcao)
        {
            var result = this.httpClient.PostAsync("central/movimentar", new JsonContent(direcao));
            var httpResponse = result.GetAwaiter().GetResult();

            var posicao = httpResponse.Read<Posicionamento>();
            return posicao;
        }

        /// <summary>
        /// The ProximaPosicao.
        /// </summary>
        /// <returns>The <see cref="Posicionamento"/>.</returns>
        public Posicionamento ProximaPosicao()
        {
            var result = this.httpClient.GetAsync("central/proximaPosicao");
            var httpResponse = result.GetAwaiter().GetResult();

            var posicao = httpResponse.Read<Posicionamento>();
            return posicao;
        }
    }

    /// <summary>
    /// Defines the <see cref="Util" />.
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// The Read.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="response">The response<see cref="HttpResponseMessage"/>.</param>
        /// <returns>The <see cref="T"/>.</returns>
        public static T Read<T>(this HttpResponseMessage response)
        {
            var body = response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<T>(body.Result);

            return obj;
        }
    }
}
