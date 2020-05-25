import { Injectable } from '@angular/core';
import { Regra } from '../modelos/regra';

import '../util';
import { getTerminais } from '../util';

export interface Conjunto {
  [key: string]: string[];
}

export interface Status {
  [key: string]: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class PrimeiroService {

  private regras: Regra[];
  private primeiros: Conjunto = {};
  private terminais: string[];
  private pilhaDeBusca: boolean[] = [];

  constructor() { }

  public primeiro(regras: Regra[]) {
    this.terminais = getTerminais(regras);

    regras.forEach(a => {
      this.primeiros[a.variavel] = [];
      this.pilhaDeBusca[a.variavel] = [];
    });


    this.regras = regras;
    let primeiro = {};

    for (let i = 0; i < regras.length; i++) {
      this.buscarPrimeiro(i, regras[i]);
    }

    debugger;
  }

  private buscarPrimeiro(sequencial: number, atual: Regra) {

    if (this.pilhaDeBusca[atual.variavel] == true) {
      debugger;
      // return;
      throw 'Infinite Loop';
    }

    this.pilhaDeBusca[atual.variavel] = true;

    // navega nas sentenças
    for (let i = 0; i < atual.cadeias.length; i++) {

      // navega nas cadeias
      for (let j = 0; j < atual.cadeias[i].length; j++) {
        let simbolo = atual.cadeias[i][j];

        // REGRA: se o simbolo é terminal, o PRIMEIRO(simbolo) = {simbolo}
        if (this.terminais.includes(simbolo)) {
          this.primeiros[atual.variavel] = this.empilhar(this.primeiros[atual.variavel], [simbolo]);
        }
        else {
          debugger;
          let proximo = this.buscarRegra(simbolo);
          this.buscarPrimeiro(0, proximo);
          debugger;

          // adiciona a cadeia de PRIMEIROS(simbolo) encontrados, a variavel atual
          this.primeiros[atual.variavel] = this.empilhar(this.primeiros[atual.variavel], this.primeiros[simbolo])
        }
      }
    }

    this.pilhaDeBusca[atual.variavel] = false;
  }

  private empilhar(array: string[], valor: string[]): string[] {

    valor.forEach(a => {
      if (!array.includes(a))
        array = array.concat(a);
    });

    return array;
  }

  private buscarRegra(simbolo: string) {
    return this.regras.find(a => a.variavel == simbolo);
  }
}
