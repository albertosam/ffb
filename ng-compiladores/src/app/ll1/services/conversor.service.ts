import { Injectable } from '@angular/core';
import { Regra } from '../modelos/regra';

@Injectable({
  providedIn: 'root'
})
export class ConversorService {

  constructor() { }

  public converter(texto: string): Regra[] {
    let entrada = this.dividir(texto);
    let regras = this.converterEmRegras(entrada);

    return regras;
  }

  private dividir(texto: string): string[][] {
    let cadeias = texto.split('\n')
      .map((linha: string) => linha.trim())
      .filter((linha: string) => linha.length)
      .map((linha: string) => {
        return linha.split(' ')
          .map((palavra: string) => palavra.trim())
          .filter((palavra: string) => palavra.length)
      });

    return cadeias;
  }

  private converterEmRegras(cadeias: string[][]): Regra[] {
    let regras: Regra[] = [];

    for (let i = 0; i < cadeias.length; i++) {

      const simbolo = cadeias[i][0];

      cadeias[i].shift();
      cadeias[i].shift();

      let expressoes = cadeias[i].join(' ')
        .split('|')
        .map((expressao: string) => {
          return expressao.split(' ').filter((palavra: string) => palavra.length)
        })
        .filter((expressao: string[]) => expressao.length > 0);

      regras.push({ variavel: simbolo, cadeias: expressoes });
    }

    return regras;
  }
}
