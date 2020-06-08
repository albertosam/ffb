import { Component, OnInit } from '@angular/core';
import { Ll1Service } from '../services/ll1.service';
import { GramaticasExemplo, CodigosExemplo, toFirstView, Combo, toFollowView, toLoggerTable } from './dados';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  erro: string = '';
  entradaGramatica: string = '';
  entradaCodigo: string;
  primeiros: { variavel: string, valores: string }[] = [];
  seguintes: { variavel: string, valores: string }[] = [];
  combo = Combo;

  logger: any[][];

  gramatica: any;
  processado: boolean = false;

  constructor(private ll1Service: Ll1Service) { }

  ngOnInit(): void {
  }

  selecionar(event: any) {
    debugger;
    this.entradaGramatica = GramaticasExemplo[event.target.value];
    this.entradaCodigo = CodigosExemplo[event.target.value];
  }

  processar() {
    this.erro = '';
    this.primeiros = [];
    this.seguintes = [];

    try {
      this.gramatica = this.ll1Service.construir(this.entradaGramatica);
      this.primeiros = toFirstView(this.gramatica);
      this.seguintes = toFollowView(this.gramatica);
      if (this.entradaCodigo) {

        let validadorResultado = this.ll1Service.validar(this.entradaCodigo);
        this.logger = toLoggerTable(validadorResultado);
      }

      this.processado = true;
    }
    catch (error) {
      debugger;
      this.erro = error;
    }
  }
}
