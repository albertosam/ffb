import { Injectable } from '@angular/core';
import { ConversorService } from './conversor.service';
import { Regra } from '../modelos/regra';

export interface Token {
  isVariavel: boolean;
  texto: string;

}
export interface GramaticaRegra {
  texto: string;
  partes: number[];
  primeiros: string[];
  seguintes: string[];
}

export interface Gramatica {
  variaveis: string[];
  terminais: string[];
  regras: { [key: string]: GramaticaRegra };
  indices: Indice[]
  tamanho: number;
}

export interface Indice {
  //[key: string]: any;
  texto: string;
  tokens: Token[];
  variavel: string;
}

@Injectable({
  providedIn: 'root'
})
export class Ll1Service {
  private gramatica: Gramatica;

  constructor(private conversor: ConversorService) { }

  public construir(gramaticaTexto: string): Gramatica {
    this.construirGramatica(gramaticaTexto);
    this.identificarPrimeirosSeguintes();

    return this.gramatica;
  }


  private construirGramatica(gramaticaTexto: string) {
    let cont = 1;

    this.gramatica = { variaveis: [], indices: [], regras: {}, terminais: [], tamanho: 0 };
    gramaticaTexto.split('\n').forEach(linha => {

      linha = linha.trim();
      if (linha.length == 0)
        return;

      let expressoes = linha.split('->');
      if (expressoes.length < 2)
        throw 'Expressão em formato incorreto. É esperado o seguinte formato: VARIAVEL -> EXPRESSAO';

      let variavel = expressoes[0].trim();
      let definicao = expressoes[1].trim();



      let partes = definicao.split('|');
      let partesIndexes = [];

      partes.forEach(parte => {
        let cadeias = parte.split(' ');
        let tokens = [];
        cadeias.forEach(token => {
          if (token.trim() === '') return;

          tokens.push({ texto: token, isVariavel: false });
        });

        this.gramatica.indices[cont] = { texto: parte, tokens: tokens, variavel: variavel };
        partesIndexes.push(cont);
        cont++;
      });

      this.gramatica.regras[variavel] = { texto: definicao, partes: partesIndexes, primeiros: [], seguintes: [] };
      this.gramatica.variaveis.push(variavel);
    });

    this.gramatica.tamanho = cont - 1;

    // revisa os tokens para indicando se é variável ou terminal
    for (var i = 1; i < cont; i++) {
      for (var ii = 0; ii < this.gramatica.indices[i].tokens.length; ii++) {
        let token = this.gramatica.indices[i].tokens[ii].texto;
        // se o token está na lista de variáveis, atualiza lista de índices
        if (this.gramatica.variaveis.includes(token)) {
          this.gramatica.indices[i].tokens[ii].isVariavel = true;
        } else {
          // atualiza lista de terminais
          if (this.gramatica.indices[i].tokens[ii].texto != '%') {
            this.gramatica.terminais = this.gramatica.terminais.concat(this.gramatica.indices[i].tokens[ii].texto);
          }
        }
      }
    }
  }

  private identificarPrimeirosSeguintes() {
    this.gramatica.variaveis.forEach(variavel => {
      this.gramatica.regras[variavel].primeiros = this.getPrimeiro(variavel);
      debugger;
      this.gramatica.regras[variavel].seguintes = this.getSeguinte(variavel, variavel, true);
    });
  }

  private getPrimeiro(variavel: string): string[] {
    var resultado: string[] = [];

    this.gramatica.regras[variavel].partes.forEach(parte => {
      for (var i = 0; i < this.gramatica.indices[parte].tokens.length; i++) {
        let token = this.gramatica.indices[parte].tokens[i];

        if (!token.isVariavel) {
          resultado = this.incluirValor(resultado, token.texto);
          break;
        } else {
          if (token.texto == variavel) {
            debugger;
            break;
          }

          let primeiros = this.getPrimeiro(token.texto);
          if (primeiros.join('').endsWith('%')) {
            if (i != this.gramatica.indices[parte].tokens.length - 1) {
              primeiros.splice(primeiros.length - 1, 1);
            }
            resultado = this.incluirValores(resultado, primeiros);
            break;
          } else {
            resultado = this.incluirValores(resultado, primeiros);
            break;
          }
        }

      }
    });

    return resultado;
  }

  private getSeguinte(variavel: string, variavelInicial: string, inicio: boolean) {
    let resultado: string[] = [];

    if (variavel == variavelInicial && !inicio)
      return resultado;

    if (this.gramatica.indices[1].variavel == variavel)
      resultado = ['$'];

    for (let i = 1; i <= this.gramatica.tamanho; i++) {
      const atual = this.gramatica.indices[i];
      let encontrado = false;

      for (let j = 0; j < atual.tokens.length; j++) {
        const token = atual.tokens[j];
        if (token.texto === variavel) {
          encontrado = true;
          if (j == atual.tokens.length - 1 && atual.variavel != token.texto) {
            let seguinte = this.getSeguinte(atual.variavel, variavelInicial, false);
            resultado = this.incluirValores(resultado, seguinte);
          }
          continue;
        }

        if (encontrado == true) {
          if (token.isVariavel == false) {
            resultado = this.incluirValor(resultado, token.texto);
            break;
          }

          let primeiro = this.getPrimeiro(token.texto);
          if (primeiro.join('').endsWith('%')) {
            if (j == atual.tokens.length - 1) {
              let seguinte = this.getSeguinte(atual.variavel, variavelInicial, false);
              resultado = this.incluirValores(resultado, seguinte);
            }

            primeiro.splice(primeiro.length - 1, 1);
            resultado = this.incluirValores(resultado, primeiro);
          }
          else {
            resultado = this.incluirValores(resultado, primeiro);
            break;
          }
        }
      }
    }

    return resultado;
  }

  private incluirValor(array: string[], valor: string) {
    if (!array.includes(valor))
      array = array.concat(valor);

    return array;
  }

  private incluirValores(array: string[], valores: string[]) {

    valores.forEach(valor => {
      if (!array.includes(valor))
        array = array.concat(valor);
    });

    return array;
  }
}
