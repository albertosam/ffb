using MultiAgentes.Api.Application.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MultiAgentes.Api.Application
{
    public static class TabuleiroConstruir
    {
        public static Tabuleiro Construir(int tamanho)
        {
            var tabuleiro = new Tabuleiro();
            tabuleiro.Dimensao = tamanho;
            tabuleiro.Posicoes = new Posicao[tamanho, tamanho];

            bool bordaCima, bordaEsquerda, bordaDireita, bordaBaixo = false;

            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    // se i == 0 -> borda cima
                    // se i == tamanho-1 -> borda baixo
                    // se j == 0 -> borda esquerda
                    // se j == tamanho-1 -> dorda direita

                    bordaCima = i == 0;
                    bordaEsquerda = j == 0;
                    bordaDireita = j == tamanho - 1;
                    bordaBaixo = i == tamanho - 1;

                    tabuleiro.Posicoes[i, j] = new Posicao(tabuleiro, i, j, bordaCima, bordaEsquerda, bordaDireita, bordaBaixo);
                }
            }

            return tabuleiro;
        }
    }
}
