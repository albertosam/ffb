import { Injectable } from '@angular/core';
import { Gramatica } from '../modelos/garmatica';

class Log1 {
  token: string;
  mensagem: string;
  erro: boolean;
}

class Log {
  acao: string;

  constructor(public topo: string, public token: string, public cadeia: string, public pilha: string[]) {
  }
}

class Passo {
  log: Log;
  erro: boolean;
  erroMensagem: string;
}

class Logger {

  passos: { [key: number]: Passo[] };
  log: Log[] = [];

  constructor() {
    this.passos = []
  }

  addErro(ponteiro: number, mensagem: string) {
    //this.passos[ponteiro].push()
  }

  addLog(ponteiro: number, topo: string, token: string, cadeia: string, pilha: string[]) {
    if (!this.passos[ponteiro])
      this.passos[ponteiro] = [];

    let passo = new Log(topo, token, cadeia, pilha.map(a => a));
    this.passos[ponteiro].push({ log: passo, erro: false, erroMensagem: '' });
    this.log.push(passo);

    return passo;
  }

  toTable() {
    let tabela = [[]];

    for (let index = 0; index < this.log.length; index++) {
      tabela[index] = [];
      tabela[index][0] = this.log[index].cadeia;
      tabela[index][1] = this.log[index].pilha.reverse().join('');
      tabela[index][2] = this.log[index].topo;
      tabela[index][3] = this.log[index].token;
      tabela[index][4] = this.log[index].acao;
    }

    return tabela;
  }
}

@Injectable({
  providedIn: 'root'
})
export class Ll1ParserService {
  private pilha: string[];
  private ponteiro: number;
  private gramatica: Gramatica;
  private topo: string;
  private cadeia: string[];
  private SEPARADOR = ' ';

  private log: Log1[];
  public logger: Logger = new Logger();

  constructor() { }

  private addLog(topo: string, token: string) {
    let passo = this.logger.addLog(this.ponteiro, topo, token, this.cadeia.filter((a, c, arrar) => c > this.ponteiro).join(''), this.pilha);
    return passo;
  }

  public validate(entrada: string, gramatica: Gramatica) {
    this.cadeia = entrada.concat(' $ ').split(' ').map(a => a.trim()).filter(a => a.length > 0);
    this.gramatica = gramatica;
    this.pilha = [];
    this.ponteiro = -1;

    this.pilha.push('$');
    this.pilha.push(gramatica.variaveis[0]);

    let tokenAtual = this.proximoToken();

    let estouro = 1000;
    let i = 0;

    let topo: string = '';
    let passo: Log;

    while (true && i < estouro) {
      topo = this.getTopo();

      // entrada em log
      passo = this.addLog(topo, tokenAtual);

      if (this.isTerminal(topo)) {
        if (topo == tokenAtual) {
          if (topo == '$') {
            break;
          }

          this.pilha.pop();
          tokenAtual = this.proximoToken();

          continue;
        } else {
          this.pilha.pop();
          this.logger.addErro(this.ponteiro, 'Token não é reconhecido');
          continue;
        }
      } else {
        var celula = this.gramatica.tabela[topo][tokenAtual];
        if (!celula) {
          throw 'Verificar';
        }

        this.pilha.pop();
        let indice = this.gramatica.indices[celula];
        var novosTokens = indice.tokens;
        passo.acao = indice.variavel.concat(' -> ').concat(indice.texto);

        for (let index = novosTokens.length - 1; index >= 0; index--) {
          let item = novosTokens[index].texto;
          this.pilha.push(item);
        }
      }

      if (!this.pilha.length)
        break;

      i++;
    }

  }

  private proximoToken(): string {
    if (this.ponteiro > this.cadeia.length)
      return undefined;

    this.ponteiro++;
    let token = this.cadeia[this.ponteiro];
    return token;
  }

  private getTopo() {
    let pos = this.pilha.length - 1;
    return this.pilha[pos];
  }

  private isTerminal(valor: string) {
    return this.gramatica.terminais.includes(valor);
  }

}
