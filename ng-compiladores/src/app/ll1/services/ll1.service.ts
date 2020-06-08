import { Injectable } from '@angular/core';
import { Gramatica } from '../modelos/garmatica';


@Injectable({
  providedIn: 'root'
})
export class Ll1Service {
  private gramatica: Gramatica;
  private pilhaDeBusca: string[];

  constructor() { }

  public construir(gramaticaTexto: string): Gramatica {
    this.construirObjetoGramatica(gramaticaTexto);
    this.identificarFirstFollow();
    this.construirTabela();
    return this.gramatica;
  }


  private construirObjetoGramatica(gramaticaTexto: string) {
    let cont = 1;

    this.gramatica = { variaveis: [], indices: [], regras: {}, terminais: ['$'], tamanho: 0, tabela: [] };
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

      this.gramatica.regras[variavel] = { texto: definicao, partes: partesIndexes, first: [], follow: [] };
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

  private identificarFirstFollow() {
    this.gramatica.variaveis.forEach(variavel => {
      this.gramatica.regras[variavel].first = this.getConjuntoFirst(variavel);

      this.pilhaDeBusca = [];  // limpa pilha de variáveis buscadas
      this.gramatica.regras[variavel].follow = this.getConjuntoFollow(variavel, variavel, true);
    });
  }

  /**
   * αƐß
   * 
   * 0) First(Ɛ) = {Ɛ}
   * 1) Fisrt(a) = {a} onde a é terminal
   * 2) A -> aα , então First(A) = {a} sendo a terminal
   * 3) A -> Bα , então First(A) = First(B) , onde B não deriva em Ɛ
   * 4) A -> Bα , então First(A) = {First(B) - Ɛ} U {First(α)} , onde B deriva em Ɛ
   * 
   *  
   * 
   * @param variavel 
   */
  private getConjuntoFirst(variavel: string): string[] {
    var resultado: string[] = [];

    this.gramatica.regras[variavel].partes.forEach(parte => {
      for (var i = 0; i < this.gramatica.indices[parte].tokens.length; i++) {
        let token = this.gramatica.indices[parte].tokens[i];

        // regra (1) e (2) - FIRST de terminal é ele mesmo
        if (!token.isVariavel) {
          resultado = this.incluirValor(resultado, token.texto);
          break;
        } else {
          if (token.texto == variavel) {
            debugger;
            break;
          }

          let primeiros = this.getConjuntoFirst(token.texto);

          // regra (4) - conjunto FIRST deriva em Ɛ
          if (primeiros.join('').endsWith('%')) {
            if (i != this.gramatica.indices[parte].tokens.length - 1) {
              primeiros.splice(primeiros.length - 1, 1);
            }
            resultado = this.incluirValores(resultado, primeiros);
            break;
          } else {
            // regra (3) adiciona ao FIRST o FIRST do token atual que é variável
            resultado = this.incluirValores(resultado, primeiros);
            break;
          }
        }

      }
    });

    return resultado;
  }

  /**
   * αƐß
   * 
   * 1) Se S é inicial, então Follow(S) = {$}. $ Indica final da sentença.
   * 2) A -> αXß , então Follow(X) = {Fisrt(ß) - Ɛ}
   * 3) A -> αX ou A -> αXß , onde First(ß) contém Ɛ então Follow(X) contém Follow(A)
   *    
   * 
   * @param variavel 
   * @param variavelInicial 
   * @param inicio 
   */
  private getConjuntoFollow(variavel: string, variavelInicial: string, inicio: boolean) {
    let resultado: string[] = [];

    // se a variável que está buscando já está pilha de busca
    // evitar loop infinito
    if (this.pilhaDeBusca.includes(variavel))
      return resultado;

    // inclui variável na pilha de busca
    this.pilhaDeBusca.push(variavel);

    if (variavel == variavelInicial && !inicio)
      return resultado;

    // regra (1) - VARIAVEL é o não terminal inicial
    if (this.gramatica.indices[1].variavel == variavel)
      resultado = ['$'];

    for (let i = 1; i <= this.gramatica.tamanho; i++) {
      const atual = this.gramatica.indices[i];
      let encontrado = false;

      for (let j = 0; j < atual.tokens.length; j++) {
        const token = atual.tokens[j];

        // encontra variável em regra de produção
        if (token.texto === variavel) {
          encontrado = true;

          // regra (2) - o conjunto FIRST do token seguinte, é adicionado ao conjunto FOLLOW(VARIAVEL)
          if (j == atual.tokens.length - 1 && atual.variavel != token.texto) {
            let seguinte = this.getConjuntoFollow(atual.variavel, variavelInicial, false);
            resultado = this.incluirValores(resultado, seguinte);
          }
          continue;
        }

        if (encontrado == true) {
          if (token.isVariavel == false) {
            resultado = this.incluirValor(resultado, token.texto);
            break;
          }

          let primeiro = this.getConjuntoFirst(token.texto);
          // regra (3) - FIRST contém Ɛ
          if (primeiro.join('').endsWith('%')) {
            if (j == atual.tokens.length - 1) {
              let seguinte = this.getConjuntoFollow(atual.variavel, variavelInicial, false);
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

  private construirTabela() {
    let tabela: any[] = [];

    this.gramatica.variaveis.forEach((variavel) => {
      tabela[variavel] = [];

      this.gramatica.terminais.forEach((terminal) => {

        if (this.gramatica.regras[variavel].first.includes(terminal)) {
          let indice = this.getIndice(variavel, terminal);
          tabela[variavel][terminal] = indice;
        } else if (this.gramatica.regras[variavel].follow.includes(terminal)) {
          if (this.gramatica.regras[variavel].first.includes('%')) {
            let indice = this.getIndice(variavel, '%');
            tabela[variavel][terminal] = indice;
          }
        } else {
          tabela[variavel][terminal] = null;
        }

      });

    });

    this.gramatica.tabela = tabela;
  }

  private getIndice(variavel: string, terminal: string) {
    let saida: number;

    for (let i = 0; i < this.gramatica.regras[variavel].partes.length; i++) {
      let parte = this.gramatica.regras[variavel].partes[i];

      for (let j = 0; j < this.gramatica.indices[parte].tokens.length; j++) {
        let token = this.gramatica.indices[parte].tokens[j];
        if (!token.isVariavel) {
          if (token.texto == terminal) {
            saida = parte;
          }
          break;
        } else {
          var primeiros = this.gramatica.regras[token.texto].first;
          if (primeiros.indexOf(terminal) !== -1) {
            saida = parte;
          }
          break;
        }
      }
    }

    return saida;
  }
}
