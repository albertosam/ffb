import { Injectable } from '@angular/core';
import { Gramatica } from '../modelos/gramatica';
import { criarGramatica, identificarFirstFollow, montarTabelaPreditiva } from '../util/gramatica';
import { incluirValores, incluirValor } from '../util/listas';
import { validar } from '../util/validador';

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
    identificarFirstFollow(this.gramatica);
    montarTabelaPreditiva(this.gramatica);

    return this.gramatica;
  }

  public validar(entrada: string) {
    return validar(entrada, this.gramatica);
  }
  
}
