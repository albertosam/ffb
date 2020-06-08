import { Injectable } from '@angular/core';
import { Gramatica } from '../modelos/garmatica';
import { criarGramatica } from '../util/gramatica';
import { incluirValores, incluirValor } from '../util/listas';
import { getConjuntoFirst } from '../util/first';
import { getConjuntoFollow } from '../util/follow';

@Injectable({
  providedIn: 'root'
})
export class Ll1Service {
  private gramatica: Gramatica;
  private pilhaDeBusca: string[];

  constructor() { }

  public construir(gramaticaTexto: string): Gramatica {
    debugger;
    this.gramatica = criarGramatica(gramaticaTexto);
    this.gramatica.identificarFirstFollow();
    this.gramatica.montarTabela();
    return this.gramatica;
  }




}
